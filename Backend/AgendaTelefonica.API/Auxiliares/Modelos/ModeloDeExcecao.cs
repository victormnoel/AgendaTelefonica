namespace AgendaTelefonica.API.Auxiliares.Modelos;

public class ModeloDeExcecao
{
    public string TipoDeExcecao { get; set; }
    public string Mensagem { get; set; }
    public string StackTrace { get; set; }
    public DateTime Data { get; set; }

    public ModeloDeExcecao() { }

    public ModeloDeExcecao(Exception excecao)
    {
        TipoDeExcecao = excecao.GetType().Name;
        Mensagem = excecao.Message;
        StackTrace = excecao.StackTrace;
        Data = DateTime.UtcNow;
    }

    public static ModeloDeExcecao CriarDe(Exception excecao)
    {
        return new ModeloDeExcecao(excecao);
    }
}