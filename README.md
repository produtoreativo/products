
# Products API

## Requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Configuração de Ambiente

### Executar o Docker Compose

Inicie os serviços do Docker (SQL Server e Elasticsearch) usando o `docker-compose`:

```bash
docker-compose up -d
```

### Restaurar as Dependências e Executar Migrações

Restaurar as dependências do projeto e aplicar as migrações para configurar o banco de dados:

```bash
dotnet restore
dotnet ef database update
```

### Executar a Aplicação

Execute a aplicação localmente:

```bash
dotnet run
```

### Acessar a Documentação da API

Abra o navegador e acesse a URL do Swagger para visualizar e testar os endpoints da API:

```
http://localhost:7001/
```

## Endpoints da API

- `GET /api/produtos` - Obter todos os produtos
- `GET /api/produtos/{id}` - Obter um produto específico pelo ID
- `POST /api/produtos` - Criar um novo produto
- `PUT /api/produtos/{id}` - Atualizar um produto existente
- `DELETE /api/produtos/{id}` - Excluir um produto
