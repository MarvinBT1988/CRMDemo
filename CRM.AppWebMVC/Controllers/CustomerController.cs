using Microsoft.AspNetCore.Mvc;
using CRM.DTOs.CustomerDTOs;
using System.Text.Json;
using System.Reflection;

namespace CRM.AppWebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClientCRMAPI;
        public CustomerController(IHttpClientFactory httpClientFactory) {           
            _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");
        }
        // GET: CustomerController
        public async Task<IActionResult> Index(SearchQueryCustomerDTO searchQueryCustomerDTO, int CountRow=0)
        {
            if (searchQueryCustomerDTO.SendRowCount == 0)
                searchQueryCustomerDTO.SendRowCount = 2;
            if (searchQueryCustomerDTO.Take == 0)
                searchQueryCustomerDTO.Take = 10;
            var result = new SearchResultCustomerDTO();
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer/search",searchQueryCustomerDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<SearchResultCustomerDTO>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                result = result != null ? result : new SearchResultCustomerDTO();
            }
            if (result.CountRow == 0 && searchQueryCustomerDTO.SendRowCount == 1)
                result.CountRow = CountRow;
            ViewBag.CountRow = result.CountRow;
            searchQueryCustomerDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryCustomerDTO;
            return View(result);
        }       

        // GET: CustomerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = new GetIdResultCustomerDTO();
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<GetIdResultCustomerDTO>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return View(result);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerDTO createCustomerDTO)
        {
            try
            {
                var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer", createCustomerDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }                    
                ViewBag.Error = "Error al intentar guardar el registro";
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = new GetIdResultCustomerDTO();
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<GetIdResultCustomerDTO>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                result=result!=null? result: new GetIdResultCustomerDTO();
            }
            return View(new EditCustomerDTO(result));
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCustomerDTO editCustomerDTO)
        {
            try
            {
                var response = await _httpClientCRMAPI.PutAsJsonAsync("/customer", editCustomerDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = "Error al intentar editar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = new GetIdResultCustomerDTO();
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<GetIdResultCustomerDTO>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                result = result != null ? result : new GetIdResultCustomerDTO();
            }
            return View(result);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GetIdResultCustomerDTO getIdResultCustomerDTO)
        {
            try
            {
                var response = await _httpClientCRMAPI.DeleteAsync("/customer/"+id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = "Error al intentar eliminar el registro";
                return View(getIdResultCustomerDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(getIdResultCustomerDTO);
            }
        }
    }
}
