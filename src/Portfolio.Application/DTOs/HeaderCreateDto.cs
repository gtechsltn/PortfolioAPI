using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs
{
    public class HeaderCreateDto
    {
        public string PhoneNumber { get; set; }
        public IFormFile LogoImage { get; set; }
    }
}
