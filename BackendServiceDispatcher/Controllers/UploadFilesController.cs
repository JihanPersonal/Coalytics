using BackendServiceDispatcher.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Controllers
{
    /// <summary>
    /// File Upload Endpoint
    /// </summary>
    [Produces("application/json")]
    [Route("api/UploadFiles")]
    public class UploadFilesController : Controller
    {
        private IBlobUploader BlobUploader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blobUploader"></param>
        public UploadFilesController(IBlobUploader blobUploader)
        {
            BlobUploader = blobUploader;
        }
        /// <summary>
        /// Upload files to Azure Blob
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            var userId = this.HttpContext.User.Identity;
            long size = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {                    
                    using (var stream = file.OpenReadStream())
                    {
                        await BlobUploader.UploadBlob(userId.Name, file.FileName, stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size});
        }
    }
}