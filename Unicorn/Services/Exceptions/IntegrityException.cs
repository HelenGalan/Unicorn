namespace Unicorn.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message) //para erros de integridade referencial
            //por ex: deletar um vendedor que tem vendas, pois vendas precisa ter um vendedor
        {

        }
    }
}
