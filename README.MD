# Meter Reading Importer

## Background
.Net Core (Framework 7.0) WebApi for uploading Meter Readings into an SQL Data using Dapper.

WebApi Validate data sent to ensure it is valid and can be imported correctly


## Setup
In order to run the WebApi you will need to source SQL Server Connection details and update MeterReadingWebAPI appsettings.json.   Ideally two accounts will be needed, one account will be to allow data migrations and will need access to dbo.master database, the other will be an account that can access the database to be created using the inbuilt migrations

From within Visual Studio RUn the application, whilst in a developer environment this will load a (OpenApi) Swagger Document.   From this UI you can select and upload a MeterReading CSV file.