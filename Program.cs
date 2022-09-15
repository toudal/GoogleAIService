using Microsoft.Extensions.Logging.EventLog;

using ECIT;

using GoogleAIService;

public class Program
{
  public static void Main(string[] args)
  {
    CreateHostBuilder(args).Build().Run();
  }

  public static IHostBuilder CreateHostBuilder(string[] pArgs) =>
      Host.CreateDefaultBuilder(pArgs)
          .ConfigureLogging(
              options => options.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information))
          .ConfigureServices((hostContext, services) =>
          {
            services.AddSingleton<GoogleAIService>();
            services.AddHostedService<WindowsBackgroundService>()
                  .Configure<EventLogSettings>(config =>
                  {
                    config.LogName = ".NET ECIT Solutions";
                    config.SourceName = "ECIT Solutions";
                  });
          }).UseWindowsService().UseContentRoot(ROOT_DIR);
}
