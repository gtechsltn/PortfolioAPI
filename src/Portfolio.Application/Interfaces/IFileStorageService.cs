using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveOrReplaceAsync(IFormFile file, string folderName);
    }
}
