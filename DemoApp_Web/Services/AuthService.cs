using DemoApp_Utility;
using DemoApp_Web.Models;
using DemoApp_Web.Models.Dto;
using DemoApp_Web.Services.IServices;

namespace DemoApp_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string demoUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            demoUrl = configuration.GetValue<string>("ServiceUrls:DemoAPI");

        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = demoUrl + "/api/v1/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = demoUrl + "/api/v1/UsersAuth/register"
            });
        }
    }
}
