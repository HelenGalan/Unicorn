using Microsoft.EntityFrameworkCore;
using Unicorn.Data;
using Unicorn.Models;

namespace Unicorn.Services
{
    // esse servico tera a mesma estrutura de dependencia que o SellerService, entao basta copiar de la
    public class DepartmentService
    {
        private readonly UnicornContext _context; //readonly e para previnir que essa dependencia seja alterada

        public DepartmentService(UnicornContext context)
        {
            _context = context;
        }

        //metodo para retornar todos os departamentos
        //public List<Department> FindAll() //essa operacao e sincrona
        //{
        //    //implementacao do metodo ira retornar o context (lista), ordenada por nome
        //    return _context.Department.OrderBy(x => x.Name).ToList();
        //}

        //operacao assincrona
        //operacao retorna um Task de um List de Department
        //decora com async
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

    }
}
