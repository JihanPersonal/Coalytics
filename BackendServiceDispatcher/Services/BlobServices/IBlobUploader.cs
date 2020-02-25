using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Services
{
    public interface IBlobUploader
    {
        Task UploadBlob(string userId, string fileName, Stream source);
    }
}
