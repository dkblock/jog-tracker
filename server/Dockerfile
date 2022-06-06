#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/JogTracker.Api/JogTracker.Api.csproj", "src/JogTracker.Api/"]
COPY ["src/JogTracker.Common/JogTracker.Common.csproj", "src/JogTracker.Common/"]
COPY ["src/JogTracker.Mappers/JogTracker.Mappers.csproj", "src/JogTracker.Mappers/"]
COPY ["src/JogTracker.Models/JogTracker.Models.csproj", "src/JogTracker.Models/"]
COPY ["src/JogTracker.Entities/JogTracker.Entities.csproj", "src/JogTracker.Entities/"]
COPY ["src/JogTracker.Configuration/JogTracker.Configuration.csproj", "src/JogTracker.Configuration/"]
COPY ["src/JogTracker.Repository/JogTracker.Repository.csproj", "src/JogTracker.Repository/"]
COPY ["src/JogTracker.Database/JogTracker.Database.csproj", "src/JogTracker.Database/"]
COPY ["src/JogTracker.Identity/JogTracker.Identity.csproj", "src/JogTracker.Identity/"]
RUN dotnet restore "src/JogTracker.Api/JogTracker.Api.csproj"
COPY . .
WORKDIR "/src/src/JogTracker.Api"
RUN dotnet build "JogTracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JogTracker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JogTracker.Api.dll"]