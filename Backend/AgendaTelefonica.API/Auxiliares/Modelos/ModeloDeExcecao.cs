namespace AgendaTelefonica.API.Auxiliares.Modelos;

public class ModeloDeExcecao
{
    #region Propriedades

    public string TipoDeExcecao { get; set; }
    public string Mensagem { get; set; }
    public string StackTrace { get; set; }
    public DateTime Data { get; set; }

    #endregion

    #region Construtor
    public ModeloDeExcecao()
    {
    }

    public ModeloDeExcecao(Exception excecao)
    {
        TipoDeExcecao = excecao.GetType().Name;
        Mensagem = excecao.Message;
        StackTrace = excecao.StackTrace;
        Data = DateTime.UtcNow;
    }

    #endregion
    
}