using DemoApp_DemoAPI.Models;
using System.Linq.Expressions;

namespace DemoApp_DemoAPI.Repository.IRepostiory
{
    public interface IDemoRepository : IRepository<Demo>
    {
      
        Task<Demo> UpdateAsync(Demo entity);
  
    }
}
