# Estágio 1: Build (Compilação)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Ecommerce.csproj", "./"]
RUN dotnet restore "Ecommerce.csproj"
COPY . .
RUN dotnet publish "Ecommerce.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio 2: Runtime (Execução)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Variáveis de ambiente para o Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# O ERRO ESTAVA AQUI (Corrigido para apenas um .dll)
ENTRYPOINT ["dotnet", "Ecommerce.dll"]