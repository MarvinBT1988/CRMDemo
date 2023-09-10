namespace CRM.DTOs.CustomerDTOs
{
    public class SearchQueryCustomerDTO
    {
        public string? Name_Like { get; set; }
        public string? LastName_Like { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }

        public byte SendRowCount { get; set; }
    }
}
