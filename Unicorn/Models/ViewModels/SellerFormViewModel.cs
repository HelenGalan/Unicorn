namespace Unicorn.Models.ViewModels
{
    public class SellerFormViewModel //dados de uma tela de cadastro de vendedor
    {
        public Seller Seller { get; set; } //Seller no singular ajuda o framework a reconhecer os dados
        public ICollection<Department> Departments { get; set; } //Departments no plurar tambem ajuda o framework a reconhecer os dados
        //na hora de fazer a conversao dos dados HTTP para objeto, ele faz automaticamente, por isso os nomes precisam estar corretos


    }
}
