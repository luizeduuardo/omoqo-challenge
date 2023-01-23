#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Omoqo.Challenge.Api/Omoqo.Challenge.Api.csproj", "Omoqo.Challenge.Api/"]
COPY ["Omoqo.Challenge.Api.Core/Omoqo.Challenge.Api.Core.csproj", "Omoqo.Challenge.Api.Core/"]
COPY ["Omoqo.Challenge.Api.Test/Omoqo.Challenge.Api.Test.csproj", "Omoqo.Challenge.Api.Test/"]
RUN dotnet restore "Omoqo.Challenge.Api/Omoqo.Challenge.Api.csproj"

COPY . .
WORKDIR "/src/Omoqo.Challenge.Api.Test"
RUN dotnet test --logger:trx

WORKDIR "/src/Omoqo.Challenge.Api"
RUN dotnet build "Omoqo.Challenge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Omoqo.Challenge.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Omoqo.Challenge.Api.dll"]