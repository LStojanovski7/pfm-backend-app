FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build

# install nodejs for angular, webpack middleware
RUN apt-get update  
RUN apt-get -f install  
RUN apt-get install -y wget  
RUN wget -qO- https://deb.nodesource.com/setup_11.x | bash -  
RUN apt-get install -y build-essential nodejs

WORKDIR /API
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
WORKDIR /API
COPY --from=build-env /API/out .
ENTRYPOINT ["dotnet", "Web.dll"]