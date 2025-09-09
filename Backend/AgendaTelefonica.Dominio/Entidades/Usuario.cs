using AgendaTelefonica.Dominio.Enums;
using AgendaTelefonica.Dominio.Excecoes;

namespace AgendaTelefonica.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public StatusPadrao Status { get; private set; }
        public DateTime DataDaCriacao { get; private set; }
        public DateTime DataDaAtualizacao { get; private set; }

        public Usuario(){}

        public Usuario(string nome, string email, string telefone)
        {
            InserirEmail(email);
            InserirNome(nome);
            InserirTelefone(telefone);
            Status = StatusPadrao.Ativo;
        }
        
        #region Regras de Negocio

        public void AtualizarInformacoesDoUsuario(string nome, string email, string telefone)
        {
            InserirNome(nome);
            InserirEmail(email);
            InserirTelefone(telefone);
        }

        public void RemoverUsuario()
        {
            if (Status == StatusPadrao.Excluido) throw new UsuarioJaExcluidoException();
            Status = StatusPadrao.Excluido;
        }

        #region Invariantes De Negocio

        private void InserirNome(string nome)
        {
            Nome = string.IsNullOrWhiteSpace(nome) 
                ? throw new NomeInvalidoException() 
                : nome.Trim();
        }

        private void InserirEmail(string email)
        {
            Email = string.IsNullOrWhiteSpace(email)
                ? throw new EmailInvalidoException()
                : email.Trim();
        }
        
        private void InserirTelefone(string telefone)
        {
            Telefone = string.IsNullOrWhiteSpace(telefone)
                ? throw new TelefoneInvalidoException()
                : telefone.Trim();
        }
        
        #endregion
        
        #endregion
    }
}
