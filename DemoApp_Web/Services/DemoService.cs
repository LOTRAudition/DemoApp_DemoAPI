using DemoApp_Utility;
using DemoApp_Web.Models;
using DemoApp_Web.Models.Dto;
using DemoApp_Web.Services.IServices;

namespace DemoApp_Web.Services
{
    public class DemoService : BaseService, IDemoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string demoUrl;

        public DemoService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            demoUrl = configuration.GetValue<string>("ServiceUrls:DemoAPI");

        }

        public Task<T> CreateAsync<T>(DemoCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = demoUrl + "/api/v1/demoAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = demoUrl + "/api/v1/demoAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = demoUrl + "/api/v1/demoAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = demoUrl + "/api/v1/demoAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(DemoUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = demoUrl + "/api/v1/demoAPI/" + dto.Id,
                Token = token
            }) ;
        }
    }
}
