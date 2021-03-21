using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_VMS.Models
{
    public class ServidorModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string EnderecoIp { get; set; }
        public int PortaIp { get; set; }

    }
}
