using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_VMS.Models
{
    public class VideoModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInclusao { get; set; }
        public string Diretorio { get; set; }
        
        public Guid ServidorId { get; set; }
        
        [ForeignKey("ServidorId")]
        public ServidorModel Servidor { get; set; }
        
        [NotMapped]
        public IFormFile Video { get; set; }
    }
}
