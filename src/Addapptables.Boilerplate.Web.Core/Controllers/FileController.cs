using Abp.Authorization;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Addapptables.Boilerplate.Net.MimeTypes;
using Addapptables.Boilerplate.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FileController : BoilerplateControllerBase
    {

        private readonly IBinaryObjectManager _binaryObjectManager;

        public FileController(IBinaryObjectManager binaryObjectManager)
        {
            _binaryObjectManager = binaryObjectManager;
        }

        [HttpPost]
        [AbpAuthorize()]
        public async Task<ActionResult> SaveBinaryFile()
        {
            try
            {
                var binaryFile = Request.Form.Files.First();

                if (binaryFile == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                byte[] fileBytes;
                using (var stream = binaryFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var binaryObject = new BinaryObject(fileBytes, binaryFile.ContentType);
                await _binaryObjectManager.SaveAsync(binaryObject);
                return Json(new AjaxResponse(new { id = binaryObject.Id, fileType = binaryFile.ContentType }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [HttpGet]
        [AbpAuthorize()]
        public async Task<ActionResult> GetBinaryFile(GetFileDto file)
        {
            var binaryFile = await _binaryObjectManager.GetOrNullAsync(new Guid(file.Id));
            if (binaryFile == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            return File(binaryFile.Bytes, string.IsNullOrWhiteSpace(binaryFile.Type) ? MimeTypeNames.ImagePng : binaryFile.Type);
        }
    }
}
