namespace Unicorn.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        //Excecoes especificas da camada de servico
        //Quando e personalizada, existe a possibilidade de tratar exclusivamente essa excecao
        //tem um controle maior para cada tipo de excecao que pode ocorrer
        
        //construtor basico recebendo uma mensagem
        //esse construtor vai repassar a chamada para a classe base
        public NotFoundException(string message) : base(message) { } //excecao personalizada
    }
}
