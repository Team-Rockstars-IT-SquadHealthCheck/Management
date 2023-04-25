#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RockstarsManagementSquad/RockstarsManagementSquad.csproj", "RockstarsManagementSquad/"]
COPY ["RockstarsManagementSquadLibrary/RockstarsManagementSquadLibrary.csproj", "RockstarsManagementSquadLibrary/"]
RUN dotnet restore "RockstarsManagementSquad/RockstarsManagementSquad.csproj"
COPY . .
WORKDIR "/src/RockstarsManagementSquad"
RUN dotnet build "RockstarsManagementSquad.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RockstarsManagementSquad.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RockstarsManagementSquad.dll"]