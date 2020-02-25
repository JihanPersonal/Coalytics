using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BackendServiceDispatcher.Services
{
    public class BlobDeleter : IBlobDeleter
    {
        private CloudStorageAccount Account { get; set; }
        private CloudBlobClient Client { get; set; }

        public BlobDeleter(CloudStorageAccount account)
        {
            Account = account;
            Client = Account.CreateCloudBlobClient();
        }
        public async Task<bool> DeleteBlob(string userId, string fileName)
        {
            CloudBlobContainer container = Client.GetContainerReference(userId);
            if (await container.ExistsAsync())
            {
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
                return await blob.DeleteIfExistsAsync();
            }
            else
            {
                return false;
            }
        }
    }
}
