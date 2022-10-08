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

### 2. Modify project's root namespace

Now you need to change the project's root namespace to reflect
your project name. For how to do it in Rider see 
[this article](https://www.jetbrains.com/help/rider/Refactorings__Adjust_Namespaces.html)
.

### 3. Setup Docker

Modify the Dockerfile so that the .dll file name matched
the project's name in the `ENTRYPOINT` directive:

`ENTRYPOINT ["dotnet", "<ProjectName>.dll"]`

Then modify the `docker-compose.yaml` file. Change services'
names, change network's name and ip.

### 4. Change the environment

Edit `.env` file to your needs.

### 5. Rename files that start with 'Cad'

## Migrations

Running migrations locally:

```
dotnet ef migrations add <migration name>
dotnet ef database update --connection "Host=127.0.0.1;Username=ubunut;Password=\!Devpassword1;Database=dbname"
```


