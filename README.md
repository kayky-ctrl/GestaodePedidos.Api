# ⚙️ ShopGest API - Sistema de Gestão de Pedidos

Esta é a API de alta performance do ecossistema **ShopGest**, desenvolvida em **ASP.NET Core**. Ela é responsável pelo processamento de regras de negócio, persistência de dados e segurança de informações sensíveis de clientes e vendas.

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
## 🚀 Como Executar
Certifique-se de ter o SDK do .NET instalado em sua máquina.

Clone o repositório para o seu ambiente local.

Configure sua String de Conexão no arquivo appsettings.json.

Execute as migrações para criar o banco de dados:

```Bash
dotnet ef database update
```
Inicie o servidor:
```Bash
dotnet run
```

ShopGest API - Garantindo segurança e agilidade no processamento de dados.
