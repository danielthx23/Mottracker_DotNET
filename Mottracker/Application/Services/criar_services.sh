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

namespace="Mottracker.Application.Services"

for nome in "${nomes[@]}"; do
  arquivo="${nome}ApplicationService.cs"
  cat > "$arquivo" <<EOF
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace $namespace
{   
    public class ${nome}ApplicationService : I${nome}ApplicationService
    {
        private readonly I${nome}Repository _repository;
      
        public ${nome}ApplicationService(I${nome}Repository repository)
        {
            _repository = repository;
        }
      
        public ${nome}Entity? Obter${nome}PorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<${nome}Entity> ObterTodos${nome}() // TODO: Arrumar o nome
        {
            return _repository.ObterTodos();
        }
              
        public ${nome}Entity? SalvarDados${nome}(${nome}Entity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public ${nome}Entity? EditarDados${nome}(int id, ${nome}Entity entity)
        {
            var ${nome}Existente = _repository.ObterPorId(id);
      
            if (${nome}Existente == null)
                return null;
                  
            // TODO: Adicionar a alteração
      
            return _repository.Atualizar(${nome}Existente);
        }
              
        public ${nome}Entity? DeletarDados${nome}(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
EOF
done
