using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebAppDBTestDocker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(o => o.ValidateScopes = false)
                .Build();
    }
}
