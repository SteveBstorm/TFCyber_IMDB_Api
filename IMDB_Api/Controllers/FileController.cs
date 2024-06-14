using IMDB_Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {

            HttpRequest req = HttpContext.Request;
            Guid d = Guid.NewGuid();
            string filename = d+req.Form.Files[0].FileName;

            if (!Directory.Exists("upload")) Directory.CreateDirectory("upload");

            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "upload/", filename);

            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                await req.Form.Files[0].CopyToAsync(fs);

                return StatusCode(200, "https://localhost:7158/upload/" + filename);
            }

            //permets de supprimer un fichier pour éviter les doublon de nom
            //if (System.IO.File.Exists(filename))
            //{
            //    System.IO.File.Delete(filename);
            //}
        }
    }
}
