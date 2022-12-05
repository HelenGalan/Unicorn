namespace Unicorn.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string Message { get; set; } //ter condicao de acrescentar uma mensagem customizada nesse objeto

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}