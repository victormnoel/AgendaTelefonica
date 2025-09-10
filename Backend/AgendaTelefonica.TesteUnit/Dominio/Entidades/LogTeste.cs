using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Excecoes;
using AutoFixture;
using Bogus;

namespace AgendaTelefonica.TesteUnit.Dominio.Entidades;

public class LogTeste
{
    #region Propriedades
    private readonly Faker _faker;
    #endregion
    
    #region Construtor
    
    public LogTeste()
    {
        _faker = new Faker();
    }
    #endregion

    #region Testes

    [Fact]
    public void DadoAsInformacoesSejamValidas_QuandoCriarUmLog_EntaoPropriedadesDevemSerPreenchidasCorretamente()
    {
        string nomeDaAcao = _faker.Hacker.Verb();
        string modeloUtilizado = _faker.Commerce.ProductName();
        string retornoDaAcao = _faker.Lorem.Sentence();
        Log logCriado = new Log(nomeDaAcao, modeloUtilizado, retornoDaAcao);

        Assert.Equal(nomeDaAcao, logCriado.NomeDaAcao);
        Assert.Equal(modeloUtilizado, logCriado.ModeloUtilizadoNaAcao);
        Assert.Equal(retornoDaAcao, logCriado.RetornoDaAcao);
        Assert.NotNull(logCriado);
        Assert.IsType<Log>(logCriado);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DadoONomeDaAcaoInvalido_QuandoCriarUmLog_EntaoDeveLancarLogInvalidoException(string nomeInvalido)
    {
        string modeloUtilizado = _faker.Commerce.ProductName();
        string retornoDaAcao = _faker.Lorem.Sentence();
        Assert.Throws<LogInvalidoException>(() => new Log(nomeInvalido, modeloUtilizado, retornoDaAcao));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DadoModeloUtilizadoInvalido_QuandoCriarUmLog_EntaoDeveLancarLogInvalidoException(string modeloInvalido)
    {
        string nomeDaAcao = _faker.Hacker.Verb();
        string retornoDaAcao = _faker.Lorem.Sentence();

        Assert.Throws<LogInvalidoException>(() => new Log(nomeDaAcao, modeloInvalido, retornoDaAcao));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DadoRetornoDaAcaoForInvalido_QuandoCriarUmLog_EntaoDeveLancarLogInvalidoException(string retornoInvalido)
    {
        string nomeDaAcao = _faker.Hacker.Verb();
        string modeloUtilizado = _faker.Commerce.ProductName();
        Assert.Throws<LogInvalidoException>(() => new Log(nomeDaAcao, modeloUtilizado, retornoInvalido));
    }

    [Fact]
    public void DadaExcecaoNoModeloValidaForInserida_QuandoRegistrarExcecao_EntaoPropriedadeExcecaoRetornadaDeveSerPreenchida()
    {
        Log logDaAcao = new Log(_faker.Hacker.Verb(),
            _faker.Commerce.ProductName(),
            _faker.Lorem.Sentence());
        string excecaoLancada = _faker.Lorem.Paragraph();
        logDaAcao.RegistrarExcecao(excecaoLancada);

        Assert.Equal(excecaoLancada, logDaAcao.ExcecaoRetornada);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DadaExcecaoRepassadaForInvalida_QuandoRegistrarExcecao_EntaoDeveLancarLogInvalidoException(string excecaoInvalida)
    {
        Log logDaAcao = new Log(_faker.Hacker.Verb(),
            _faker.Commerce.ProductName(),
            _faker.Lorem.Sentence());

        Assert.Throws<LogInvalidoException>(() =>
            logDaAcao.RegistrarExcecao(excecaoInvalida));
    }

    [Fact]
    public void TendoNovasInformacoesValidasParaInserir_QuandoExecutarOMetodoInserirInformacoes_EntaoPropriedadesDevemSerAtualizadas()
    {
        Log logDaAcao = new Log(_faker.Hacker.Verb(),
            _faker.Commerce.ProductName(),
            _faker.Lorem.Sentence());
        string novoNome = _faker.Hacker.Verb();
        string novoModelo = _faker.Commerce.ProductName();

        logDaAcao.InserirInformacoes(novoNome, novoModelo);
        
        Assert.Equal(novoNome, logDaAcao.NomeDaAcao);
        Assert.Equal(novoModelo, logDaAcao.ModeloUtilizadoNaAcao);
    }
    #endregion
}