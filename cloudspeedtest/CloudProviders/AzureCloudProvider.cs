using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpeedTest.CloudProviders
{
    class AzureCloudProvider : ICloudProvider
    {
        static string azureconnstring = ConfigurationSettings.AppSettings["azureconnstring"];
        static string containername = ConfigurationSettings.AppSettings["azurecontainername"];

        CloudBlobContainer container;

        public void Authenticate()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureconnstring);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(containername);
        }

        public void WriteFile(System.IO.Stream stream, string name)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            blockBlob.UploadFromStream(stream);
        }
    }
}
