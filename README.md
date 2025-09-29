# Mottracker

## Disclaimer
Na entrega não foi solicitado, mas decidimos criar as relações antecipadamente. Queremos adicionar a opção de que as tabelas pai possam criar as tabelas filhas diretamente a partir de um único JSON.

TODOs:

- Melhorar as requisições, pois estão demorando bastante.
- Permitir a criação da tabela pai e das tabelas filhas de forma integrada.
- Adicionar requisições mais precisas, por exemplo, buscando atributos específicos.
- Implementar segurança e criptografia de senha (incluindo no endpoint de login).
- Revisar os DTOs e os dados que realmente serão necessários para o Frontend.

## Descrição do Projeto

Projeto desenvolvido para o **Challenge da empresa Mottu**, FIAP 2025.

O projeto busca resolver o problema de organização e localização das motos nos pátios da Mottu, facilitando o monitoramento em tempo real. A solução utiliza câmeras com sensores de posicionamento para capturar a localização exata de cada moto. Cada moto possui um **QR Code exclusivo**, assim como os pátios, permitindo identificação e rastreamento eficiente.

A **API** integra os dados capturados pelos dispositivos IoT com uma estrutura de armazenamento adequada:

* **Banco NoSQL**: dados de IoT em tempo real.
* **Banco relacional**: dados estruturados (usuários, motos, contratos, pátios etc).

Essa integração permite o acompanhamento via aplicativo, promovendo **eficiência, segurança e organização**. O sistema também gerencia cadastro/edição de dados, autenticação/autorizacão, gestão de permissões e dashboards com relatórios para tomada de decisão.

## Justificativa da Arquitetura

O projeto foi desenvolvido seguindo os princípios da **Clean Architecture** (Arquitetura Limpa), que oferece inúmeras vantagens para sistemas corporativos como este:

### **Separação de Responsabilidades**
- **Domain**: Contém as entidades de negócio, interfaces de repositório e regras de domínio, garantindo que o núcleo do sistema seja independente de tecnologias externas
- **Application**: Implementa os casos de uso (Use Cases), DTOs e mappers, isolando a lógica de aplicação
- **Infrastructure**: Gerencia a persistência de dados e integrações externas (Oracle Database)
- **Presentation**: Interface com o usuário através de Controllers e Views

### **Benefícios da Arquitetura Escolhida**

1. **Testabilidade**: Cada camada pode ser testada independentemente, facilitando a implementação de testes unitários e de integração

2. **Manutenibilidade**: Mudanças em uma camada não afetam as outras, reduzindo o acoplamento e facilitando manutenções futuras

3. **Flexibilidade**: A troca de banco de dados (Oracle → PostgreSQL, por exemplo) requer alterações apenas na camada Infrastructure

4. **Escalabilidade**: Novos casos de uso podem ser adicionados sem impactar o código existente

5. **Inversão de Dependência**: As camadas internas não dependem das externas, seguindo o princípio SOLID

### **Estrutura do Projeto**
```
Mottracker/
├── Domain/           # Entidades, Interfaces, Enums
├── Application/      # Use Cases, DTOs, Mappers
├── Infrastructure/   # Repositórios, Context EF
└── Presentation/     # Controllers, Views
```

### **Padrões Implementados**
- **Repository Pattern**: Abstração da camada de dados
- **Use Case Pattern**: Encapsulamento da lógica de negócio
- **DTO Pattern**: Transferência de dados entre camadas
- **Mapper Pattern**: Conversão entre entidades e DTOs

Esta arquitetura garante que o sistema seja **robusto, escalável e fácil de manter**, essencial para um projeto corporativo que pode crescer e evoluir ao longo do tempo.

## Autores

### Turma 2TDSR - Análise e Desenvolvimento de Sistemas

* Daniel Saburo Akiyama - RM 558263
* Danilo Correia e Silva - RM 557540
* João Pedro Rodrigues da Costa - RM 558199

## Azure CLI Scripts para criar a VM que vai hospedar a API

Scripts para criar e rodar a API na Azure

[Azure CLI Scripts](AzureScripts.md) <- Acesse os comandos aqui

## Dockerfile

Dockerfile de produção (mais leve):
[Dockerfile Production](./Mottracker/Dockerfile.Production)

Dockerfile de desenvolvimento (mais pesada):
[Dockerfile Development](./Mottracker/Dockerfile.Production)

Imagem Docker Hub: [Imagem Docker Hub com as duas Tags](https://hub.docker.com/repository/docker/danielakiyama/mottracker/general)

**Observação**: Em produção, o ideal é usar a imagem aspnet apenas para executar a aplicação, deixando as migrações para serem feitas fora do container ou em um container separado com o SDK. Isso torna a imagem final mais leve e segura. Contudo, optamos por utilizar o SDK na imagem para facilitar o desenvolvimento do projeto.

## Instalação do Projeto via Docker (Entrega DevOps)

### Requisitos
- Docker instalado e com a engine ligada

### Configuração

1. Rode um container utilizando variaveis de ambiente e a imagem no Docker Hub pelo build

Utilize o comando abaixo, substituindo meuusuario e minhasenha com suas credenciais do Oracle DB:

```bash
  # Esse comando só funciona em bash por conta do \
  docker run -d \
  -e ORACLE_USER=seusuario \
  -e ORACLE_PASSWORD=suasenha \
  -e ORACLE_HOST=oracle.fiap.com.br \
  -e ORACLE_PORT=1521 \
  -e ORACLE_SID=ORCL \
  -e RUN_MIGRATIONS=true \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -p 5169:5169 \
  danielakiyama/mottracker:development-v1.0.0
```

Em uma linha só (recomendado):
```bash
  # Funciona no CMD e Bash (recomendado)
  docker run -d -e ORACLE_USER=seusuario -e ORACLE_PASSWORD=suasenha -e ORACLE_HOST=oracle.fiap.com.br -e ORACLE_PORT=1521 -e ORACLE_SID=ORCL -e RUN_MIGRATIONS=true -e ASPNETCORE_ENVIRONMENT=Development -p 5169:5169 danielakiyama/mottracker:development-v1.0.0
```

OU, se não quiser rodar as migrations:

```bash
  # Esse comando só funciona em bash por conta do \
  docker run -d \
  -e ORACLE_USER=seusuario \
  -e ORACLE_PASSWORD=suasenha \
  -e ORACLE_HOST=oracle.fiap.com.br \
  -e ORACLE_PORT=1521 \
  -e ORACLE_SID=ORCL \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -p 5169:5169 \
  danielakiyama/mottracker:production-v1.0.0
```

Em uma linha só (recomendado):
```bash
  # Funciona no CMD e Bash (recomendado)
  docker run -d -e ORACLE_USER=seusuario -e ORACLE_PASSWORD=suasenha -e ORACLE_HOST=oracle.fiap.com.br -e ORACLE_PORT=1521 -e ORACLE_SID=ORCL -e ASPNETCORE_ENVIRONMENT=Development -p 5169:5169 danielakiyama/mottracker:production-v1.0.0
```

Legendas:

```bash
docker run -d \
  -e ORACLE_USER=seusuario           # Usuário do banco Oracle
  -e ORACLE_PASSWORD=suasenha        # Senha do usuário Oracle
  -e ORACLE_HOST=oracle.fiap.com.br  # Host do banco Oracle
  -e ORACLE_PORT=1521                # Porta do banco Oracle (geralmente 1521)
  -e ORACLE_SID=ORCL                 # SID da instância Oracle
  -e RUN_MIGRATIONS=true             # Controla se as migrações serão executadas na inicialização (true/false)
  -e ASPNETCORE_ENVIRONMENT=Development  # Define o ambiente da aplicação (Development/Production)
  -p 5169:5169                      # Mapeia a porta 5169 do container para a mesma porta na máquina host
  danielakiyama/mottracker:<tags diferentes>  # Nome da imagem e tag Docker a ser executada, lembrando Production (leve) não roda migrations, Development (pesada) roda.
```

## Instalação do Projeto via Host (Entrega DotNET)

### Requisitos
- .NET SDK 8.0 instalado
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

   (entre no diretório do projeto primeiro)

```bash
  dotnet ef database update
```

Caso não tenha a ferramenta dotnet-ef instalada, instale com:

```bash
  dotnet tool install --global dotnet-ef
```

### Rodar o Projeto Localmente

Após configurar a string de conexão e aplicar as migrations, você pode rodar a API de duas maneiras:

#### 1. Com IDE (Rider ou Visual Studio)

1. Abra a solução no Rider ou Visual Studio.
2. Selecione o projeto da API como *Startup Project*.
3. Clique em **Run** com o perfil `http`.

#### 2. Sem IDE (usando linha de comando)

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

## Documentação da API

### **Swagger UI**
A documentação interativa da API está disponível através do Swagger UI:
- **URL**: http://localhost:5169/swagger
- **Funcionalidades**:
  - Visualização de todos os endpoints
  - Teste direto dos endpoints
  - Exemplos de request/response
  - Esquemas de dados detalhados

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
4. Todos os campos obrigatórios e opcionais estão documentados

### **Documentação Técnica**
- **Rate Limiting**: 5 requisições por 20 segundos
- **Compressão**: Brotli e Gzip habilitados
- **CORS**: Configurado para React App (localhost:5173)
- **Paginação**: Suporte a paginação em todos os endpoints de listagem
- **HATEOAS**: Links de navegação incluídos nas respostas

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
