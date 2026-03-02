# ── Build stage ───────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


# Copy project files for layer caching
COPY src/TodoApi.Domain/TodoApi.Domain.csproj           src/TodoApi.Domain/
COPY src/TodoApi.Application/TodoApi.Application.csproj src/TodoApi.Application/
COPY src/TodoApi.Infrastructure/TodoApi.Infrastructure.csproj src/TodoApi.Infrastructure/
COPY src/TodoApi.API/TodoApi.API.csproj                 src/TodoApi.API/


RUN dotnet restore src/TodoApi.API/TodoApi.API.csproj


COPY . .
RUN dotnet publish src/TodoApi.API/TodoApi.API.csproj -c Release -o /app/publish


# ── Runtime stage ─────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app


# Non-root user for security
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser


COPY --from=build /app/publish .


EXPOSE 8080
ENTRYPOINT ["dotnet", "TodoApi.API.dll"]
