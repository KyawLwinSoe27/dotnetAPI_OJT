namespace PracticeApi.Models
{
    public class CustomerResult
    {
        public int Id {get; set;}
        public string CustomerName { get; set; } = string.Empty;
        public DateTime CustomerRegisterDate { get; set; }
        public string CustomerAddress { get; set; } = string.Empty;
        public int? CustomerTypeId { get; set; }
        public string? CustomerTypeName { get; set; }
    }
}