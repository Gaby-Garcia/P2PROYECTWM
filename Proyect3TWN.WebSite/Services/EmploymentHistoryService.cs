using Newtonsoft.Json;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Services;

public class EmploymentHistoryService : IEmploymentHistoryService
{
    public readonly string _baseURL = "http://localhost:5209/";
    private readonly string _endpoint = "api/EmploymentHistory";
    
    public async Task<Response<List<EmploymentHistoryDto>>> GetAllAsync()
    {
        var url = $"{_baseURL}{_endpoint}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<List<EmploymentHistoryDto>>>(json);
        
        return response;
    }
    
    public async Task<Response<EmploymentHistoryDto>> GetById(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<EmploymentHistoryDto>>(json);
        return response;
    }

    public async Task<Response<EmploymentHistoryDto>> SaveAsync(EmploymentHistoryDto employmentHistoryDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(employmentHistoryDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<EmploymentHistoryDto>>(json);
        return response;
    }

 
    public async Task<Response<EmploymentHistoryDto>> UpdateAsync(EmploymentHistoryDto employmentHistoryDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(employmentHistoryDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<EmploymentHistoryDto>>(json);

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var cliente = new HttpClient();
        var res = await cliente.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<bool>>(json);
        return response;
    }
}