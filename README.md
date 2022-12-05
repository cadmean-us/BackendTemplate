# ASP.NET Backend template project

This template can be used to create a new backend project.
It provides basic directory structure as well as some useful
classes to start developing new project fast.

The template used the following technologies:

- ASP.NET Core
- Cadmean.RPC.ASP
- Cadmean.CoreKit
- EntityFramework
- Postgres database

You can change the database to any database supported by 
EntityFramework.

## How to use this template

### 1. Create new project from this template

You can use built in GitHub functionality to create a new 
project from this template repository. Alternatively you can
just clone this repo.

### 2. Rename the project

Rename the root directory of the solution to your project name.
Then rename the .sln file inside the solution directory to match
the solution directory name.

Now use the Rider's refactor option to rename the project
inside the solution.

### 3. Modify project's root namespace

Now you need to change the project's root namespace to reflect
your project name. For how to do it in Rider see 
[this article](https://www.jetbrains.com/help/rider/Refactorings__Adjust_Namespaces.html)
.

### 4. Change the environment

Edit `.env` file to your needs.

### 5. Rename files that start with 'Cad'

- Configuration/CadOptions.cs
- Controllers/CadController.cs
- Exceptions/CadException.cs

### 6. Update project properties

Edit the Docker image name:
```
<ContainerImageName>cadmean.azurecr.io/backend-template</ContainerImageName>
```

## System requirements

- .NET 7.0
- ASP.NET 7.0
- Docker with compose plugin

## Configuration

The project can use different methods of configuration:

- `appsettings.Development.json` (only for development, it's included in the VCS)
- `appsettings.Production.json` (not included in VCS)
- Environment variables

Environment variables will override settings from JSON files if the naming convention is followed: `Section__Variable`.

For details see [microsoft docs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0).

## Run the project locally

First, you will need a database for the backend to connect to.
You can start the database through docker compose.

Create a `.env` file with the following variables:
```
POSTGRES_PASSWORD=Nyd9vqADyhQB44gJ
POSTGRES_USER=ubunut
POSTGRES_DB=hazedev
```
Then run:
```
docker compose up -d database
```

Then apply migrations:
```
dotnet ef database update --connection "Host=127.0.0.1;Username=ubunut;Password=<password>;Database=<db name>" --project HazeBackend
```

Now you can run the project with your IDE or with dotnet cli (run the command in the solution direcory):
```
dotnet run --project HazeBackend
```

## Migrations

Creating new migration:
```
dotnet ef migrations add <migration name> --project HazeBackend
```

Applying migrations:
```
dotnet ef database update --connection "Host=127.0.0.1;Username=ubunut;Password=<password>;Database=<db name>" --project HazeBackend
```

## Deploy project to server

### Build the image

The following command shoud be executed in the project folder,
i.e. `cd HazeBackend` inside the solution directory.

Build beta:
```
dotnet publish --os linux --arch x64 /t:PublishContainer -c Release -p:ContainerImageTag=beta
docker push cadmean.azurecr.io/haze-backend:beta
```

Build master:
```
dotnet publish --os linux --arch x64 /t:PublishContainer -c Release -p:ContainerImageTag=master
docker push cadmean.azurecr.io/haze-backend:master
```

### Update the server

Command to update server:
```
git pull; docker compose pull; docker compose up -d haze-backend; yes | docker image prune
```
