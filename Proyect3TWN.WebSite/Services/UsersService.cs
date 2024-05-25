    using System.Text;
    using Newtonsoft.Json;
    using Proyect2TWM.Api.Dto;
    using Proyect2TWM.Core.Http;
    using Proyect3TWN.WebSite.Services.Interfaces;

    namespace Proyect3TWN.WebSite.Services;

    public class UsersService : IUsersService
    {
           public readonly string _baseURL = "http://localhost:5209/";
        private readonly string _endpoint = "api/Users";
        
        public async Task<Response<List<UsersDto>>> GetAllAsync()
        {
            var url = $"{_baseURL}{_endpoint}";
            var cliente = new HttpClient();
            var res = await cliente.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<Response<List<UsersDto>>>(json);
            
            return response;
        }
        
        public async Task<Response<UsersDto>> GetById(int id)
        {
            var url = $"{_baseURL}{_endpoint}/{id}";
            var cliente = new HttpClient();
            var res = await cliente.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Response<UsersDto>>(json);
            return response;
        }

        public async Task<Response<UsersDto>> SaveAsync(UsersDto usersDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(usersDto);
            var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var res = await client.PostAsync(url, content);
            var json = await res.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<Response<UsersDto>>(json);

            return response;
        }

     
        public async Task<Response<UsersDto>> UpdateAsync(UsersDto usersDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(usersDto);
            var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var res = await client.PutAsync(url, content);
            var json = await res.Content.ReadAsStringAsync();
            
            var response = JsonConvert.DeserializeObject<Response<UsersDto>>(json);

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
        
        
        public async Task<Response<LoginDto>> AuthenticateAsync(string email, string password)
        {
            var url = $"{_baseURL}{_endpoint}/login";
            var loginDto = new LoginDto { Email = email, Password = password };
            var jsonRequest = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var res = await client.PostAsync(url, content);
            var json = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Response<LoginDto>>(json);
            return response;
        }
    }
