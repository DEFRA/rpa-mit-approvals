ARG PARENT_VERSION=1.5.0-dotnet6.0

# Development
FROM defradigital/dotnetcore-development:$PARENT_VERSION AS development

ARG PARENT_VERSION
ARG PACKAGE_FEED_URL
ARG PACKAGE_FEED_USERNAME
ARG PACKAGE_FEED_PAT

LABEL uk.gov.defra.parent-image=defra-dotnetcore-development:${PARENT_VERSION}

RUN mkdir -p /home/dotnet/EST.MIT.Approvals.Api/ /home/dotnet/EST.MIT.Approvals.Api.Tests/ /home/dotnet/EST.MIT.Approvals.Data/

COPY --chown=dotnet:dotnet ./docker-nuget.config ./nuget.config

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
FROM defradigital/dotnetcore:$PARENT_VERSION AS production

ARG PARENT_VERSION
ARG PARENT_REGISTRY

LABEL uk.gov.defra.parent-image=defra-dotnetcore-development:${PARENT_VERSION}

ARG PORT=3000
ENV ASPNETCORE_URLS=http://*:${PORT}
EXPOSE ${PORT}

COPY --from=development /home/dotnet/out/ ./

CMD dotnet EST.MIT.Approvals.Api.dll