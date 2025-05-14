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
namespace="Mottracker.Application.Dtos"

# Loop para criar os arquivos
for nome in "${nomes[@]}"; do
  arquivo="${nome}Dto.cs"
  cat > "$arquivo" <<EOF
namespace $namespace
{
    public class ${nome}Dto
    {
    }
}
EOF
done
