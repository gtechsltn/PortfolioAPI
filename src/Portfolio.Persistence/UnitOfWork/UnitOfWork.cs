using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Persistence.Context;
using Portfolio.Persistence.Repositories;

namespace Portfolio.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            HeaderRepository = new GenericRepository<Header>(_context);
            AboutRepository = new GenericRepository<About>(_context);
        }

        public IGenericRepository<Header> HeaderRepository { get; }

        public IGenericRepository<About> AboutRepository { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
