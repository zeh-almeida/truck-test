# Truck Test
Welcome to the Truck Test.

## Introduction

This Solution aims to showcase my abilities regarding Full-Stack development with a focus on Back-End.
The project has been developed using Angular and ASP.Net Core on top of a Docker infrastructure.

## Setting up

### Software

In order to run this project you must:

- Install [.Net Core 3.1 SDK](https://dotnet.microsoft.com/download)
- Install [Node.js LTS](https://nodejs.org/en/download/) 
- Install [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Environment variables

You must set up the following variables:

- `DB_PASSWORD` for the `SQL Server` user. The same password will be used to setup both the API and Database containers.
  The system has a fall back password if none is set, but you really should use a better one. 
  - This password must adhere to the [SQL Server Password Strength](https://docs.microsoft.com/en-us/sql/relational-databases/policy-based-management/sql-server-login-password-strength?view=sql-server-ver15)

## Running

Once the project is cloned, the following commands must be run:

- `dotnet restore` at the project root to ensure all `Nuget` packages are downloaded
- `npm install` at the `trucks-test.api\ClientApp` folder in order to install all `javascript` packages

The application is set up in a way that all database migrations will run when the server starts.

### Running through `docker-compose`
Once the environment variables are set, just run the ```docker-compose up```  command.

Docker will spin the Server and Database containers for you:

- The application should be available at `localhost:8443`.
  - Also, there is a `Swagger` page at `localhost:8443/api`
  - Because of the Angular CLI used by the .Net environment, is possible the web page displays an error when running the server. A simple refresh or a `F5` will force the server to reload the page and display correctly. This is due a non-configurable timeout during the server start up phase. 
- A local instance of SQL Server should start at  `localhost:1443`. The user is set as `SA` and the password is the `DB_PASSWORD ` environment variable.

