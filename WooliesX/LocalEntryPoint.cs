using System.IO;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace WooliesX
{
    public class LocalEntryPoint
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>().UseNLog()
                .UseUrls("http://localhost:5002")
                .Build();

            host.Run();
        }
    }
}
