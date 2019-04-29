# dotnet-azure

[![Build Status](https://dev.azure.com/shayneboyer/dotnet-azure/_apis/build/status/spboyer.dotnet-azure?branchName=master)](https://dev.azure.com/shayneboyer/dotnet-azure/_build/latest?definitionId=3&branchName=master)

.NET Core global tool for creating and updating web applications in Azure.

## Requirements

* .NET Core 2.2 or higher
* Azure Account - [Get one for FREE](https://aka.ms/dotnet-azure)

## Installation

```console
dotnet tool install dotnet-azure --global
```

## Usage

### help

```console
Usage: dotnet-azure [options] [command]

Options:
  -?|-h|--help  Show help information

Commands:
  deploy        Deploy application to Azure App Service. Options are used for [NEW] application deployments only.
  login         Login into Azure
```

### login

Run `login` command first to authenticate against your Azure account.

`dotnet azure login`

```console
To sign in, use a web browser to open the page https://microsoft.com/devicelogin and enter the code XXXXXXXXX to authenticate.
```

### deploy

Use defaults, random generated application and resource group name. Current directory is assumed for application for deployment.

```console
dotnet azure deploy
```

Pass project folder.

```console
dotnet azure deploy \mynewproject
```

Pass all options.

```console
dotnet azure deploy --location westus --name fancywebapp --group fancywebgroup --plan BasicB1
```

Short hand for `location`, `name`, and `group`

```console
dotnet azure deploy -l eastus2 -n fancywebapp -g fancygroup
```

Full help output for **deploy** command.

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