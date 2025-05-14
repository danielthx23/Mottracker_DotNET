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

namespace="Mottracker.Domain.Interfaces"

for nome in "${nomes[@]}"; do
  arquivo="I${nome}Repository.cs"
  cat > "$arquivo" <<EOF
using Mottracker.Domain.Entities;

namespace $namespace
{   
    public interface I${nome}Repository
    {
        IEnumerable<${nome}Entity> ObterTodos();
        ${nome}Entity? ObterPorId(int id);
        ${nome}Entity? Salvar(${nome}Entity entity);
        ${nome}Entity? Atualizar(${nome}Entity entity);
        ${nome}Entity? Deletar(int id);
    }
}
EOF
done
