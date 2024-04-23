# rpa-mit-approvals

This repository hosts a minimal API designed designed to manage and validate invoice approval processes within the system, ensuring that only authorized and appropriate actions are taken for sensitive operations like invoice approvals.

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=rpa-mit-approvals&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=rpa-mit-approvals) [![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=rpa-mit-approvals&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=rpa-mit-approvals) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=rpa-mit-approvals&metric=coverage)](https://sonarcloud.io/summary/new_code?id=rpa-mit-approvals) [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=rpa-mit-approvals&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=rpa-mit-approvals)
## Requirements

Amend as needed for your distribution, this assumes you are using windows with WSL.
- <details>
    <summary> .NET 8 SDK </summary>
    

    #### Basic instructions for installing the .NET 8 SDK on a debian based system.
  
    Amend as needed for your distribution.

    ```bash
    wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    sudo apt-get update && sudo apt-get install -y dotnet-sdk-8.0
    ```
</details>

- <details>
    <summary> EF Core Tools </summary>
    
    ```bash
    dotnet tool install --global dotnet-ef
    ```
</details>

- [Docker](https://docs.docker.com/desktop/install/linux-install/)

---
## Create the database

Create the postgres database in docker

```bash
docker pull postgres
```

```bash
docker run --name MY_POSTGRES_DB -e POSTGRES_PASSWORD=password -p 5432:5432 -d postgres
```

---
## Local Setup

To run this service locally complete the following steps.

### Set up user secrets

Use the secrets-template to create a secrets.json in the same folder location as the [EST.MIT.Approvals.Api.csproj](https://github.com/DEFRA/rpa-mit-approvals/blob/main/EST.MIT.Approvals.Api/EST.MIT.Approvals.Api.csproj") file. 

An **example** connection template and the format to include permitted domains is provided here.

```json
{
	"AllowedEmailDomains": "defra.gov.uk|other.gov.uk",
	"DbConnectionTemplate": "Server={0};Port={1};Database={2};User Id={3};Password={4};"
}
```

Once this is done run the following command to add the projects user secrets

```bash
cat secrets.json | dotnet user-secrets set
```

These values can also be created as environment variables or as a development app settings file, but the preferred method is via user secrets.

### Apply DB migrations

We use EF Core to handle database migrations. Run the following command to update migrations on database.

**NOTE** - You will need to create the database in postgres before migrating.

```bash
dotnet ef database update
```
### Start the Api

```bash
cd RPA.MIT.ReferenceData.Api
```

```bash
dotnet run
```

---
## Running in Docker

To create the application as a docker container run the following command in the parent directory.

```bash
docker compose up
```

---

## Endpoints

### HTTP

#### Invoice Approvals

Validates an approver for a specific invoice based on approval group settings. Returns success if the approver is valid, otherwise returns an error message explaining why validation failed.

```http
POST /approvals/approver/validate
```
#### Swagger

Swagger is also available in development environments with more detailed information on the endpoints and their expected payloads.
```http
/swagger
```

