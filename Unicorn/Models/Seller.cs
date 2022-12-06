using System.ComponentModel.DataAnnotations;

namespace Unicorn.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} required!")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")] nao funciona por causa do Globalization que nao esta feito
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")] //para ter duas casas decimais (zero indica o valor do atributo)
        public double BaseSalary { get; set; }
        public Department Department { get; set; } //associacao varios para um
        public int DepartmentId { get; set; } //avisando o EF que esse ID tem que existir pois o tipo int nao pode ser nulo, aqui e a relacao referencial, chave estrangeira
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
        public double TotalSales(DateTime initial, DateTime final)
        {
            return SalesRecord.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
            //o where filtra a lista de vendas para criar uma nova lista contendo apenas as vendas no periodo de data informado
            //apos a filtragen, o Sum calcula a soma de Amount
            //usou a expressao lambda dentro da operavao LINQ
        }

    }
}
