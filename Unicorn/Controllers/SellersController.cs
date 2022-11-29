using Microsoft.AspNetCore.Mvc;
using Unicorn.Models;
using Unicorn.Services;

namespace Unicorn.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService; //declaracao da dependencia para o servico SellerService

        public SellersController(SellerService sellerService) //construtor para injetar a dependencia
        {
            _sellerService = sellerService;
        }

        public IActionResult Index() //chamei o controlador
        {
            var list = _sellerService.FindAll(); //o controlador acessou meu model e colocou o dado em "list"
            return View(list); //encaminhou os dados para a view
            //dinamica do MVC
        }

        public IActionResult Create()
        {
            //so retorna a view chamada Create
            return View();
        }

        [HttpPost] //anotacao para indicar que essa acao e uma acao de post
        [ValidateAntiForgeryToken] //anotacao para previnir ataque CSRF
        //CSRF e quando alguem se aproveita da sua sessao de autenticacao e envia dados maliciosos

        //recebe o objeto vendedor que veio na requisicao e instancia
        public IActionResult Create(Seller seller)
        {
            //implementacao a acao de inserir no banco de dados
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); //redireciona para a acao Index que e inicial do CRUD de vendedores
            //o nameof facilita a manutencao, porque se a string mudar um dia, nao precisa mudar aqui
        }
    }
}
