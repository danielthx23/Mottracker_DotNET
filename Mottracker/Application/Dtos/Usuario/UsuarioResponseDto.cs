namespace Mottracker.Application.Dtos.Usuario
{
    public class UsuarioLoginResponseDto
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string TokenUsuario { get; set; }
        public DateTime DataNascimentoUsuario { get; set; }
        public DateTime CriadoEmUsuario { get; set; }
        public bool LoginSucesso { get; set; }
        public string Mensagem { get; set; }
    }
}