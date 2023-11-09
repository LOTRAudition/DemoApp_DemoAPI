using DemoApp_DemoAPI.Data;
using DemoApp_DemoAPI.Models;
using DemoApp_DemoAPI.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DemoApp_DemoAPI.Repository
{
    public class DemoRepository : Repository<Demo>, IDemoRepository
    {
        private readonly ApplicationDbContext _db;
        public DemoRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

  
        public async Task<Demo> UpdateAsync(Demo entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Demos.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
