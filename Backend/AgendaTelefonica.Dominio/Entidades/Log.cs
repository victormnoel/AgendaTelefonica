using AgendaTelefonica.Dominio.Excecoes;

namespace AgendaTelefonica.Dominio.Entidades;

public class Log
{
    #region Propriedades
    public int Id { get; private set; }
    public string NomeDaAcao { get; private set; }
    public string ModeloUtilizadoNaAcao { get; private set; }
    public string RetornoDaAcao { get; set; }
    public string? ExcecaoRetornada { get; set; }
    public DateTime DataDaCriacao { get; private set; }

    #endregion

    #region Construtor

    public Log()
    {
    }

    public Log(string nomeDaAcao, string modeloUtilizado, string retornoDaAcao)
    {
        InserirNomeDaAcao(nomeDaAcao);
        InserirModeloUtilizado(modeloUtilizado);
        InserirRetornoDaAcao(retornoDaAcao);
        DataDaCriacao = DateTime.Now;
    }

    #endregion

    #region Regras de Negocios

    public void RegistrarExcecao(string exececaoRetornada)
    {
        ExcecaoRetornada = string.IsNullOrWhiteSpace(exececaoRetornada)
            ? throw new LogInvalidoException("Os dados da exceção gerada devem ser informados!")
            : exececaoRetornada;
    }
    
    public void InserirInformacoes(string nomeDaAcao, string modeloUtilizado)
    {
        InserirNomeDaAcao(nomeDaAcao);
        InserirModeloUtilizado(modeloUtilizado);
    }
    
    #region Invariantes de Negocio

    private void InserirNomeDaAcao(string nomeDaAcao)
    {
        NomeDaAcao = string.IsNullOrWhiteSpace(nomeDaAcao)
            ? throw new LogInvalidoException("O nome da ação informado deve ser válida!")
            : nomeDaAcao;
    }

    private void InserirModeloUtilizado(string? modeloUtilizado)
    {
        ModeloUtilizadoNaAcao = string.IsNullOrWhiteSpace(modeloUtilizado)
            ? throw new LogInvalidoException("Deve ser informado o modelo que foi utilizado na ação!")
            : modeloUtilizado;
    }

    private void InserirRetornoDaAcao(string retornoDaAcao)
    {
        RetornoDaAcao = string.IsNullOrWhiteSpace(retornoDaAcao)
            ? throw new LogInvalidoException("Deve ser informado o retorno da ação realizada!")
            : retornoDaAcao;
    }
    
    #endregion
    
    #endregion
}