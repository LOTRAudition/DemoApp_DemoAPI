using DemoApp_DemoAPI.Models;
using System.Linq.Expressions;

namespace DemoApp_DemoAPI.Repository.IRepostiory
{
    public interface IDemoNumberRepository : IRepository<DemoNumber>
    {
      
        Task<DemoNumber> UpdateAsync(DemoNumber entity);
  
    }
}
