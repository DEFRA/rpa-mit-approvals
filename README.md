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

