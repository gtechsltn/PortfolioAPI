using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Header> HeaderRepository { get; }
        IGenericRepository<About> AboutRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
