using System;
using Azure.Storage.Blobs;

namespace ConsumeStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var blobUriBuilder = new BlobUriBuilder(new Uri(""));

            var blobServiceClient = new BlobServiceClient("");
            var containerClient = blobServiceClient.GetBlobContainerClient("");
            BlobClient blobClient1 = containerClient.GetBlobClient("");



            var blobClient = new BlobClient("", "", "");
            

            
        }
    }
}
