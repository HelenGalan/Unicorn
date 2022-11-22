using Unicorn.Models;

namespace Unicorn.Data
{
    //quando um SeedingService for criado, vai receber uma instancia do context tambem
    public class SeedingService
    {
        private UnicornContext _context;

        public SeedingService(UnicornContext context)
        {
            _context = context;
        }

        //operacao responsavel por popular a BD
        public void Seed()
        {
            //if para testar se existe algum dado na base de dados (se existir, nao faz nada)
            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any()) //esta testando as tres tabelas com a operacao any do linq
            {
                return; //para cortar a execucao do metodo, nao faz mais nada, pois o BD ja foi populado
            }

            Department d1
        }
    }
}
