FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# install nodejs for angular, webpack middleware
# RUN apt-get update  
# RUN apt-get -f install  
# RUN apt-get install -y wget  
# RUN wget -qO- https://deb.nodesource.com/setup_11.x | bash -  
# RUN apt-get install -y build-essential nodejs

WORKDIR /API
COPY *.csproj ./
RUN dotnet restore API.csproj
COPY . ./
RUN dotnet publish API.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS runtime
WORKDIR /API
COPY — from=build /API/out ./
ENTRYPOINT [“dotnet”, “dockerapi.dll”]`