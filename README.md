# Approvals Web Service

## Introduction 
This repo contains the MIT Approvals Api.

## Repo status

| Name                                              |  Status|
|---------------------------------------------------|---------------------|
|[Build]() | [![Build Status](https://dev.azure.com/defragovuk/DEFRA-EST/_apis/build/status/EST.MIT.Approvals?branchName=main)](https://dev.azure.com/defragovuk/DEFRA-EST/_build/latest?definitionId=3703&branchName=main) |
|[SonarCloud](https://sonarcloud.io/project/overview?id=EST.MIT.Approvals)| [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=EST.MIT.Approvals&metric=alert_status&token=e40e1dada8517104fe7cb280a5a9de6b558a717c)](https://sonarcloud.io/summary/new_code?id=EST.MIT.Approvals) |


## Running Application
### Requirements
* Git
* .NET 6 SDK
* PostgreSQL
* **Optional:** Docker - Only needed if running PostgreSQL within container

### PostgreSQL
Execute the following commands to run Postgres inside a docker container:
```ps
docker pull postgres
docker run --name approvals-postgres -p 127.0.0.1:5432:5432 -e POSTGRES_PASSWORD=approvalspassword -d est_mit_approvals
```

Or install a standalone instance using the following link:

[PostgreSQL: Windows installers](https://www.postgresql.org/download/windows/)

### EF Core Tools
Follow this guide to install EF Core global tools:

[Entity Framework Core tools reference - .NET Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Secret Configuration
This project uses dotnet user-secrets to store connection strings.

Use the following commands to init user-secrets in the project:

```ps
dotnet user-secrets init --project EST.MIT.Approvals.Api
```

Use the following command to set secrets:

```ps
dotnet user-secrets set "<Secret name>" "<Secret>"
```

The following secrets need to be set in the EST.MIT.Approvals.Api project:
|Secret|Description|
|------|-----------|
|DbConnectionString|PostgreSQL connection string|

The database connection should connect to a database called `est_mit_approvals`.

#### Example Postgres Connection String
```
Server=127.0.0.1;Port=5432;Database=est_mit_approvals;User Id=<UserID>;Password=<Password>;
```

### Setup Database
This project uses EF Core to handle database migrations. Run the following command to update migrations on database.

```ps
dotnet ef database update --project .\EST.MIT.Approvals.Api
```

#### Seeding Reference Data
**Important**: The seed ref data function is a destructive process which deletes all rows in database before running seed operation. For this reason, the `--seed-ref-data` argument will only run in a dev environment.

Reference data can be seeded to the database at application startup by using the `--seed-ref-data` argument.

The source of the seed data is currently from 2022-23 Delegated Authorities Register v1.3 Blank CF. Any updates made to the Excel file after this version may not be reflected in the seed data.

### Starting Api
```ps
cd EST.MIT.Approvals.Api
dotnet run
```

Endpoints are accessible at https://localhost:7258.
Swagger page is accessible at https://localhost:7258/swagger/index.html.

## Endpoints

### Parameters
The following querystring parameters are available when calling the API (also visible on swagger page)

invoiceScheme (string):
* A1
* B2
* C3

invoiceAmount (decimal):
* 1000
* 5000
* 10000

The following combinations will yield results
* invoiceScheme=A1&invoiceAmount=1000


### GET Endpoints

`GET /approvals/invoiceapprovers?invoiceScheme={invoiceScheme}&invoiceAmount={invoiceAmount}`

Retrieves a list of valid approvals codes for the given invoice scheme and invoice amount path.

200 Response Example

```json
[
  {
    "id": 1,
    "emailAddress": "ApproverOne@defra.gov.uk",
    "firstName": "Approver",
    "lastName": "One,"
  }
]
```


`GET /approvals/healthcheck/ping`

Retrieves a status message.

200 Response Example

```json
"Approvals.Api Endpoint up and running @22/05/2023 06:49:18"
```

