using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Specification;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationStoreDbContext _context;

        public GenericRepository(ApplicationStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();
        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecifications(spec).ToListAsync();

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }


        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }
        // To Get Count Product in API
        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }


        public void Add(T entity)
           => _context.Set<T>().Add(entity);
        public void Update(T entity)
            => _context.Set<T>().Update(entity);
        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);
    }
}
