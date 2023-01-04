FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
COPY . /src
WORKDIR /src

RUN dotnet build "DrivingCalendar.API.Host/DrivingCalendar.API.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DrivingCalendar.API.Host/DrivingCalendar.API.Host.csproj" -c Release -o /app/publish

FROM base AS final
RUN ls
COPY --from=publish /app/publish .