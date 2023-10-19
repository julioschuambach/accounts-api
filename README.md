# accounts-api
 
## Introduction
This project consists of a ASP.NET API, for account registration and management.

## Requirements
> .NET 6

## How to use
### Restoring the project
`dotnet restore`

### Building the project
`dotnet build`

### Running the project
`dotnet run`

After running the project, you can access the API by the following link: `http://localhost:5236`

## Endpoints
**Post**<br>
`/signup` - Used for signing up<br>
`/signin` - Used for signing in 

**Get**<br>
`/` - Used for listing all registered accounts (requires administrator role)
