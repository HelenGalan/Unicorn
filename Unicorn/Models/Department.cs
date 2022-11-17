namespace Unicorn.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
        //associacao (um para varios)
        //ja instancia para garantir que a lista e instanciada

        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller); //pega a lista de vendedores e adiciona nela esse vendedor do argumento
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
            //pega cada vendedor da minha lista
            //chama o totalSales do vendedor no periodo inicial e final
            //ai faz uma foma de todos os vendedores do meu departamento
        }
    }
}
