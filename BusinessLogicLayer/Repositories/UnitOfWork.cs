using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;

namespace Talabat.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationStoreDbContext _context;
        private Hashtable _repostories;

        public UnitOfWork(ApplicationStoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> Complete()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repostories == null) _repostories = new Hashtable();

            var type = typeof(TEntity).Name;

            if(!_repostories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repostories.Add(type, repository);
            }

            return (IGenericRepository<TEntity>) _repostories[type];
        }
    }
}
