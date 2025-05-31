@echo off 

:: --------------------------- Root PATH ----------------------------------------
set msbuildPath=C:\"Program Files"\"Microsoft Visual Studio"\2022\Community\Common7\Tools\VsDevCmd.bat

set rootSourceCodePath=C:\DDD\ProductService
set rootOutputPath=C:\CODE\Setup_MTO\Run_Development\backup\Auv_Config\Run_Development
set rootPublishPath=C:\CODE\Setup_MTO\Run_Development\backup\Auv_Config\Run_Development\publish\
set rootBuildPath=C:\CODE\Setup_MTO\Run_Development\build
set netVersion=net8.0
set localPath=\bin\local\%netVersion%
set localPathWithoutLocal=\bin\%netVersion%
set choice=0
::--------------------Need install azure-functions-core-tools to run azure function service---------------
::------------------------------npm install azure-functions-core-tools -g----------------------------------
:: --------------------------- The PATH of source project  ----------------------------------------

set coreWebSourceUrl=%rootSourceCodePath%\CoreService\Auvenir.Web

set fileServiceSourcetUrl=%rootSourceCodePath%\FileService\Auvenir.FileService.WebApi

set pm_PlanningStepSourceUrl=%rootSourceCodePath%\PlanningStepService\PlanningStep

set pm_CommonToolServiceSourceUrl=%rootSourceCodePath%\CommonToolService\CommonToolService

set pm_NonAccountWorkingPaperServiceSourceUrl=%rootSourceCodePath%\NonAccountWorkingPaperService

set wpSourceUrl=%rootSourceCodePath%\AccountWorkingPaperService\Auvenir.WorkingPaper

set feSourceUrl=%rootSourceCodePath%\FrontEnd

set spoApiSourceUrl=%rootSourceCodePath%\SPOApi

set spo_SPOSetupProcessorSourceUrl=%rootSourceCodePath%\SPOSetupProcessor

set fileIntegrityCheckProcessorUrl=%rootSourceCodePath%\FileIntegrityCheckProcessor\Auvenir.FileIntegrityCheckProcessor

set archiveWebJobSourceUrl=%rootSourceCodePath%\ArchivingWebJob\Archiving.Processor

set archiveApiSourceUrl=%rootSourceCodePath%\ArchivingWebAPI\Archiving.WebAPI

set contentProcessorSourceUrl=%rootSourceCodePath%\ContentProcessorService\Auvenir.ContentProcessor

set carryforwardProcessorSourceUrl=%rootSourceCodePath%\Carryforward\Auvenir.CarryforwardProcessor

set carryforwardContentProcessorSourceUrl=%rootSourceCodePath%\Carryforward\Auvenir.CfContentProcessor

set carryforwardCopyEngProcessorSourceUrl=%rootSourceCodePath%\Carryforward\Auvenir.CopyEngProcessor

set carryforwardFileTransferProcessorSourceUrl=%rootSourceCodePath%\Carryforward\Auvenir.FileTransferProcessor

set houseKeeperProcessorSourceUrl=%rootSourceCodePath%\HouseKeeperProcessor

set auvenirDuplicateEngProcessorUrl=%rootSourceCodePath%\DuplicateEngagement\Auvenir.DuplicateEngProcessor

set auvenirDuplicateFileProcessorUrl=%rootSourceCodePath%\DuplicateEngagement\Auvenir.DuplicateFileProcessor

set integrationHubProcessorUrl=%rootSourceCodePath%\IntegrationHubProcessor\Auvenir.IntegrationHubProcessor

set integrationHubServiceUrl=%rootSourceCodePath%\IntegrationHubService\Auvenir.IntegrationHubService

set annualSnapshotProcessorUrl=%rootSourceCodePath%\AnnualSnapshotProcessor\Auvenir.AnnualSnapshotProcessor

set notificationProcessorUrl=%rootSourceCodePath%\NotificationProcessor\NotificationProcessor

:: --------------------------- The PATH you want to build to   ----------------------------------------
set fePublishOutputUrl=%rootOutputPath%\Frontend\%version%

if %choice%==1 (
    set feOutputUrl=%fePublishOutputUrl%
) else (
   set feOutputUrl=%feSourceUrl%%localPath%
)

if %choice%==1 (
    set coreWebOutputUrl=%rootOutputPath%\BackEnd\%version%\Auvenir.Web
) else (
   set coreWebOutputUrl=%coreWebSourceUrl%%localPath%
)

if %choice%==1 (
    set pm_PlanningStepOutputUrl=%rootOutputPath%\BackEnd\%version%\PlanningStep
) else (
   set pm_PlanningStepOutputUrl=%pm_PlanningStepSourceUrl%%localPath%
)

if %choice%==1 (
    set pm_CommonToolServiceOutputUrl=%rootOutputPath%\BackEnd\%version%\CommonToolService
) else (
   set pm_CommonToolServiceOutputUrl=%pm_CommonToolServiceSourceUrl%%localPath%
)

if %choice%==1 (
    set pm_NonAccountWorkingPaperServiceOutputUrl=%rootOutputPath%\BackEnd\%version%\NonAccountWorkingPaperService
) else (
   set pm_NonAccountWorkingPaperServiceOutputUrl=%pm_NonAccountWorkingPaperServiceSourceUrl%%localPath%
)

if %choice%==1 (
    set wpOutputUrl=%rootOutputPath%\BackEnd\%version%\AccountWorkingPaperService
) else (
   set wpOutputUrl=%wpSourceUrl%%localPath%
)

if %choice%==1 (
    set spoApiOutputUrl=%rootOutputPath%\BackEnd\%version%\SPOApi
) else (
   set spoApiOutputUrl=%spoApiSourceUrl%%localPath%
)

if %choice%==1 (
    set spo_SPOSetupProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\SPOSetupProcessor
) else (
   set spo_SPOSetupProcessorOutputUrl=%spo_SPOSetupProcessorSourceUrl%%localPath%
)

if %choice%==1 (
    set fileIntegrityCheckProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\FileIntegrityCheckProcessor
) else (
   set fileIntegrityCheckProcessorOutputUrl=%fileIntegrityCheckProcessorUrl%%localPath%
)

if %choice%==1 (
    set fileServiceOutputUrl=%rootOutputPath%\BackEnd\%version%\FileService
) else (
   set fileServiceOutputUrl=%fileServiceSourcetUrl%%localPath%
)

if %choice%==1 (
    set archiveWebJobOutputUrl=%rootOutputPath%\BackEnd\%version%\ArchivingWebJob
) else (
   set archiveWebJobOutputUrl=%archiveWebJobSourceUrl%%localPath%
)

if %choice%==1 (
   set archiveApiOutputUrl=%rootOutputPath%\BackEnd\%version%\ArchivingWebAPI
) else (
   set archiveApiOutputUrl=%archiveApiSourceUrl%%localPathWithoutLocal%
)

if %choice%==1 (
    set contentProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\ContentProcessor
) else (
   set contentProcessorOutputUrl=%contentProcessorSourceUrl%%localPath%
)

if %choice%==1 (
    set carryforwardProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\CarryforwardProcessor
) else (
   set carryforwardProcessorOutputUrl=%carryforwardProcessorSourceUrl%%localPath%
)

if %choice%==1 (
    set carryforwardContentProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\CarryforwardContentProcessor
) else (
   set carryforwardContentProcessorOutputUrl=%carryforwardContentProcessorSourceUrl%%localPath%
)

if %choice%==1 (
    set carryforwardCopyEngProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\CarryforwardCopyEngProcessor
) else (
   set carryforwardCopyEngProcessorOutputUrl=%carryforwardCopyEngProcessorSourceUrl%%localPath%
)

if %choice%==1 (
    set carryforwardFileTransferProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\CarryforwardFileTransferProcessor
) else (
   set carryforwardFileTransferProcessorOutputUrl=%carryforwardFileTransferProcessorSourceUrl%%localPath%
)

if %choice%==1 (
    set houseKeeperProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\HouseKeeperProcessor
) else (
   set houseKeeperProcessorOutputUrl=%houseKeeperProcessorSourceUrl%%localPath%
)

if %choice%==1 (
    set auvenirDuplicateEngProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\Auvenir.DuplicateEngProcessor
) else (
   set auvenirDuplicateEngProcessorOutputUrl=%auvenirDuplicateEngProcessorUrl%%localPath%
)

if %choice%==1 (
    set auvenirDuplicateFileProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\Auvenir.DuplicateFileProcessor
) else (
   set auvenirDuplicateFileProcessorOutputUrl=%auvenirDuplicateFileProcessorUrl%%localPath%
)

if %choice%==1 (
    set integrationHubProcessorOutputUrl=%rootOutputPath%\BackEnd\%version%\Auvenir.IntegrationHubProcessor
) else (
   set integrationHubProcessorOutputUrl=%integrationHubProcessorUrl%%localPath%
)

if %choice%==1 (
    set integrationHubServiceOutputUrl=%rootOutputPath%\BackEnd\%version%\IntegrationHubService
) else (
   set integrationHubServiceOutputUrl=%integrationHubServiceUrl%%localPath%
)

if %choice%==1 (
    set annualSnapshotProcessorOutPutUrl=%rootOutputPath%\BackEnd\%version%\Auvenir.AnnualSnapshotProcessor
) else (
   set annualSnapshotProcessorOutPutUrl=%annualSnapshotProcessorUrl%%localPath%
)

if %choice%==1 (
    set notificationProcessorOutPutUrl=%rootOutputPath%\BackEnd\%version%\NotificationProcessor
) else (
   set notificationProcessorOutPutUrl=%notificationProcessorUrl%%localPath%
)
