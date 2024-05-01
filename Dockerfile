FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development

RUN mkdir -p /home/dotnet/EST.MIT.Approvals.Api/ /home/dotnet/EST.MIT.Approvals.Api.Tests/ /home/dotnet/EST.MIT.Approvals.Data/

COPY --chown=dotnet:dotnet ./EST.MIT.Approvals.Data/*.csproj ./EST.MIT.Approvals.Data/
RUN dotnet restore ./EST.MIT.Approvals.Data/EST.MIT.Approvals.Data.csproj

COPY --chown=dotnet:dotnet ./EST.MIT.Approvals.Api/*.csproj ./EST.MIT.Approvals.Api/
RUN dotnet restore ./EST.MIT.Approvals.Api/EST.MIT.Approvals.Api.csproj

COPY --chown=dotnet:dotnet ./EST.MIT.Approvals.Api.Tests/*.csproj ./EST.MIT.Approvals.Api.Tests/
RUN dotnet restore ./EST.MIT.Approvals.Api.Tests/EST.MIT.Approvals.Api.Tests.csproj

COPY --chown=dotnet:dotnet ./EST.MIT.Approvals.Api/ ./EST.MIT.Approvals.Api/
COPY --chown=dotnet:dotnet ./EST.MIT.Approvals.Data/ ./EST.MIT.Approvals.Data/
COPY --chown=dotnet:dotnet ./EST.MIT.Approvals.Api.Tests/ ./EST.MIT.Approvals.Api.Tests/

RUN dotnet publish ./EST.MIT.Approvals.Api/ -c Release -o /home/dotnet/out

ARG PORT=3000
ENV PORT ${PORT}
EXPOSE ${PORT}

CMD dotnet watch --project ./EST.MIT.Approvals.Api run --urls "http://*:${PORT}"

# Production
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS production

ARG PORT=3000
ENV ASPNETCORE_URLS=http://*:${PORT}
EXPOSE ${PORT}

COPY --from=development /home/dotnet/out/ ./

CMD dotnet EST.MIT.Approvals.Api.dll