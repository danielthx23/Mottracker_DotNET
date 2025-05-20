## Criação da VM na Azure que vai hospedar a API via CLI

### Logue na sua conta da Azure via CLI
az login

### Crie o grupo de recursos na região Brazil South 
az group create --name rg-vmMottracker --location brazilsouth

### Criando a VM com a imagem Ubuntu2204 tamanho Standard_B2s (2vCPU 4RAM) com o usuario mottrackeradmin, no final gerar as chaves para conectar via ssh.
az vm create --resource-group rg-vmMottracker --name vmMottracker --image Ubuntu2204 --size Standard_B2s --admin-username mottrackeradmin --generate-ssh-keys

### Comandos para abrir as portas da VM (5169 e o da API)
az vm open-port --resource-group rg-vmMottracker --name vmMottracker --port 80
az vm open-port --resource-group rg-vmMottracker --name vmMottracker --port 443 --priority 1001
az vm open-port --resource-group rg-vmMottracker --name vmMottracker --port 5169 --priority 1002
az vm open-port --resource-group rg-vmMottracker --name vmMottracker --port 1521 --priority 1003

### Conectar via SSH na VM
ssh -i caminho/para/chave/pem mottrackeradmin@ipdasuavm

### Rodar a API na VM
docker run -d -e ORACLE_USER=seusuario -e ORACLE_PASSWORD=suasenha -e ORACLE_HOST=oracle.fiap.com.br -e ORACLE_PORT=1521 -e ORACLE_SID=ORCL -e ASPNETCORE_ENVIRONMENT=Development -p 5169:5169 danielakiyama/mottracker:production-v1.0.0

### Acessar a VM com a API rodando em segundo plano via browser
http://<ip_da_sua_vm>:5169/swagger