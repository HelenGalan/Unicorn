using Unicorn.Data;
using Unicorn.Models;

namespace Unicorn.Services
{
    public class SellerService
    {
        private readonly UnicornContext _context; //readonly e para previnir que essa dependencia seja alterada

        public SellerService(UnicornContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll() //retorna uma lista com todos os vendedores do banco de dados (pelo EF)
        {
            return _context.Seller.ToList();
            //acessa a fonte de dados relacionada a tabela de vendedores e converte pra uma lista
            //essa operacao por enquanto e sincrona, que significa:
            //vai rodar o acesso ao banco de dados e a aplicacao vai ficar bloqueada esperando essa operacao terminar
            //desta forma o sistema nao tem uma boa performance
        }
    }
}
