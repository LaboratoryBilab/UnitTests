FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /src
COPY ["UnitTestApp/UnitTestApp.csproj", "UnitTestApp/"]
COPY ["UnitTestApp.Tests/UnitTestApp.Tests.csproj", "UnitTestApp.Tests/"]
RUN dotnet restore "UnitTestApp/UnitTestApp.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet build "UnitTestApp/UnitTestApp.csproj" -c Release -o /app
RUN dotnet build "UnitTestApp.Tests/UnitTestApp.Tests.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UnitTestApp/UnitTestApp.csproj" -c Release -o /app
RUN dotnet publish "UnitTestApp.Tests/UnitTestApp.Tests.csproj" -c Release -o /app

# Запуск тестов
RUN dotnet test "UnitTestApp.Tests/UnitTestApp.Tests.csproj"

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UnitTestApp.dll"]