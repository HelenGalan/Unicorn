using Microsoft.AspNetCore.Mvc;
using Unicorn.Models;
using Unicorn.Models.ViewModels;
using Unicorn.Services;

namespace Unicorn.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService; //declaracao da dependencia para o servico SellerService
        private readonly DepartmentService _departmentService; //dependencia para o servico de DepartmentService

        public SellersController(SellerService sellerService, DepartmentService departmentService) //construtor para injetar a dependencia no objeto
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index() //chamei o controlador
        {
            var list = _sellerService.FindAll(); //o controlador acessou meu model e colocou o dado em "list"
            return View(list); //encaminhou os dados para a view
            //dinamica do MVC
        }

        public IActionResult Create()
        {
            //primeiro precisa carregar os departamentos, entao usa o FindAll pra ele buscar no BD todos os departamentos no servico que a gente acabou de criar
            var departments = _departmentService.FindAll();
            //Instanciar o objeto do ViewModel
            var viewModel = new SellerFormViewModel { Departments= departments };
            //passa o objeto viewModel para a view
            return View(viewModel);
            //agora a tela de cadastro quando for acionada pela primeira vez, vai receber o objeto viewModel com os departamentos populados
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
