# Base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o .csproj e restaura dependências
COPY src/TodoListApi/TodoListApi.csproj .
RUN dotnet restore TodoListApi.csproj

# Copia o resto dos arquivos do projeto
COPY src/TodoListApi/. .

# Compila o projeto
RUN dotnet build TodoListApi.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publica o projeto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish TodoListApi.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final de execucao
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "TodoListApi.dll"]