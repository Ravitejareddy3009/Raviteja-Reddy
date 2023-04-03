using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new Microsoft.AspNetCore.Hosting.WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .Build();
 
            host.Run();
            
        }
    }
}
