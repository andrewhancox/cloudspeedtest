using CloudSpeedTest.CloudProviders;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace CloudSpeedTest
{
    class Program
    {

        static int uploadcount = 0;
        static DateTime starttime = DateTime.Now;
        static Timer t;

        static void Main(string[] args)
        {
            t = new Timer(10000);
            t.Elapsed += new ElapsedEventHandler(MeasureSpeed);
            t.Enabled = true;

            ICloudProvider provider = new RSCloudProvider();

            provider.Authenticate();

            Parallel.For(0, 10000, j =>
            {
                using (var stream = System.IO.File.OpenRead("C:\\projects\\temp\\boat.jpg"))
                {
                    var name = Guid.NewGuid().ToString();
                    provider.WriteFile(stream,name);
                    uploadcount++;
                }
            });

            Console.ReadLine();
        }

        static void MeasureSpeed(object source, ElapsedEventArgs e)
        {
            var t1 = DateTime.Now;
            var filespersecond = uploadcount / ((t1 - starttime).TotalSeconds);
            Console.WriteLine(filespersecond);

            //reset time + count
            uploadcount = 0;
            starttime = DateTime.Now;
        }
    }
}
