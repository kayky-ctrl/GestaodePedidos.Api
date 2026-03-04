# ⚙️ ShopGest API - Sistema de Gestão de Pedidos

Esta é a API de alta performance do ecossistema **ShopGest**, desenvolvida em **ASP.NET Core**. Ela é responsável pelo processamento de regras de negócio, persistência de dados e segurança de informações sensíveis de clientes e vendas.

---

## 📌 Menu de Navegação
* [🛠️ Tecnologias e Frameworks](#️-tecnologias-e-frameworks)
* [🔐 Segurança e Regras de Negócio](#-segurança-e-regras-de-negócio)
* [🛣️ Endpoints da API](#️-endpoints-da-api)
* [📂 Estrutura do Projeto](#-estrutura-do-projeto)
* [🗄️ Banco de Dados e Persistência](#️-banco-de-dados-e-persistência)
* [🚀 Como Executar Localmente](#-como-executar-o-projeto-localmente)
* [☁️ Deploy em Produção](#️-deploy-em-produção)

---

## 🛠️ Tecnologias e Frameworks

* **ASP.NET Core Web API:** Framework principal para construção dos serviços.
* **Entity Framework Core:** ORM utilizado para comunicação e mapeamento do banco de dados.
* **C# / .NET:** Linguagem e plataforma base para o desenvolvimento.
* **SHA-256 Hashing:** Implementação de segurança para proteção de dados sensíveis.
* **Injeção de Dependência:** Arquitetura desacoplada utilizando interfaces para serviços.

## 🔐 Segurança e Regras de Negócio

* **Criptografia de Cartões:** A API utiliza o `HashService` para converter números de cartão de crédito em hashes SHA-256 antes de salvar no banco de dados.
* **Integridade de Dados:** O sistema utiliza `IHashService` para garantir que informações críticas não sejam armazenadas em texto plano.
* **Fluxo de Pedidos:** A criação de pedidos força automaticamente o status inicial como `AguardandoPagamento` e registra a `DataCompra` via servidor.
* **Exposição Seletiva:** Uso de DTOs e Projeções para garantir que apenas dados necessários (como nome e email) sejam enviados ao front-end, ocultando hashes de segurança.

## 🛣️ Endpoints da API

### Clientes (`/api/Clientes`)
* `GET /`: Retorna a lista de clientes cadastrados (ID, Nome, CPF, Email e Score).
* `GET /{id}`: Busca um cliente específico por seu identificador único.
* `POST /Criar-cliente`: Registra um novo cliente, processando o hash do cartão de crédito no ato do cadastro.

### Pedidos (`/api/pedidos`)
* `GET /`: Lista todos os pedidos realizados com detalhes do cliente vinculado.
* `GET /entregador`: Endpoint otimizado que filtra apenas pedidos com status `EmTransito`.
* `POST /criar-pedido`: Registra uma nova venda vinculada a um `ClienteId`.

## 📂 Estrutura do Projeto

```text
├── Controllers
│   ├── ClientesController.cs  # Endpoints de busca e cadastro de clientes
│   └── PedidosController.cs   # Endpoints de fluxo de vendas e entregas
├── Services
│   └── Security
│       ├── IHashService.cs    # Interface de contrato para segurança
│       └── HashService.cs     # Implementação de Hashing SHA-256
├── Models                     # Entidades do Banco de Dados e DTOs
└── Data                       # Contexto do Entity Framework (AppDbContext)
```
## 🗄️ Banco de Dados e Persistência

O projeto utiliza o **SQLite** como motor de banco de dados, o que torna a aplicação leve, portátil e de fácil configuração para ambientes de teste e produção rápida.

* **Arquivo do Banco:** As informações são armazenadas em um arquivo físico chamado `shopgest.db`.
* **Localização (Local):** Ao executar o projeto em sua máquina (via `dotnet run`), o arquivo será criado automaticamente na raiz da pasta do projeto API.
* **Localização (Docker/Render):** Dentro do container Docker, o arquivo reside na pasta `/app/shopgest.db`.
* **Migrações:** O esquema das tabelas é gerenciado pelo **Entity Framework Core**. Graças ao comando `context.Database.EnsureCreated()` no `Program.cs`, o banco de dados e todas as tabelas (Clientes e Pedidos) são gerados automaticamente assim que a API é iniciada pela primeira vez, eliminando a necessidade de configurações manuais de servidor.



> **Nota sobre Persistência na Render:** Como a Render utiliza sistemas de arquivos efêmeros em planos gratuitos, o arquivo `.db` é resetado sempre que o serviço é reiniciado ou um novo deploy é feito. Para uso em produção real com retenção de dados a longo prazo, recomenda-se o uso de um "Disk Storage" montado no container ou a migração para PostgreSQL.

---

## 🚀 Como Executar o Projeto Localmente

Como este projeto utiliza **Docker** e **SQLite**, você não precisa configurar um servidor de banco de dados pesado na sua máquina.

### Pré-requisitos
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (opcional, se não usar Docker)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (recomendado)

### 📥 Clonando o Repositório
```bash
git clone https://github.com/kayky-ctrl/GestaodePedidos.Api.git
cd GestaodePedidos.Api
```

### 🐳 Rodando com Docker (Mais fácil)
O Docker garantirá que a API rode exatamente como está em produção.

**Construir a imagem:**
```bash
docker build -t shopgest-api .
```
### Executar o container:

```Bash
docker run -p 5000:80 shopgest-api
```
Acesse: http://localhost:5000/swagger

## 💻 Rodando sem Docker (Manual)
### Restaurar dependências:

```Bash
dotnet restore
```
### Atualizar o Banco de Dados:
A API está configurada para criar o arquivo shopgest.db automaticamente no primeiro acesso através do comando
```
context.Database.EnsureCreated(). Caso prefira rodar via CLI:
```

```Bash
dotnet ef database update
```
### Executar a aplicação:

``` Bash
dotnet run
```
## ☁️ Deploy em Produção

A API está hospedada na **Render** e configurada para *Continuous Deployment* via GitHub.

* **URL Base:** [https://gestaodepedidos-api.onrender.com](https://gestaodepedidos-api.onrender.com)
* **Banco de Dados:** SQLite (persistido dentro do container).
* **CORS:** Liberado para todas as origens para facilitar a integração com o frontend.

---

## 🛠️ Configurações Importantes de Infraestrutura

* **Dockerfile:** Utiliza imagens base Linux (Debian) para garantir alta compatibilidade com ambientes Cloud.
* **Auto-Init:** A API possui lógica de inicialização automática de banco de dados no `Program.cs`, dispensando a necessidade de rodar migrations manuais no servidor (utiliza o método `context.Database.EnsureCreated()`).

---
