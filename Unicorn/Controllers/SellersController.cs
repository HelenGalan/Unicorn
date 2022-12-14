using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Unicorn.Models;
using Unicorn.Models.ViewModels;
using Unicorn.Services;
using Unicorn.Services.Exceptions;

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

        public async Task<IActionResult> Index() //chamei o controlador
        {
            var list = await _sellerService.FindAllAsync(); //o controlador acessou meu model e colocou o dado em "list"
            return View(list); //encaminhou os dados para a view
            //dinamica do MVC
        }

        public async Task<IActionResult> Create()
        {
            //primeiro precisa carregar os departamentos, entao usa o FindAll pra ele buscar no BD todos os departamentos no servico que a gente acabou de criar
            var departments = await _departmentService.FindAllAsync();
            //Instanciar o objeto do ViewModel
            var viewModel = new SellerFormViewModel { Departments = departments };
            //passa o objeto viewModel para a view
            return View(viewModel);
            //agora a tela de cadastro quando for acionada pela primeira vez, vai receber o objeto viewModel com os departamentos populados
        }

        [HttpPost] //anotacao para indicar que essa acao e uma acao de post
        [ValidateAntiForgeryToken] //anotacao para previnir ataque CSRF
        //CSRF e quando alguem se aproveita da sua sessao de autenticacao e envia dados maliciosos

        //recebe o objeto vendedor que veio na requisicao e instancia
        public async Task<IActionResult> Create(Seller seller)
        {
            //vai ficar aqui nesse if enquanto o usuario nao preencher direito o formulario
            //testar se seller e valido
            //teste se o modelo foi validado, se nao for, retorna a mesma view ate o usuario consertar
            //esse teste e pra garantir se o java script estiver desativado no navegador
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            //implementacao a acao de inserir no banco de dados
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index)); //redireciona para a acao Index que e inicial do CRUD de vendedores
            //o nameof facilita a manutencao, porque se a string mudar um dia, nao precisa mudar aqui
        }

        //simplesmente abre uma tela de confirmacao, nao deleta de fato
        public async Task<IActionResult> Delete (int? id) //o ? quer dizer opcional, entao recebe um int opcional
        {
            if (id == null) //testando se o id e nulo, se for nulo, significa que a requisicao foi feita de forma indevida
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" }); //objeto notfounf instancia uma resposta basica mas podemos persinalizar para uma pagina de erro
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); //pegar o objeto que estou mandando deletar, no banco de dados
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found!" }); //se o ojjeto for nulo significa que nao existe, entao tambem retorna notfound
            }

            //agora sim se tudo deu certo, retorna a view passando o objeto como argumento
            return View(obj);
        }

        // acao que de fato deleta quando se clica no botao delete da tela de confirmacao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id); //deletou o vendedor
                return RedirectToAction(nameof(Index)); //depois que deletou, vai redirecionar pra tela inicial de listagem vendedor do CRUD

            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new {message = e.Message});
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            //mesma logica que o Delete

            if (id == null) //testando se o id e nulo, se for nulo, significa que a requisicao foi feita de forma indevida
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" }); //objeto notfounf instancia uma resposta basica mas podemos persinalizar para uma pagina de erro
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); //pegar o objeto que estou mandando deletar, no banco de dados
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found!" }); ; //se o ojjeto for nulo significa que nao existe, entao tambem retorna notfound
            }

            //agora sim se tudo deu certo, retorna a view passando o objeto como argumento
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id) //essa acao serve pra abrir a tela pra editar o vendedor
        {
            if (id == null) //testei se o ID nao existe
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) //testei se e nulo
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found!" });
            }

            //se tudo passar, ai sim abre a tela de edicao, pra isso precisa carregar os departamentos pra povoar a caixa de selecao
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            //se nao for valido, retorna a mesma view com o objeto
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id) //testar se o id que veio no parametro do metodo for diferente do seller.id, da url da requisicao, significa que algo esta errado
            {
                return RedirectToAction(nameof(Error), new { message = "Id doens't match!" });
            } //se passar, significa que esta ok

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index)); //redireciona para a pagina iniciar do CRUD
            }

            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            //catch(DbConcurrencyException e) //commentei porque o redirecionamento assima ja trata esse tambem
            //{
            //    return RedirectToAction(nameof(Error), new { message = "Id not found!" });
            //}
        }

        public IActionResult Error (string message) //acao de erro
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //macete pra pegar o ID interno da requisicao
            };
            return View(viewModel);
        }
    }
}
