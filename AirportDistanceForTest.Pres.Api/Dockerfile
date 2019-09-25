FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["AirportDistanceForTest.Pres.Api/AirportDistanceForTest.Pres.Api.csproj", "AirportDistanceForTest.Pres.Api/"]
COPY ["AirportDistanceForTest.Domain/AirportDistanceForTest.Domain.csproj", "AirportDistanceForTest.Domain/"]
COPY ["AirportDistanceForTest.Infra.Data/AirportDistanceForTest.Infra.Data.csproj", "AirportDistanceForTest.Infra.Data/"]
COPY ["AirportDistanceForTest.Application/AirportDistanceForTest.Application.csproj", "AirportDistanceForTest.Application/"]
RUN dotnet restore "AirportDistanceForTest.Pres.Api/AirportDistanceForTest.Pres.Api.csproj"
COPY . .
WORKDIR "/src/AirportDistanceForTest.Pres.Api"
RUN dotnet build "AirportDistanceForTest.Pres.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AirportDistanceForTest.Pres.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AirportDistanceForTest.Pres.Api.dll"]