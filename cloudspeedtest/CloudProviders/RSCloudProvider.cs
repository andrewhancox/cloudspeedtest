using net.openstack.Core.Exceptions.Response;
using net.openstack.Providers.Rackspace;
using net.openstack.Providers.Rackspace.Objects;
using System;
using System.Configuration;
using System.IO;

namespace CloudSpeedTest.CloudProviders
{
    class RSCloudProvider : ICloudProvider
    {
        static string username = ConfigurationSettings.AppSettings["rsusername"];
        static string apikey = ConfigurationSettings.AppSettings["rsapikey"];
        static string containername = ConfigurationSettings.AppSettings["rscontainername"];
        CloudFilesProvider cloudFilesProvider;

        public void Authenticate()
        {
            try
            {
                cloudFilesProvider = new CloudFilesProvider(new RackspaceCloudIdentity
                {
                    Username = username,
                    APIKey = apikey,
                    CloudInstance = CloudInstance.UK
                });
            }
            catch (ResponseException ex2)
            {
                throw new Exception("Auth Failed : " + ex2.ToString());
            }
        }

        public void WriteFile(Stream stream, string name)
        {
            cloudFilesProvider.CreateObject(containername, stream, name);
        }
    }
}
