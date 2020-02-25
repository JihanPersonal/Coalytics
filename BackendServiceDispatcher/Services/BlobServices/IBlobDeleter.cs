using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Services
{
    public interface IBlobDeleter
    {
        Task<bool> DeleteBlob(string userId, string fileName);
    }
}
