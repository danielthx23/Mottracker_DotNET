# Mottracker

## Descrição do Projeto
Projeto para o Challenge da empresa Mottu, FIAP 2025.

A princípio, o projeto visa resolver o problema de organização da localização das motos nos pátios da Mottu, facilitando a gestão e monitoramento em tempo real dos veículos. Nossa solução utiliza câmeras equipadas com sensores de posicionamento que capturam a localização exata das motos. Cada moto possui um QR Code exclusivo para identificação individual, enquanto os pátios também são mapeados com QR Codes para facilitar a localização dos veículos em áreas específicas.

A API desenvolvida funcionará como uma plataforma integrada que une os dados gerados pelos dispositivos IoT das câmeras com a infraestrutura de armazenamento adequada para cada tipo de dado: os dados de IoT serão armazenados em um banco NoSQL, garantindo alta performance e escalabilidade para o processamento de grandes volumes de dados em tempo real; já os dados estruturados, como informações de motos, pátios, usuários, contratos e dashboards, serão mantidos em um banco de dados relacional.

Essa integração permite o acompanhamento em tempo real das motos via aplicativo, promovendo maior eficiência operacional, segurança e organização.

Além disso, o sistema gerencia cadastro e edição de dados, no futuro vai oferecer funcionalidades para autenticação e autorização dos usuários, gestão de permissões e geração de relatórios através de dashboards customizáveis, que auxiliam a equipe da Mottu a tomar decisões baseadas em dados confiáveis e atualizados.

## Instalação do Projeto

### Requisitos
- .NET SDK 7.0

### Configuração

1. No arquivo `appsettings.Development.json`, configure a string de conexão do Oracle DB com seu usuário e senha, por exemplo:

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

### Rodar o projeto
Após configurar a string de conexão e aplicar as migrations, para rodar a API localmente, utilize o Rider ou Visual Studio e clique em http run.

Por padrão, a API estará disponível em:

http://localhost:5169

Agora você pode acessar o Swagger para testar as rotas em:

http://localhost:5169/swagger

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
