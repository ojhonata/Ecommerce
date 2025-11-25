# Estágio de Build
# Usa a imagem oficial do Node.js com Alpine Linux para um tamanho menor
FROM node:lts-alpine as build-stage
WORKDIR /app
# Copia apenas os arquivos de dependências primeiro para aproveitar o cache do Docker
COPY package*.json ./
RUN npm install
# Copia o restante do código
COPY . .
# Executa o build da aplicação (gerando os arquivos estáticos na pasta 'dist')
RUN npm run build

---

# Estágio de Produção (Servidor Nginx)
# Usa a imagem oficial do Nginx com Alpine Linux
FROM nginx:stable-alpine as production-stage
# Copia os artefatos de build do estágio anterior
# O caminho '/app/dist' é o resultado do 'npm run build' no estágio 'build-stage'
COPY --from=build-stage /app/dist /usr/share/nginx/html

# --- Configuração do Nginx ---
# A correção aqui é usar o seu arquivo de configuração personalizado.
# Você deve criar um arquivo 'nginx.conf' na mesma pasta do Dockerfile.
# Ele será copiado substituindo o padrão.
COPY nginx.conf /etc/nginx/conf.d/default.conf

# O Cloud Run requer que a aplicação escute na porta definida pela variável de ambiente PORT (geralmente 8080).
# O Nginx já está configurado para 8080 no arquivo 'nginx.conf' corrigido.
EXPOSE 8080

# Comando para iniciar o Nginx
CMD ["nginx", "-g", "daemon off;"]