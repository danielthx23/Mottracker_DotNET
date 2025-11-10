# Mottracker

## Descrição do Projeto

Projeto desenvolvido para o **Challenge da empresa Mottu**, FIAP 2025.

O projeto busca resolver o problema de organização e localização das motos nos pátios da Mottu, facilitando o monitoramento em tempo real. A solução utiliza câmeras com sensores de posicionamento para capturar a localização exata de cada moto. Cada moto possui um **QR Code exclusivo**, assim como os pátios, permitindo identificação e rastreamento eficiente.

A **API** integra os dados capturados pelos dispositivos IoT com uma estrutura de armazenamento adequada:

* **Banco NoSQL**: dados de IoT em tempo real.
* **Banco relacional**: dados estruturados (usuários, motos, contratos, pátios etc).

Essa integração permite o acompanhamento via aplicativo, promovendo **eficiência, segurança e organização**. O sistema também gerencia cadastro/edição de dados, autenticação/autorizacão, gestão de permissões e dashboards com relatórios para tomada de decisão.

## Estrutura com Solution e Projects

O repositório agora usa uma solution (.sln) com projetos separados por responsabilidade. Isso facilita o trabalho em IDEs (Visual Studio / Rider) e integrações CI/CD.

Estrutura de arquivos relevante:

```
Mottracker.sln # Solution na raiz do repositório
Mottracker.Presentation/ # Projeto Web (Razor Pages / API)
Mottracker.Application/ # Caso de uso, serviços de aplicação
Mottracker.Domain/ # Entidades e contratos de domínio
Mottracker.Infrastructure/ # Implementações de persistência e integrações
Mottracker.IoC/ # Injeção de dependências / composition root
Mottracker.Tests/ # Projetos de testes (unit e integration)
Dockerfile # Dockerfile usado pela pipeline / deploy
entrypoint.sh # Script de entrypoint para migrações e start
azure-pipelines.yml # Pipeline CI/CD em YAML (Azure DevOps)
README.md # Documentação (este arquivo)
```

Como abrir e rodar na sua máquina (IDE)

1. Abra `Mottracker.sln` com Visual Studio ou Rider.
2. Restaure os pacotes NuGet (IDE faz automaticamente ou `dotnet restore`).
3. Defina `Mottracker.Presentation` como projeto de inicialização.
4. Configure `appsettings.Development.json` com sua connection string (ou use variáveis de ambiente).
5. Execute o projeto (F5 / Run).

Build e testes via CLI

```bash
# Na raiz do repositório
dotnet restore
dotnet build
dotnet test
```

Notas sobre CI/CD e Docker

- A pipeline `azure-pipelines.yml` foi atualizada para trabalhar com a solution e os projetos separados. Ela realiza build, publisha artefato, executa testes e faz deploy.
- O `Dockerfile` na raiz foi ajustado para incluir opção de executar migrações via variável `RUN_MIGRATIONS` ou no pipeline (job de migrations separado).

Variáveis importantes para Azure DevOps (configure na aba Variables no portal):

- `DB_CONNECTION` (secret) — connection string do banco.
- `runMigrations` — true/false para habilitar o stage de migrações.
- `containerRegistry`, `containerRegistryServiceConnection`, `azureSubscription`, `webAppName`, `webAppResourceGroup` — para build/push e deploy.

Se precisar que eu gere ou atualize o `Mottracker.sln` (ou mova projetos para `src/` e `tests/`), diga qual opção prefere e eu aplico a mudança.

---

## Autores

* Daniel Saburo Akiyama - RM558263
* Danilo Correia e Silva - RM557540
* João Pedro Rodrigues da Costa - RM558199

## DevOps / Pipeline e Docker (alterações recentes)

Resumo das alterações feitas para CI/CD e migrações:

- O `Dockerfile` foi atualizado para permitir executar migrações condicionalmente.
 - Adicionado `ARG RUN_MIGRATIONS` e `ENV RUN_MIGRATIONS`.
 - O `entrypoint.sh` verifica `RUN_MIGRATIONS` e, se `true`, executa o comando EF:
 `dotnet ef database update --project ./src/Mottracker.Infrastructure/Mottracker.Infrastructure.csproj --startup-project ./src/Mottracker.Presentation/Mottracker.Presentation.csproj`.
 - O Dockerfile atual copia o código fonte e instala `dotnet-ef` para permitir migrações a partir do container (desenvolvimento). Em produção é recomendável usar `aspnet` image e executar migrações fora do runtime image.

- A pipeline do Azure DevOps (`azure-pipelines.yml`) foi atualizada para suportar:
 - Build da imagem Docker e push para um registry (opcional).
 - Stage separado `Migrations` que executa `dotnet-ef` no agente (instala a ferramenta e executa `dotnet ef database update`) quando a variável `runMigrations` = `true`.
 - Publicação de artefatos e execução de testes (CI).
 - Deploy para Azure Web App apontando para a imagem Docker gerada (CD).

Variáveis importantes a configurar no Azure DevOps (UI -> Pipelines -> Variables):

- `DB_CONNECTION` (secret) — connection string do banco, usada pelo EF como `ConnectionStrings__Default`.
- `runMigrations` — true/false para habilitar o stage de migrações.
- `containerRegistry` — endereço do registry (ex.: `myacr.azurecr.io`).
- `containerRegistryServiceConnection` — nome do Service Connection configurado no projeto Azure DevOps.
- `azureSubscription` — nome da Service Connection do Azure (ARM).
- `webAppName`, `webAppResourceGroup` — para o passo de deploy.

Recomendações para migrações

- Local (Docker):
 - Build da imagem:

```bash
# Na raiz do repositório
docker build -f Dockerfile -t mottracker:local .
```

 - Rodar container e aplicar migrations (exemplo PowerShell):

```powershell
docker run --rm `
 -e RUN_MIGRATIONS=true `
 -e ConnectionStrings__Default="Server=...;Database=...;User Id=...;Password=...;" `
 -p5024:5024 `
 mottracker:local
```

- Pipeline (sem Docker run):
 - A pipeline instala `dotnet-ef` no agente e roda `dotnet ef database update` usando `DB_CONNECTION` (mais simples para CI).

Observações

- Garanta que o banco esteja acessível a partir do agente (ou do host onde o container roda) e que as regras de firewall/NSG permitam conexões.
- Marque `DB_CONNECTION` como variável secreta no Azure DevOps.
- Em produção, a imagem final idealmente deve ser baseada em `mcr.microsoft.com/dotnet/aspnet:8.0` e não conter o SDK; migrações podem ser rodadas em job separado ou em um container temporário com SDK.

## Dockerfile

Dockerfile de produção (local):
- `Dockerfile` (raiz do projeto / Mottracker)

## Instalação do Projeto via Docker (Entrega DevOps)

### Requisitos
- Docker instalado e com a engine ligada

### Comandos de exemplo

Rodar com migrations (exemplo):

```bash
docker build -f Dockerfile -t youruser/mottracker:dev .

docker run -d \
 -e ConnectionStrings__Default="Server=...;Database=...;User Id=...;Password=...;" \
 -e RUN_MIGRATIONS=true \
 -e ASPNETCORE_ENVIRONMENT=Development \
 -p5169:5169 \
 youruser/mottracker:dev
```

Rodar sem migrations:

```bash
docker run -d \
 -e ConnectionStrings__Default="Server=...;Database=...;User Id=...;Password=...;" \
 -e ASPNETCORE_ENVIRONMENT=Production \
 -p5169:5169 \
 youruser/mottracker:prod
```

## Instalação do Projeto via Host (Entrega DotNET)

### Requisitos
- .NET SDK8.0 instalado
- Rider / Visual Studio instalado (opcional)

### Configuração

Clone o projeto utilizando git

1. No arquivo `appsettings.Development.json`, configure e adicione a string de conexão do Oracle DB com seu usuário e senha, por exemplo:

```json
"ConnectionStrings": {
 "Oracle": "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=seuusuario;Password=suasenha;"
}
```

2. Execute as migrations para criar as tabelas no banco Oracle:

```bash
 dotnet ef database update
```

Caso não tenha a ferramenta dotnet-ef instalada, instale com:

```bash
 dotnet tool install --global dotnet-ef
```

### Rodar o Projeto Localmente

Após configurar a string de conexão e aplicar as migrations, você pode rodar a API de duas maneiras:

####1. Com IDE (Rider ou Visual Studio)

1. Abra a solução no Rider ou Visual Studio.
2. Selecione o projeto da API como *Startup Project*.
3. Clique em **Run** com o perfil `http`.

####2. Sem IDE (usando linha de comando)

1. **Restaurar e compilar:**

- Para rodar o backend, entre no diretório `Mottracker_DotNet` antes de executar o comando.
 
- Caso deseje rodar o backend de dentro do diretório do projeto .net, altere o caminho e remova a flag `--project Mottracker` no comando correspondente.

```bash
dotnet restore Mottracker/Mottracker.csproj
dotnet build
```

4. **Rodar o projeto:**

```bash
dotnet run --project Mottracker --urls "http://localhost:5169"
```

## Funcionalidades Implementadas

### ✅ Health Check
- **Endpoint**: `GET /health`
- Retorna o status da aplicação e do banco de dados
- Útil para monitoramento e orquestração de containers

### ✅ Versionamento da API
- Suporte a versionamento via:
  - Query string: `?version=1.0`
  - Header: `X-Version: 1.0`
  - URL: `/api/v1/...`
- Versão padrão: v1.0

### ✅ Segurança JWT
- Autenticação baseada em JWT (JSON Web Token)
- Endpoint de login: `POST /api/v1/usuario/login`
- Endpoints protegidos requerem token JWT no header `Authorization: Bearer {token}`
- Configuração via `appsettings.json` ou variáveis de ambiente:
  - `Jwt:SecretKey`
  - `Jwt:Issuer`
  - `Jwt:Audience`

### ✅ ML.NET - Previsão de Demanda
- **Endpoint**: `POST /api/v1/ml/prediction/moto-demand`
- Utiliza ML.NET para prever a quantidade de motos disponíveis em um pátio
- Considera padrões sazonais e dias da semana
- Requer autenticação JWT

## Documentação da API

### **Swagger UI**
A documentação interativa da API está disponível através do Swagger UI:
- **URL**: http://localhost:5169/swagger
- **Funcionalidades**:
  - Visualização de todos os endpoints
  - Teste direto dos endpoints
  - Exemplos de request/response
  - Esquemas de dados detalhados
  - Autenticação JWT integrada (botão "Authorize")

### **Documentação de Exemplos**
O projeto inclui exemplos completos para todos os DTOs na pasta `Mottracker/Docs/Samples/`:

#### **Exemplos Disponíveis:**
- **Camera**: `CameraRequestDtoSample.cs`, `CameraResponseListSample.cs`
- **Contrato**: `ContratoRequestDtoSample.cs`, `ContratoResponseListSample.cs`
- **Endereco**: `EnderecoRequestDtoSample.cs`, `EnderecoResponseListSample.cs`
- **LayoutPatio**: `LayoutPatioRequestDtoSample.cs`, `LayoutPatioResponseListSample.cs`
- **Moto**: `MotoRequestDtoSample.cs`, `MotoResponseListSample.cs`
- **Patio**: `PatioRequestDtoSample.cs`, `PatioResponseListSample.cs`
- **Permissao**: `PermissaoRequestDtoSample.cs`, `PermissaoResponseListSample.cs`
- **QrCodePonto**: `QrCodePontoRequestDtoSample.cs`, `QrCodePontoResponseListSample.cs`
- **Telefone**: `TelefoneRequestDtoSample.cs`, `TelefoneResponseListSample.cs`
- **Usuario**: `UsuarioRequestDtoSample.cs`, `UsuarioResponseListSample.cs`, `UsuarioLoginDtoSample.cs`
- **UsuarioPermissao**: `UsuarioPermissaoRequestDtoSample.cs`, `UsuarioPermissaoResponseListSample.cs`

#### **Como Usar os Exemplos:**
1. Os exemplos são automaticamente carregados no Swagger UI
2. Cada endpoint mostra exemplos de request e response
3. Os exemplos incluem dados realistas para facilitar os testes

### **Documentação Técnica**
- **Rate Limiting**: 5 requisições por 20 segundos
- **Compressão**: Brotli e Gzip habilitados
- **CORS**: Configurado para React App (localhost:5173)
- **Paginação**: Suporte a paginação em todos os endpoints de listagem
- **HATEOAS**: Links de navegação incluídos nas respostas
- **Health Check**: Endpoint `/health` para monitoramento
- **Versionamento**: Suporte a múltiplas versões da API
- **JWT**: Autenticação e autorização baseada em tokens
- **ML.NET**: Previsão de demanda usando machine learning

## Acesso à API

- **API**: http://localhost:5169  
- **Swagger**: http://localhost:5169/swagger

- **Render**: https://mottracker-dotnet.onrender.com

## Rotas da API

## Camera

- `GET /api/Camera` - Lista todas as câmeras  
- `POST /api/Camera` - Salva uma nova câmera  
- `GET /api/Camera/{id}` - Obtém câmera por ID  
- `PUT /api/Camera/{id}` - Atualiza uma câmera  
- `DELETE /api/Camera/{id}` - Deleta uma câmera  
- `GET /api/Camera/por-nome` - Obtém câmeras por nome  
- `GET /api/Camera/por-status` - Obtém câmeras por status  

---

## Contrato

- `GET /api/Contrato` - Lista todos os contratos  
- `POST /api/Contrato` - Cria um novo contrato  
- `GET /api/Contrato/{id}` - Obtém contrato por ID  
- `PUT /api/Contrato/{id}` - Atualiza contrato existente  
- `DELETE /api/Contrato/{id}` - Remove um contrato  
- `GET /api/Contrato/ativo/{ativoContrato}` - Lista contratos por status de ativo  
- `GET /api/Contrato/usuario/{usuarioId}` - Lista contratos por ID do usuário  
- `GET /api/Contrato/moto/{motoId}` - Lista contratos por ID da moto  
- `GET /api/Contrato/nao-expirados` - Lista contratos que ainda não expiraram  
- `GET /api/Contrato/renovacao-automatica/{renovacao}` - Lista contratos com renovação automática  
- `GET /api/Contrato/por-data-entrada` - Lista contratos entre duas datas de entrada  

---

## Endereco

- `GET /api/Endereco` - Lista todos os endereços  
- `POST /api/Endereco` - Cria um novo endereço  
- `GET /api/Endereco/{id}` - Obtém endereço por ID  
- `PUT /api/Endereco/{id}` - Atualiza endereço existente  
- `DELETE /api/Endereco/{id}` - Remove um endereço  
- `GET /api/Endereco/cep/{cep}` - Obtém endereço por CEP  
- `GET /api/Endereco/estado/{estado}` - Busca endereços por estado  
- `GET /api/Endereco/cidade/{cidade}` - Busca endereços por cidade  
- `GET /api/Endereco/cidade-estado` - Busca por cidade e estado  
- `GET /api/Endereco/bairro/{bairro}` - Busca endereços por bairro  
- `GET /api/Endereco/logradouro` - Busca por logradouro parcial  
- `GET /api/Endereco/patio/{patioId}` - Busca endereço por ID de Pátio  

---

## LayoutPatio

- `GET /api/LayoutPatio` - Lista todos os layouts de pátio  
- `POST /api/LayoutPatio` - Cria um novo layout de pátio  
- `GET /api/LayoutPatio/{id}` - Obtém layout de pátio por ID  
- `PUT /api/LayoutPatio/{id}` - Atualiza um layout de pátio  
- `DELETE /api/LayoutPatio/{id}` - Remove um layout de pátio  
- `GET /api/LayoutPatio/porPatio` - Obtém layouts por ID do pátio  
- `GET /api/LayoutPatio/porDataCriacao` - Obtém layouts por intervalo de data de criação  

---

## Moto

- `GET /api/Moto` - Lista todas as motos  
- `POST /api/Moto` - Cria uma nova moto  
- `GET /api/Moto/{id}` - Obtém moto por ID  
- `PUT /api/Moto/{id}` - Atualiza uma moto  
- `DELETE /api/Moto/{id}` - Remove uma moto  
- `GET /api/Moto/placa/{placa}` - Obtém moto por placa  
- `GET /api/Moto/estado/{estado}` - Obtém motos por estado  
- `GET /api/Moto/contrato/{contratoId}` - Obtém motos por contrato  

---

## Patio

- `GET /api/Patio` - Lista todos os pátios  
- `POST /api/Patio` - Cria um novo pátio  
- `GET /api/Patio/{id}` - Obtém pátio por ID  
- `PUT /api/Patio/{id}` - Atualiza um pátio  
- `DELETE /api/Patio/{id}` - Deleta um pátio  
- `GET /api/Patio/buscar-por-nome` - Busca pátios pelo nome contendo  
- `GET /api/Patio/motos-disponiveis-maior-que` - Busca pátios com motos disponíveis acima de uma quantidade  
- `GET /api/Patio/data-posterior` - Busca pátios por data posterior  
- `GET /api/Patio/data-anterior` - Busca pátios por data anterior  

---

## Permissao

- `GET /api/Permissao` - Lista todas as permissões  
- `POST /api/Permissao` - Cria uma nova permissão  
- `GET /api/Permissao/{id}` - Obtém permissão por ID  
- `PUT /api/Permissao/{id}` - Atualiza uma permissão  
- `DELETE /api/Permissao/{id}` - Deleta uma permissão  
- `GET /api/Permissao/buscar-por-nome` - Busca permissões pelo nome  
- `GET /api/Permissao/buscar-por-descricao` - Busca permissões pela descrição  

---

## QrCodePonto

- `GET /api/QrCodePonto` - Lista todos os QR Codes de ponto  
- `POST /api/QrCodePonto` - Cria um novo QR Code de ponto  
- `GET /api/QrCodePonto/{id}` - Obtém QR Code de ponto por ID  
- `PUT /api/QrCodePonto/{id}` - Atualiza um QR Code de ponto  
- `DELETE /api/QrCodePonto/{id}` - Deleta um QR Code de ponto  
- `GET /api/QrCodePonto/identificador/{identificador}` - Obtém QR Code por identificador  
- `GET /api/QrCodePonto/layoutpatio/{layoutPatioId}` - Lista QR Codes por LayoutPatioId  
- `GET /api/QrCodePonto/posx` - Lista QR Codes por faixa de PosX  
- `GET /api/QrCodePonto/posy` - Lista QR Codes por faixa de PosY  

---

## Telefone

- `GET /api/Telefone` - Lista todos os telefones  
- `POST /api/Telefone` - Cria um novo telefone  
- `GET /api/Telefone/{id}` - Obtém telefone por ID  
- `PUT /api/Telefone/{id}` - Atualiza um telefone  
- `DELETE /api/Telefone/{id}` - Deleta um telefone  
- `GET /api/Telefone/por-numero/{numero}` - Lista telefones por número  
- `GET /api/Telefone/por-usuario/{usuarioId}` - Lista telefones por ID de usuário  
- `GET /api/Telefone/por-tipo/{tipo}` - Lista telefones por tipo  

---

## Usuario

- `GET /api/Usuario` - Lista todos os usuários  
- `POST /api/Usuario` - Cria um novo usuário  
- `GET /api/Usuario/{id}` - Obtém usuário por ID  
- `PUT /api/Usuario/{id}` - Atualiza um usuário  
- `DELETE /api/Usuario/{id}` - Deleta um usuário  
- `GET /api/Usuario/email/{email}` - Obtém usuário por e-mail  

---

## UsuarioPermissao

- `GET /api/UsuarioPermissao` - Lista todas as permissões de usuários  
- `POST /api/UsuarioPermissao` - Cria uma nova permissão de usuário  
- `GET /api/UsuarioPermissao/usuario/{usuarioId}/permissao/{permissaoId}` - Obtém permissão por ID composto  
- `PUT /api/UsuarioPermissao/usuario/{usuarioId}/permissao/{permissaoId}` - Atualiza uma permissão de usuário  
- `DELETE /api/UsuarioPermissao/usuario/{usuarioId}/permissao/{permissaoId}` - Deleta uma permissão de usuário  
- `GET /api/UsuarioPermissao/usuario/{usuarioId}` - Lista permissões por ID de usuário  
- `GET /api/UsuarioPermissao/permissao/{permissaoId}` - Lista usuários por ID de permissão

---

## ML.NET - Previsão de Demanda

- `POST /api/v1/ml/prediction/moto-demand` - Prevê demanda de motos disponíveis em um pátio (requer JWT)

---

## Health Check

- `GET /health` - Verifica o status da aplicação e do banco de dados

---

## Autenticação

- `POST /api/v1/usuario/login` - Realiza login e retorna token JWT
- `POST /api/v1/usuario/logout` - Realiza logout (invalida token)

## Executando os Testes

### Requisitos
- .NET SDK8.0 instalado
- Projeto compilado e dependências restauradas

### Executar Todos os Testes

```bash
# Na raiz do repositório
dotnet test
```

### Executar Apenas Testes Unitários

```bash
dotnet test --filter "TestCategory=Unit" # se as categorias estiverem definidas
# ou executar por projeto de testes
dotnet test ./Mottracker.Tests/Mottracker.Tests.csproj --filter "FullyQualifiedName~UnitTests"
```

### Executar Apenas Testes de Integração

```bash
dotnet test --filter "TestCategory=Integration"
# ou por namespace/nome completo
dotnet test ./Mottracker.Tests/Mottracker.Tests.csproj --filter "FullyQualifiedName~IntegrationTests"
```

### Executar Testes Específicos (exemplos)

```bash
# Executar apenas os testes do JwtService (unit)
dotnet test --filter "FullyQualifiedName~JwtServiceTests"

# Executar apenas os testes do serviço de ML.NET (unit)
dotnet test --filter "FullyQualifiedName~MlPredictionServiceTests"

# Executar apenas os testes de Health Check (integration)
dotnet test --filter "FullyQualifiedName~HealthCheckTests"

# Executar apenas os testes do controller de Moto (integration)
dotnet test --filter "FullyQualifiedName~MotoControllerTests"

# Executar apenas os testes do controller de ML (integration)
dotnet test --filter "FullyQualifiedName~MlPredictionControllerTests"
```

### Estrutura dos Testes (atualizada)

```
Mottracker.Tests/
├── UnitTests/
│ ├── JwtServiceTests.cs # Testes do serviço JWT
│ └── MlPredictionServiceTests.cs # Testes do serviço ML.NET
└── IntegrationTests/
 ├── HealthCheckTests.cs # Testes do endpoint /health
 ├── MotoControllerTests.cs # Testes do controller Moto (CRUD/rotas)
 └── MlPredictionControllerTests.cs # Testes do controller ML (endpoints protegidos)
```

### Observações sobre execução
- Alguns testes de integração podem depender de variáveis de ambiente ou de um banco acessível. Configure `DB_CONNECTION` (ou outras variáveis) antes de executar os testes que precisam de acesso ao banco.
- Use `dotnet test --list-tests` para listar testes disponíveis e construir filtros.
