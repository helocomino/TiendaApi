# 1. Etapa de compilación (SDK de .NET)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar archivos del proyecto y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos y compilar
COPY . ./
RUN dotnet publish -c Release -o out

# 2. Etapa de ejecución (Runtime de .NET)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Exponer el puerto que usa Render por defecto
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Comando para iniciar la API
ENTRYPOINT ["dotnet", "TiendaApi.dll"]