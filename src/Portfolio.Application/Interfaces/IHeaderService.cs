using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces
{
    public interface IHeaderService
    {
        Task AddHeaderAsync(HeaderCreateDto dto);
        Task<HeaderViewDto?> GetHeaderAsync();
    }
}
