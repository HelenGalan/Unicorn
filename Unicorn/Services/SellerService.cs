﻿using Microsoft.EntityFrameworkCore;
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

        public List<Seller> FindAll() //retorna uma lista com todos os vendedores do banco de dados (pelo EF)
        {
            return _context.Seller.ToList();
            //acessa a fonte de dados relacionada a tabela de vendedores e converte pra uma lista
            //essa operacao por enquanto e sincrona, que significa:
            //vai rodar o acesso ao banco de dados e a aplicacao vai ficar bloqueada esperando essa operacao terminar
            //desta forma o sistema nao tem uma boa performance
        }

        //metodo para inserir no banco de dados um novo vendedor
        public void Insert (Seller obj)
        {
            _context.Add(obj); //adiciona no BD, operacao do EF
            _context.SaveChanges(); //pra salvar a adicao
        }

        //selecionar o objeto para proceder com a delecao
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        //depois de selecionar, deletar esse objeto
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id); //implementacao baseada no scaffolding, chamando o objeto
            _context.Seller.Remove(obj); //aqui apenas remove o objeto do dbset
            _context.SaveChanges(); //efetivar no banco de dados
        }

        //metodo pra atualizar um objeto do tipo seller
        public void Update(Seller obj)
        {
            //testando se existe o id desse objeto no BD
            if (!_context.Seller.Any(x => x.Id == obj.Id)) //se nao existir, lanca a excecao
            {
                throw new NotFoundException("Id not found!");
            } //se passar por esse if, significa que ja existe o objeto la
            try //quando chama a operacao de atualizar no banco de dados, pode ocorrer uma excecao de conflito de concorrencia
            {
                _context.Update(obj); //entao atualiza
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e) //se acontecer essa excecao do EF
            {
                throw new DbConcurrencyException(e.Message); //relanca a excecao em nivel de servico e colocar a mensagem que veio do BD
            } //importante para segregar as camadas e respeitar a arquitetura (controlador conversa com a camada de servico e excecoes od nivel de acesso a dados sao capturados pelo servico e relancadas como excecoes de servico para o controlador)
        }
    }
}
