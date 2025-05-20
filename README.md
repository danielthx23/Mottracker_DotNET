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

## Autores

### Turma 2TDSR - Análise e Desenvolvimento de Sistemas

* Daniel Saburo Akiyama - RM 558263
* Danilo Correia e Silva - RM 557540
* João Pedro Rodrigues da Costa - RM 558199

## Azure CLI Scripts para criar a VM que vai hospedar a API

TODO -> Referenciar arquivo

## Instalação do Projeto via Docker (Entrega DevOps)

### Requisitos
- Docker instalado e com a engine ligada

### Configuração

1. Rode um container utilizando variaveis de ambiente e a imagem no Docker Hub pelo build

Utilize o comando abaixo, substituindo meuuser e minhasenha com suas credenciais do Oracle DB:

```bash
  docker run \
  -e ORACLE_USER=seusuario           # Usuário do banco Oracle
  -e ORACLE_PASSWORD=suasenha        # Senha do usuário Oracle
  -e ORACLE_HOST=oracle.fiap.com.br  # Host do banco Oracle
  -e ORACLE_PORT=1521                # Porta do banco Oracle (geralmente 1521)
  -e ORACLE_SID=ORCL                 # SID da instância Oracle
  -e RUN_MIGRATIONS=true             # Controla se as migrações serão executadas na inicialização (true/false)
  -e ASPNETCORE_ENVIRONMENT=Development  # Define o ambiente da aplicação (Development/Production)
  -p 5169:5169                      # Mapeia a porta 5169 do container para a mesma porta na máquina host
  danielthx23/mottracker             # Nome da imagem Docker a ser executada
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

```bash
dotnet restore Mottracker/Mottracker.csproj
dotnet build
```

4. **Rodar o projeto:**

```bash
dotnet run --project Mottracker --urls "http://localhost:5169"
```

## Acesso à API

- API: http://localhost:5169  
- Swagger: http://localhost:5169/swagger

## Rotas da API

### Camera
- `GET /api/Camera` - Lista todas as câmeras  
- `POST /api/Camera` - Salva uma nova câmera  
- `GET /api/Camera/{id}` - Obtém câmera por ID  
- `PUT /api/Camera/{id}` - Atualiza uma câmera  
- `DELETE /api/Camera/{id}` - Deleta uma câmera  

### Contrato
- `GET /api/Contrato` - Lista todos os contratos  
- `POST /api/Contrato` - Cria um novo contrato  
- `GET /api/Contrato/{id}` - Obtém contrato por ID  
- `PUT /api/Contrato/{id}` - Atualiza contrato existente  
- `DELETE /api/Contrato/{id}` - Remove um contrato  

### Endereco
- `GET /api/Endereco` - Lista todos os endereços  
- `POST /api/Endereco` - Cria um novo endereço  
- `GET /api/Endereco/{id}` - Obtém endereço por ID  
- `PUT /api/Endereco/{id}` - Atualiza endereço existente  
- `DELETE /api/Endereco/{id}` - Remove um endereço  

### LayoutPatio
- `GET /api/LayoutPatio` - Lista todos os layouts de pátio  
- `POST /api/LayoutPatio` - Cria um novo layout de pátio  
- `GET /api/LayoutPatio/{id}` - Obtém layout de pátio por ID  
- `PUT /api/LayoutPatio/{id}` - Atualiza um layout de pátio  
- `DELETE /api/LayoutPatio/{id}` - Remove um layout de pátio  

### Moto
- `GET /api/Moto` - Lista todas as motos  
- `POST /api/Moto` - Cria uma nova moto  
- `GET /api/Moto/{id}` - Obtém moto por ID  
- `PUT /api/Moto/{id}` - Atualiza uma moto  
- `DELETE /api/Moto/{id}` - Remove uma moto  

### Patio
- `GET /api/Patio` - Lista todos os pátios  
- `POST /api/Patio` - Cria um novo pátio  
- `GET /api/Patio/{id}` - Obtém pátio por ID  
- `PUT /api/Patio/{id}` - Atualiza um pátio  
- `DELETE /api/Patio/{id}` - Deleta um pátio  

### Permissao
- `GET /api/Permissao` - Lista todas as permissões  
- `POST /api/Permissao` - Cria uma nova permissão  
- `GET /api/Permissao/{id}` - Obtém permissão por ID  
- `PUT /api/Permissao/{id}` - Atualiza uma permissão  
- `DELETE /api/Permissao/{id}` - Deleta uma permissão  

### QrCodePonto
- `GET /api/QrCodePonto` - Lista todos os QR Codes de ponto  
- `POST /api/QrCodePonto` - Cria um novo QR Code de ponto  
- `GET /api/QrCodePonto/{id}` - Obtém QR Code de ponto por ID  
- `PUT /api/QrCodePonto/{id}` - Atualiza um QR Code de ponto  
- `DELETE /api/QrCodePonto/{id}` - Deleta um QR Code de ponto  

### Telefone
- `GET /api/Telefone` - Lista todos os telefones  
- `POST /api/Telefone` - Cria um novo telefone  
- `GET /api/Telefone/{id}` - Obtém telefone por ID  
- `PUT /api/Telefone/{id}` - Atualiza um telefone  
- `DELETE /api/Telefone/{id}` - Deleta um telefone  

### Usuario
- `GET /api/Usuario` - Lista todos os usuários  
- `POST /api/Usuario` - Cria um novo usuário  
- `GET /api/Usuario/{id}` - Obtém usuário por ID  
- `PUT /api/Usuario/{id}` - Atualiza um usuário  
- `DELETE /api/Usuario/{id}` - Deleta um usuário  

### UsuarioPermissao
- `GET /api/UsuarioPermissao` - Lista todas as permissões de usuários  
- `POST /api/UsuarioPermissao` - Cria uma nova permissão de usuário  
- `GET /api/UsuarioPermissao/usuario/{usuarioId}/permissao/{permissaoId}` - Obtém permissão de usuário por ID composto  
- `PUT /api/UsuarioPermissao/usuario/{usuarioId}/permissao/{permissaoId}` - Atualiza uma permissão de usuário  
- `DELETE /api/UsuarioPermissao/usuario/{usuarioId}/permissao/{permissaoId}` - Deleta uma permissão de usuário  

## Implementações Futuras & TODO's

- Buscas por atributos das tabelas
- Segurança e criptografia de senha
- Implementação do NoSQL (MongoDB)
- Lógica para alterar dados das tabelas a partir do NoSQL
- Otimizar requisições do banco de dados
