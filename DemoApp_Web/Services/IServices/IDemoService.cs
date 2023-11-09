using DemoApp_Web.Models.Dto;

namespace DemoApp_Web.Services.IServices
{
    public interface IDemoService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(DemoCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(DemoUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
