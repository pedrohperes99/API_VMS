using API_VMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_VMS.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ServidorModel> Servidores { get; set; }

        public DbSet<VideoModel> Videos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:srvdbph.database.windows.net,1433;Initial Catalog=APIVMS;Persist Security Info=False;User ID=*****;Password=*****;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
