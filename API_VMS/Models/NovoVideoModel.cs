using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_VMS.Models
{
    public class NovoVideoModel
    {
        public string Descricao { get; set; }
        [Required]
        public IFormFile Video { get; set; }
    }
}
