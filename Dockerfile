# Estágio de Compilação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o projeto e restaura (mais rápido para cache)
COPY ["ShopGestProjeto.Api.csproj", "./"]
RUN dotnet restore "ShopGestProjeto.Api.csproj"

# Copia o resto dos arquivos e compila
COPY . .
RUN dotnet publish "ShopGestProjeto.Api.csproj" -c Release -o /app/publish

# Estágio Final de Execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Garante permissão de escrita para o SQLite criar o banco
USER root
RUN chmod -R 777 /app

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "ShopGestProjeto.Api.dll"]
