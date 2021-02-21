#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["btg-pqr-back.Api/btg-pqr-back.Api.csproj", "btg-pqr-back.Api/"]
RUN dotnet restore "btg-pqr-back.Api/btg-pqr-back.Api.csproj"
COPY . .
WORKDIR "/src/btg-pqr-back.Api"
RUN dotnet build "btg-pqr-back.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "btg-pqr-back.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "btg-pqr-back.Api.dll"]