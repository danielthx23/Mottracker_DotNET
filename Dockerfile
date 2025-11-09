FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Mottracker.Domain/Mottracker.Domain.csproj", "Mottracker.Domain/"]
COPY ["Mottracker.Application/Mottracker.Application.csproj", "Mottracker.Application/"]
COPY ["Mottracker.Infrastructure/Mottracker.Infrastructure.csproj", "Mottracker.Infrastructure/"]
COPY ["Mottracker.IoC/Mottracker.IoC.csproj", "Mottracker.IoC/"]
COPY ["Mottracker.Presentation/Mottracker.Presentation.csproj", "Mottracker.Presentation/"]

RUN dotnet restore "Mottracker.Presentation/Mottracker.Presentation.csproj"

COPY . .
WORKDIR /src/Mottracker.Presentation
RUN dotnet publish "Mottracker.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime
ARG RUN_MIGRATIONS=false
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5024
ENV ASPNETCORE_ENVIRONMENT=Production
ENV RUN_MIGRATIONS=${RUN_MIGRATIONS}

# create app user and ensure tools path
RUN useradd -m appuser
ENV PATH="/home/appuser/.dotnet/tools:$PATH"
# install ef tool (if needed for runtime migrations)
RUN dotnet tool install --global dotnet-ef --version8.0.0 || true

# Copy published app and full source (needed if dotnet-ef executes against project files)
COPY --from=build /app/publish .
COPY --from=build /src ./src

# Entrypoint script to run migrations conditionally
COPY entrypoint.sh ./entrypoint.sh
USER root
RUN chmod +x ./entrypoint.sh && chown -R appuser:appuser /app
USER appuser

EXPOSE 5024

ENTRYPOINT ["/bin/sh", "-c", "if [ -f ./entrypoint.sh ]; then ./entrypoint.sh; else dotnet Mottracker.Presentation.dll; fi"]
