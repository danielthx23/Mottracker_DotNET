// JavaScript personalizado para o Mottracker
// Sistema de rastreamento de motos

// Função para confirmar exclusão genérica
function confirmarExclusao(id, nome, tipo) {
    if (confirm(`Deseja realmente excluir ${tipo} "${nome}"?`)) {
        document.getElementById(`deleteForm${id}`).submit();
    }
}

// Função para confirmar exclusão de moto
function confirmarExclusaoMoto(id, placa) {
    if (confirm(`Deseja realmente excluir a moto com placa "${placa}"?`)) {
        document.getElementById(`deleteForm${id}`).submit();
    }
}

// Função para confirmar exclusão de contrato
function confirmarExclusaoContrato(id, numero) {
    if (confirm(`Deseja realmente excluir o contrato "${numero}"?`)) {
        document.getElementById(`deleteForm${id}`).submit();
    }
}

// Função para confirmar exclusão de pátio
function confirmarExclusaoPatio(id, nome) {
    if (confirm(`Deseja realmente excluir o pátio "${nome}"?`)) {
        document.getElementById(`deleteForm${id}`).submit();
    }
}

// Função para confirmar exclusão de câmera
function confirmarExclusaoCamera(id, nome) {
    if (confirm(`Deseja realmente excluir a câmera "${nome}"?`)) {
        document.getElementById(`deleteForm${id}`).submit();
    }
}

// Função para confirmar exclusão de QR Code
function confirmarExclusaoQrCode(id, identificador) {
    if (confirm(`Deseja realmente excluir o QR Code "${identificador}"?`)) {
        document.getElementById(`deleteForm${id}`).submit();
    }
}

// Função para formatar placa de moto
function formatarPlaca(placa) {
    return placa.replace(/([A-Z]{3})([0-9]{4})/, '$1-$2');
}

// Função para validar CEP
function validarCEP(cep) {
    const cepRegex = /^\d{5}-?\d{3}$/;
    return cepRegex.test(cep);
}

// Função para formatar CEP
function formatarCEP(cep) {
    return cep.replace(/(\d{5})(\d{3})/, '$1-$2');
}

// Função para validar telefone
function validarTelefone(telefone) {
    const telefoneRegex = /^\(\d{2}\)\s\d{4,5}-\d{4}$/;
    return telefoneRegex.test(telefone);
}

// Função para formatar telefone
function formatarTelefone(telefone) {
    return telefone.replace(/(\d{2})(\d{4,5})(\d{4})/, '($1) $2-$3');
}

// Inicialização de tooltips do Bootstrap
document.addEventListener('DOMContentLoaded', function () {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });
    
    // Inicialização de popovers do Bootstrap
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl)
    });
    
    // Auto-formatação de campos
    const cepInputs = document.querySelectorAll('input[name*="CEP"], input[name*="Cep"]');
    cepInputs.forEach(input => {
        input.addEventListener('input', function(e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length >= 5) {
                value = value.replace(/(\d{5})(\d{3})/, '$1-$2');
            }
            e.target.value = value;
        });
    });
    
    const telefoneInputs = document.querySelectorAll('input[name*="Telefone"], input[name*="telefone"]');
    telefoneInputs.forEach(input => {
        input.addEventListener('input', function(e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length >= 10) {
                value = value.replace(/(\d{2})(\d{4,5})(\d{4})/, '($1) $2-$3');
            }
            e.target.value = value;
        });
    });
});
