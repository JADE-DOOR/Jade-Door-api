using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace JADE_DOOR.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // ① 强制加载 appsettings.json（最重要）
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                    // ② 加载环境专用的 appsettings.Development.json（如果存在）
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile(
                        $"appsettings.{env.EnvironmentName}.json",
                        optional: true,
                        reloadOnChange: true
                    );

                    // ③ 加载环境变量
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
