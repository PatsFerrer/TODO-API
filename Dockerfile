# Base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# CORRECAO: Adicionar o caminho completo a partir da raiz
COPY src/TodoListApi/TodoListApi.csproj .
RUN dotnet restore TodoListApi.csproj

# CORRECAO: Copiar o conteudo da pasta da API
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

# Copia os scripts SQL para a imagem final
COPY --from=build /src/Data ./Data

ENTRYPOINT ["dotnet", "TodoListApi.dll"]