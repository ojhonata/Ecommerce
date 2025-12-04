# [DreamCar](https://dreamcar.kozow.com/)
## Backend – E-commerce de Carros de Luxo

Beckend desenvolvido para uma plataforma de e-commerce especializada em **carros de luxo**, oferecendo uma API completa para cadastro, gerenciamento e venda de veículos premium.  
O sistema inclui autenticação JWT, integração com ViaCEP, organização por categorias e marcas, upload de imagens, gerenciamento de usuários e estrutura preparada para integração com frontend e serviços externos.

---

## Objetivo do Projeto

O objetivo deste backend é fornecer uma **API robusta, segura e escalável** para o e-commerce, permitindo:

- Cadastro e gerenciamento de carros de luxo  
- CRUD de categorias e marcas  
- Autenticação e autorização via JWT  
- Consulta de endereço através da API ViaCEP
- Gerenciamento de usuários e endereços  
- Upload de imagens integrado ao Cloudinary  
- Integração com MySQL via Entity Framework Core  
- Disponibilização de endpoints REST bem estruturados  
- Documentação automática com Swagger  

---

## Tecnologias Utilizadas

- **ASP.NET Core**
- **Entity Framework Core**
- **MySQL**
- **Swagger**
- **JWT Authentication**
- **AutoMapper**
- **Cloudinary**
- **ViaCEP API** (para consulta de endereços por CEP)
- **.env loader** para variáveis de ambiente

---
# Como Rodar o Projeto Localmente

## Criar o Arquivo `.env`

O projeto utiliza variáveis de ambiente carregadas a partir de um arquivo `.env` na raiz do projeto.

Crie um arquivo **`.env`** no diretório principal e adicione exatamente os seguintes campos:

```
POSTGRES_URL = Host=;Port=;Database=ecommerce;Username=;Password=

MYSQL_URL = server=; port=; database=ecommerce; user=; password=; Persist Security Info=False; Convert Zero Datetime=True

CloudName =
ApiKey =
ApiSecret =

TokenSecret =
```

Instalar dependências do projeto
```
dotnet restore
```
Rodar as migrations
Criar uma nova migration:
```
dotnet ef migrations add nome-da-migration
```

Atualizar o banco de dados:
```
dotnet ef database update
```

Rodar o projeto
```
dotnet run
```

## [FrontEnd](https://github.com/Caua94/EcommerceCar)
