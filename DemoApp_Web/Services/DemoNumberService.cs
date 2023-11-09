using DemoApp_Utility;
using DemoApp_Web.Models;
using DemoApp_Web.Models.Dto;
using DemoApp_Web.Services.IServices;
using Newtonsoft.Json.Linq;

namespace DemoApp_Web.Services
{
    public class DemoNumberService : BaseService, IDemoNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string demoUrl;

        public DemoNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            demoUrl = configuration.GetValue<string>("ServiceUrls:DemoAPI");

        }

        public Task<T> CreateAsync<T>(DemoNumberCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = demoUrl + "/api/v1/demoNumberAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = demoUrl + "/api/v1/demoNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = demoUrl + "/api/v1/demoNumberAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = demoUrl + "/api/v1/demoNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(DemoNumberUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = demoUrl + "/api/v1/demoNumberAPI/" + dto.DemoNo,
                Token = token
            }) ;
        }
    }
}
