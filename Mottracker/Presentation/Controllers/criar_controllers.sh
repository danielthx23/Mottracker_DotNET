#!/usr/bin/env bash

# Lista de nomes base
nomes=(
  "Camera"
  "Contrato"
  "Endereco"
  "LayoutPatio"
  "Moto"
  "Patio"
  "Permissao"
  "QrCodePonto"
  "Telefone"
  "Usuario"
  "UsuarioPermissao"
)

# Namespace do projeto (altere conforme necessário)
namespace="Mottracker.Presentation.Controllers"

# Loop para criar os arquivos
for nome in "${nomes[@]}"; do
  arquivo="${nome}Controller.cs"
  cat > "$arquivo" <<EOF
namespace $namespace
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ${nome}Controller : ControllerBase
    {
        private readonly I${nome}ApplicationService _applicationService;
      
        public ${nome}Controller(I${nome}ApplicationService applicationService)
        {
          _applicationService = applicationService;
        }
    }
}
EOF
done
