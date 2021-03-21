using API_VMS.Data;
using API_VMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace API_VMS.Controllers
{
    [ApiController]
    [Route("vms/v1")]
    public class ServersController : ControllerBase
    {
        private readonly ILogger<ServersController> _logger;

        public ServersController(ILogger<ServersController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Criar um novo servidor.
        /// </summary>
        /// <param name="dadosServidor">Dados do novo servidor a ser criado.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/server")]
        public async Task<ActionResult<ServidorModel>> Criar(ServidorModel dadosServidor)
        {
            if (ModelState.IsValid)
            {
                var context = new DatabaseContext();

                context.Servidores.Add(dadosServidor);
                await context.SaveChangesAsync();

                return dadosServidor;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Atualizar um servidor existente.
        /// </summary>
        /// <param name="dadosServidor">Dados do servidor a ser atualizado.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/server/{Id}")]
        public async Task<ActionResult<ServidorModel>> Atualizar(Guid Id, ServidorModel dadosServidor)
        {
            if (ModelState.IsValid)
            {
                var context = new DatabaseContext();

                var servidorAAtualizar = context.Servidores.Find(Id);

                if (servidorAAtualizar == null)
                {
                    return NotFound(ModelState);
                }
                else
                {
                    servidorAAtualizar.EnderecoIp = dadosServidor.EnderecoIp;
                    servidorAAtualizar.Nome = dadosServidor.Nome;
                    servidorAAtualizar.PortaIp = dadosServidor.PortaIp;
                    context.Servidores.Update(servidorAAtualizar);
                    await context.SaveChangesAsync();

                    return servidorAAtualizar;
                }
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        /// <summary>
        /// Remover um servidor específico.
        /// </summary>
        /// <param name="Id">ID do servidor que deseja remover.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/servers/{Id}")]
        public async Task<ActionResult<ServidorModel>> Remover(Guid Id)
        {
            var context = new DatabaseContext();

            var servidorARemover = context.Servidores.Find(Id);

            if (servidorARemover != null)
            {
                context.Servidores.Remove(servidorARemover);
                await context.SaveChangesAsync();

                return servidorARemover;
            }
            else
            {
                return NotFound(Id);
            }
        }

        /// <summary>
        /// Buscar dados de um servidor específico.
        /// </summary>
        /// <param name="Id">ID do servidor que deseja buscar.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/servers/{Id}")]
        public ActionResult<ServidorModel> Recuperar(Guid Id)
        {
            var context = new DatabaseContext();

            var servidorBuscado = context.Servidores.Find(Id);

            if (servidorBuscado != null)
            {
                return servidorBuscado;
            }
            else
            {
                return NotFound(ModelState);
            }
        }

        /// <summary>
        /// Obter todos os servidores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/servers")]
        public IEnumerable<ServidorModel> Listar()
        {
            var context = new DatabaseContext();

            var listaServidores = context.Servidores.ToList();

            return listaServidores;
        }

        /// <summary>
        /// Obter todos os servidores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/servers/available/{Id}")]
        public ActionResult<HttpResponseMessage> Disponibilidade(Guid Id)
        {
            var context = new DatabaseContext();
            var response = new HttpResponseMessage();

            var dadosServidorConsultado = context.Servidores.Find(Id);

            if (dadosServidorConsultado == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(response);
            }

            var ping = new Ping();

            var retorno = ping.Send(dadosServidorConsultado.EnderecoIp, 30000);
            
            response.ReasonPhrase = $"Status do servidor {dadosServidorConsultado.EnderecoIp}: {retorno.Status}";
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return response;
        }
    }
}
