using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BackendServiceDispatcher.Services
{
    public class BlobUploader : IBlobUploader
    {
        private CloudStorageAccount Account { get; set; }
        private CloudBlobClient Client { get; set; }

        public BlobUploader(CloudStorageAccount account)
        {
            Account = account;
            Client = Account.CreateCloudBlobClient();
        }

        public async Task UploadBlob(string userId, string fileName, Stream source)
        {
            CloudBlobContainer container = Client.GetContainerReference(userId);
            await container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            await blob.UploadFromStreamAsync(source);
        }

    }
}
