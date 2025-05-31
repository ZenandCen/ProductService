using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProductService.Api.Data.Mappings;
using ProductService.Api.Data.Models;
using ProductService.Api.Extensions;
using ProductService.Application;
using ProductService.Application.Sagas;
using ProductService.BackgroundServices;
using ProductService.BackgroundServices.Consumers;
using ProductService.Common.Interfaces;
using ProductService.Common.Sagas;
using ProductService.Domain.Exceptions;
using ProductService.EventSourcing.PostgresEventStore;
using ProductService.Infrastructure;
using ProductService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddBackgroundServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/asfpnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPostgresEventStore(builder.Configuration.GetConnectionString("EventStoreDb")!);
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

// Cấu hình MassTransit một lần duy nhất
builder.Services.AddMassTransit(x =>
{
    // Đăng ký consumer cho Background Services
    x.AddConsumer<NotificationConsumer>();

    // Đăng ký Saga
    x.AddSagaStateMachine<ProductCreationSaga, ProductCreationSagaState>()
       .EntityFrameworkRepository(r =>
       {
           r.ExistingDbContext<SagaDbContext>();
           r.UsePostgres();
       }); // Lưu trữ trạng thái Saga trong bộ nhớ (có thể thay bằng database nếu cần)

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        // Cấu hình endpoint cho NotificationConsumer
        cfg.ReceiveEndpoint("product-created-queue", e =>
        {
            e.ConfigureConsumer<NotificationConsumer>(context);
        });

        // Cấu hình endpoint cho Saga
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<IEventStore, PostgresEventStoreRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware để xử lý exception toàn cục
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var response = exception switch
        {
            DomainException domainEx => ApiResponse<ProductResponse>.Error(
                message: domainEx.Message,
                code: ErrorCodes.DomainValidationError, // Mã lỗi cho DomainException
                errors: null
            ),
            _ => ApiResponse<ProductResponse>.Error(
                message: "An unexpected error occurred",
                code: ErrorCodes.UnexpectedError, // Mã lỗi chung
                errors: exception?.Message
            )
        };

        context.Response.StatusCode = exception switch
        {
            DomainException _ => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
    });
});

// Endpoint minimalApi
app.MapProductEndpoints();


app.Run();


