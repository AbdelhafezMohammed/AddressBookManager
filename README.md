# AddressBook

# How to run the solution:

 **Start backend using Dotnet CLI:**
 
 1- Navigate to the solution root folder
 
 2- Using Terminal or CMD run the command `dotnet restore` to retore nuget packages
 
 3- Navigate to src/backend/Addressbook/Addressbook.API
 
 4- Open Terminal or CMD run the command `dotnet run`

 **Start backend using  Visual Studio:**
 
 1-Open the solution file using Visual Studio
 
 2-Build the solution that will automatically restore Nuget packages
 
 3-Run the project using F5 or from Solution Explorer right click on Addressbook.API >Debug>Start New Instance

**Database Migrations**
Database migrations and seed data will run automatically when application starts however to run migrations manually navigate to path src/backend/Addressbook/Addressbook.API and open Terminal or CMD and run command
`dotnet ef database update
`

 **Install Database:**
 
 - using docker
   
   
 1- Navigate to the solution root folder
 
 2-  Using the terminal or cmd run command `docker compose up`
 
 
 -Local SQL Server
 
1- Install SQL server locally and set password for sql server `sa` user from `SqlServerConnectionString` in appsettings.json in Addressbook.API


 **How to test Requirements:**
 
- Use your browser to navigate https://localhost:7170/swagger/index.html for API documentation 


# Technology stack

Backend: C#, Asp.Net WebAPI, Entity Framework Core, MS SQL Server


# Tooling

VSCode, Visual Studio

MS SQL Server

Docker

Dotnet SDK V 8.0.203

Dotnet CLI


# Out of scope

Support CIDR Notation

