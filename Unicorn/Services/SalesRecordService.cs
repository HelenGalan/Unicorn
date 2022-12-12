using Microsoft.EntityFrameworkCore;
using Unicorn.Data;
using Unicorn.Models;

namespace Unicorn.Services
{
    public class SalesRecordService
    {
        private readonly UnicornContext _context;

        public SalesRecordService(UnicornContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //logica pra encontrar as vendas no intervalo de data no parametro
            var result = from obj in _context.SalesRecord select obj; //objeto inicial pra contruir consultas nele
            //essa consulta nao e executada pela simples definicao dela
            //pega o SalesRecord que e do tipo DbSet e controi o objeto result de tipo IQueryable
            //em cima deste objeto, eu posso acrescentar mais detalhes para a consulta
            if (minDate.HasValue) //ou seja, eu informei uma data minima
            {
                result = result.Where(x => x.Date >= minDate.Value); //expressao lambda que espressa a restricao de data
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller) //fazendo o join das tabelas
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date) //ordenando por data
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            //logica pra encontrar as vendas no intervalo de data no parametro
            var result = from obj in _context.SalesRecord select obj; //objeto inicial pra contruir consultas nele
            //essa consulta nao e executada pela simples definicao dela
            //pega o SalesRecord que e do tipo DbSet e controi o objeto result de tipo IQueryable
            //em cima deste objeto, eu posso acrescentar mais detalhes para a consulta
            if (minDate.HasValue) //ou seja, eu informei uma data minima
            {
                result = result.Where(x => x.Date >= minDate.Value); //expressao lambda que espressa a restricao de data
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller) //fazendo o join das tabelas
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date) //ordenando por data
                .GroupBy(x => x.Seller.Department) //quando agrupa, o tipo nao pode ser list, precisa ser igrouping
                .ToListAsync();
        }
    }
}
