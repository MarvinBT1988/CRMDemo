using CRM.DTOs.CustomerDTOs;

namespace CRM.AppWebBlazor.Data
{
    public class CustomerService
    {
        readonly HttpClient _httpClientCRMAPI;             
        public CustomerService(IHttpClientFactory httpClientFactory) {
            _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");
        }
        public async Task<SearchResultCustomerDTO> Search(SearchQueryCustomerDTO searchQueryCustomerDTO)
        {
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer/search", searchQueryCustomerDTO);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SearchResultCustomerDTO>();               
                return result ?? new SearchResultCustomerDTO();
            }
            return new SearchResultCustomerDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
        }

        public async Task<GetIdResultCustomerDTO> GetById(int id)
        {
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();
                return result ?? new GetIdResultCustomerDTO();
            }
            return new GetIdResultCustomerDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
        }

        public async Task<int> Create(CreateCustomerDTO createCustomerDTO)
        {
            int result = 0;
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer", createCustomerDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

        public async Task<int> Edit(EditCustomerDTO editCustomerDTO)
        {
            int result = 0;
            var response = await _httpClientCRMAPI.PutAsJsonAsync("/customer", editCustomerDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }
        public async Task<int> Delete(int id)
        {
            int result = 0;
            var response = await _httpClientCRMAPI.DeleteAsync("/customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }
    }
}
