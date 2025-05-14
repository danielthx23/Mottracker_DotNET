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
namespace="Mottracker.Infrastructure.Data.Repositories"

# Loop para criar os arquivos
for nome in "${nomes[@]}"; do
  arquivo="${nome}Repository.cs"
  cat > "$arquivo" <<EOF
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace $namespace
{   
    public class ${nome}Repository : I${nome}Repository
        {
            private readonly ApplicationContext _context;
    
            public ${nome}Repository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<${nome}Entity> ObterTodos()
            {
                var ${nome} = _context.${nome}.ToList();
    
                return ${nome};
            }
    
            public ${nome}Entity? ObterPorId(int id)
            {
                var ${nome} = _context.${nome}.Find(id);
    
                return ${nome};
            }
    
            public ${nome}Entity? Salvar(${nome}Entity entity)
            {
                _context.${nome}.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public ${nome}Entity? Atualizar(${nome}Entity entity)
            {
                _context.${nome}.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public ${nome}Entity? Deletar(int id)
            {
                var entity = _context.${nome}.Find(id);
    
                if (entity is not null)
                {
                    _context.${nome}.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
EOF
done
