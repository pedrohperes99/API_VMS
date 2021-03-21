using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_VMS
{
    public class VideoUploadClass
    {
        public static IWebHostEnvironment _enviroment;

        public VideoUploadClass(IWebHostEnvironment environment)
        {
            _enviroment = environment;
        }

        public IFormFile files { get; set; }

        public async Task<string> UpparVideo(IFormFile objFile)
        {
            if(objFile.Length > 0)
            {
                if (!Directory.Exists(_enviroment.WebRootPath + "\\UploadVideo\\"))
                {
                    Directory.CreateDirectory(_enviroment.WebRootPath + "\\UploadVideo\\");
                }

                using (FileStream fileStream = File.Create(_enviroment.WebRootPath + "\\UploadVideo\\" + objFile.FileName))
                {
                    await objFile.CopyToAsync(fileStream);
                    fileStream.Flush();
                    return "\\UploadVideo\\" + objFile.FileName;
                }
            }
            else
            {
                return "Selecione um arquivo válido!";
            }
        }
    }
}
