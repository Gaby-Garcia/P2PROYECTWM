using Newtonsoft.Json;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;
using Proyect3TWN.WebSite.Services.Interfaces;

namespace Proyect3TWN.WebSite.Services;

public class VacationsAbsencesService : IVacationsAbsencesService
{
       public readonly string _baseURL = "http://localhost:5209/";
    private readonly string _endpoint = "api/VacationsAbsences";
    
    public async Task<Response<List<VacationsAbsencesDto>>> GetAllAsync()
    {
        var url = $"{_baseURL}{_endpoint}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<List<VacationsAbsencesDto>>>(json);
        
        return response;
    }
    
    public async Task<Response<VacationsAbsencesDto>> GetById(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<VacationsAbsencesDto>>(json);
        return response;
    }

    public async Task<Response<VacationsAbsencesDto>> SaveAsync(VacationsAbsencesDto vacationsAbsencesDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(vacationsAbsencesDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<VacationsAbsencesDto>>(json);

        return response;
    }

 
    public async Task<Response<VacationsAbsencesDto>> UpdateAsync(VacationsAbsencesDto vacationsAbsencesDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(vacationsAbsencesDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<VacationsAbsencesDto>>(json);

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