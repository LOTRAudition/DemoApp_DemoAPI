using DemoApp_DemoAPI.Data;
using DemoApp_DemoAPI.Models;
using DemoApp_DemoAPI.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DemoApp_DemoAPI.Repository
{
    public class DemoNumberRepository : Repository<DemoNumber>, IDemoNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public DemoNumberRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

  
        public async Task<DemoNumber> UpdateAsync(DemoNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.DemoNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
