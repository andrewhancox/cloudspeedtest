using System.IO;
namespace CloudSpeedTest.CloudProviders
{
    interface ICloudProvider
    {
        void Authenticate();
        void WriteFile(Stream stream, string name);
    }
}
