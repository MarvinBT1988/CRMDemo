namespace CRM.DTOs.CustomerDTOs
{
    public class SearchResultCustomerDTO
    {                
        public int CountRow { get; set; }
        public List<CustomerDTO> Data { get; set; }
        public class CustomerDTO {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string? Address { get; set; }
        }
    }

}
