using API_VMS.Data;
using API_VMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_VMS.Controllers
{
    [ApiController]
    [Route("vms/v1/api/servers/{serverId:Guid}")]
    public class VideosController : ControllerBase
    {
        public static IWebHostEnvironment _enviroment;
        public VideosController(IWebHostEnvironment environment)
        {
            _enviroment = environment;
        }

        /// <summary>
        /// Adicionar um novo video.
        /// </summary>
        /// <param name="novoVideo">Dados do novo video a ser adicionado.</param>
        /// <param name="serverId">Id do servidor que o video será adicionado.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("videos")]
        public async Task<ActionResult<VideoModel>> Adicionar([FromForm] NovoVideoModel novoVideo, Guid serverId)
        {
            if (ModelState.IsValid && novoVideo.Video.Length > 0)
            {
                var context = new DatabaseContext();
                var dadosVideo = new VideoModel();

                var upload = new VideoUploadClass(_enviroment);

                dadosVideo.Video = novoVideo.Video;
                dadosVideo.DataInclusao = DateTime.Now;
                dadosVideo.Diretorio = _enviroment.WebRootPath + "\\UploadVideo\\" + novoVideo.Video.FileName;
                dadosVideo.Descricao = novoVideo.Descricao;
                dadosVideo.ServidorId = serverId;

                await upload.UpparVideo(dadosVideo.Video);                

                context.Videos.Add(dadosVideo);
                await context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Remover um video específico.
        /// </summary>
        /// <param name="Id">ID do video que deseja remover.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("videos/{Id}")]
        public async Task<ActionResult<VideoModel>> Remover(Guid Id)
        {
            var context = new DatabaseContext();

            var videoARemover = context.Videos.Find(Id);

            if (videoARemover != null)
            {
                context.Videos.Remove(videoARemover);
                await context.SaveChangesAsync();

                return videoARemover;
            }
            else
            {
                return NotFound(Id);
            }
        }

        /// <summary>
        /// Buscar dados de um video específico.
        /// </summary>
        /// <param name="Id">ID do video que deseja buscar.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("videos/{Id}")]
        public ActionResult<VideoModel> Recuperar(Guid Id)
        {
            var context = new DatabaseContext();

            var videoBuscado = context.Videos.Find(Id);

            if (videoBuscado != null)
            {
                return videoBuscado;
            }
            else
            {
                return NotFound(ModelState);
            }
        }

        /// <summary>
        /// Listar todos os videos de um servidor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("videos")]
        public IEnumerable<VideoModel> Listar(Guid serverId)
        {
            var context = new DatabaseContext();

            var listaVideos = context.Videos.Where(v => v.ServidorId == serverId);

            return listaVideos;
        }



    }
}
