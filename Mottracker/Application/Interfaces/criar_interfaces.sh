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

namespace="Mottracker.Application.Interfaces"

for nome in "${nomes[@]}"; do
  arquivo="I${nome}ApplicationService.cs"
  cat > "$arquivo" <<EOF
namespace $namespace
{   
    public interface I${nome}ApplicationService
    {
        IEnumerable<${nome}Entity> ObterTodos${nome}();
        ${nome}Entity? Obter${nome}PorId(int id);
        ${nome}Entity? SalvarDados${nome}(${nome}Entity entity);
        ${nome}Entity? EditarDados${nome}(int id, ${nome}Entity entity);
        ${nome}Entity? DeletarDados${nome}(int id);
    }
}
EOF
done

# TODO: FILTRAR OS METODOS NECESSÁRIOS
