using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Services
{
    public interface IBlobDownloader
    {
        Task<string> DownloadBlob(string userId, string fileName);
    }
}
