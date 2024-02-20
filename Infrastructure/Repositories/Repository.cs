using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        private readonly DDDContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public Repository(DDDContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<T> GetById(int id)
        {
            
            return
               await _context.Set<T>().FindAsync(id);

        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Add(entity);

            return entity;
        }

    }
}
