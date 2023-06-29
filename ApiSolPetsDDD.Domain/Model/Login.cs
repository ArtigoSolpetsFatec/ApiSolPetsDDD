using System;

namespace ApiSolPetsDDD.Domain.Model
{
    public class Login
    {
        public int? IdLogin { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string SenhaCriptografada { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? DhUltimaAtualizacao { get; set; }
        public bool LoginIsValid { get; set; }
    }
}
