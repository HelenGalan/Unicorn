using Microsoft.EntityFrameworkCore;
using Unicorn.Data;
using Unicorn.Models;
using Unicorn.Services.Exceptions;

namespace Unicorn.Services
{
    public class SellerService
    {
        private readonly UnicornContext _context; //readonly e para previnir que essa dependencia seja alterada

        public SellerService(UnicornContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync() //retorna uma lista com todos os vendedores do banco de dados (pelo EF)
        {
            return await _context.Seller.ToListAsync();
            //acessa a fonte de dados relacionada a tabela de vendedores e converte pra uma lista
            //essa operacao por enquanto e sincrona, que significa:
            //vai rodar o acesso ao banco de dados e a aplicacao vai ficar bloqueada esperando essa operacao terminar
            //desta forma o sistema nao tem uma boa performance
        }

        //metodo para inserir no banco de dados um novo vendedor
        public async Task InsertAsync (Seller obj)
        {
            _context.Add(obj); //adiciona no BD, operacao do EF
            await _context.SaveChangesAsync(); //pra salvar a adicao
        }

        //selecionar o objeto para proceder com a delecao
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        //depois de selecionar, deletar esse objeto
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id); //implementacao baseada no scaffolding, chamando o objeto
                _context.Seller.Remove(obj); //aqui apenas remove o objeto do dbset
                await _context.SaveChangesAsync(); //efetivar no banco de dados
            }
            catch (DbUpdateException e) 
            {
                throw new IntegrityException("You can't delete this seller because there are seeling connected!");
            }
        }

        //metodo pra atualizar um objeto do tipo seller
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            //testando se existe o id desse objeto no BD
            if (!hasAny) //se nao existir, lanca a excecao
            {
                throw new NotFoundException("Id not found!");
            } //se passar por esse if, significa que ja existe o objeto la
            try //quando chama a operacao de atualizar no banco de dados, pode ocorrer uma excecao de conflito de concorrencia
            {
                _context.Update(obj); //entao atualiza
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e) //se acontecer essa excecao do EF
            {
                throw new DbConcurrencyException(e.Message); //relanca a excecao em nivel de servico e colocar a mensagem que veio do BD
            } //importante para segregar as camadas e respeitar a arquitetura (controlador conversa com a camada de servico e excecoes od nivel de acesso a dados sao capturados pelo servico e relancadas como excecoes de servico para o controlador)
        }
    }
}
