using Serilog;
using Serilog.Events;

namespace ProductWebAPI.Models
{
    public class LoggerSetup
    {
        public static async Task SetupLoggerAsync()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() //2.6 MinimumLevel
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information) //зміни рівня логування для Microsoft-логів
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Seq("http://localhost:7021") //5 
            .CreateLogger();
            
            Log.Information("Hello, Serilog!");//2.2 Information 
            Log.Warning("Something unexpected happened");//2.3 Warning
            Log.Fatal("The application encountered a fatal error and must exit");//2.5 Fatal
            //3 
            var user = new User { FirstName = "John", LastName = "Jonson" };
            Log.Information("User {@User} logged in at {LoginTime}", user, DateTime.Now);

            int a = 10, b = 2;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);//2.1 Debug
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");//2.4 Error
            }
            finally
            {
                await Log.CloseAndFlushAsync();
            }
        }
    }
}
