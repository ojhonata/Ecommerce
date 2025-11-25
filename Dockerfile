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

# Variáveis de ambiente
# ASPNETCORE_URLS define onde o Kestrel deve escutar.
# Usar a porta 8080 como fallback, mas o Render geralmente injeta sua própria porta via variável PORT.
# A porta é definida no ENTRYPOINT para ser flexível.
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# 🚀 CORREÇÃO AQUI: Usar a variável de ambiente $PORT do Render no comando
# O Render injeta a variável $PORT. O Kestrel escuta na porta definida por essa variável.
ENTRYPOINT ["dotnet", "Ecommerce.dll", "--urls", "http://0.0.0.0:$PORT"]