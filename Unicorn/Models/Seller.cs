namespace Unicorn.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; } //associacao varios para um
        public ICollection<SalesRecord> SalesRecord { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //operacao para adicionar uma venda na lista de vendas
        public void AddSales(SalesRecord sales) //recebe como argumento o sales
        {
            SalesRecord.Add(sales); //chama a operacao Add recebendo o sales
        }

        //agora e para remover uma venda do vendedor
        public void RemoveSales(SalesRecord sales)
        {
            SalesRecord.Remove(sales); //passando sales como argumento
        }

        //recebe uma data inicial e final e retorna o total de vendas naquele periodo
        public double TotalSales (DateTime initial, DateTime final)
        {
            return SalesRecord.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
            //o where filtra a lista de vendas para criar uma nova lista contendo apenas as vendas no periodo de data informado
            //apos a filtragen, o Sum calcula a soma de Amount
            //usou a expressao lambda dentro da operavao LINQ
        }

    }
}
