# dotnet-azure

.NET Core global tool for creating and updating web applications in Azure.

`dotnet azure login`


### help

```console
Usage: dotnet-azure [options] [command]

Options:
  -?|-h|--help  Show help information

Commands:
  deploy        Deploy application to Azure App Service. Options are used for [NEW] application deployments only.
  get-cli       Download and install the Azure CLI
  login         Login into Azure
```

`dotnet azure deploy`

`dotnet azure deploy <PROJECT FOLDER PATH>`

### deploy command options

```console
Deploy application to Azure App Service. Options are used for [NEW] application deployments only.

Usage: dotnet-azure deploy [arguments] [options]

Arguments:
  AppPath

Options:
  -?|-h|--help                       Show help information
  -n|--name <APP_NAME>               Name of application, must be unique.
  -l|--location <LOCATION>           Region or location of app deployment. (eastus, westus, etc.)
  -g|--group <RESOURCE_GROUP>        Resource group name to create and use for deployment.
  -p|--plan <APP_SERVICE_PLAN_TYPE>  Type of App Service Plan to create for application. Options (BasicB1, SharedD1, FreeF1, PremiumP1 - more info https://aka.ms/azure-appserviceplans )
```

## Requirements

* .NET Core 2.1 or higher
* Azure Account - [Get one for FREE](https://aka.ms/dotnet-azure)

## Installation

```console
dotnet tool install dotnet-azure --global
```

### Notes

* Creates random application and resource group name
* Creates a D1 Application Service Plan