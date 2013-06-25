using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Configuration;
using System.IO;

namespace CloudSpeedTest.CloudProviders
{
    class S3CloudProviderStaticAuth : ICloudProvider
    {
        static string s3appkey = ConfigurationSettings.AppSettings["s3appkey"];
        static string s3secretkey = ConfigurationSettings.AppSettings["s3secretkey"];
        static string containername = ConfigurationSettings.AppSettings["s3containername"];
        static AmazonS3 client;

        public void Authenticate()
        {
            if (client != null)
            {
                return;
            }

            client = AWSClientFactory.CreateAmazonS3Client(s3appkey, s3secretkey);
        }

        public void WriteFile(Stream stream, string name)
        {
            var request = new PutObjectRequest().WithBucketName(containername).WithKey(name);
            request.InputStream = stream;
            client.PutObject(request);
        }
    }
}
