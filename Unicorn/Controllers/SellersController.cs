using Microsoft.AspNetCore.Mvc;
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
    }
}
