#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Folio3.DotNet.Sbp.Api/Folio3.DotNet.Sbp.Api.csproj", "Folio3.DotNet.Sbp.Api/"]
RUN dotnet restore "Folio3.DotNet.Sbp.Api/Folio3.DotNet.Sbp.Api.csproj"
COPY . .
WORKDIR "/src/Folio3.DotNet.Sbp.Api"
RUN dotnet build "Folio3.DotNet.Sbp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Folio3.DotNet.Sbp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Folio3.DotNet.Sbp.Api.dll"]