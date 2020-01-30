namespace LoggingProject
{
    using LoggingLibrary.Domain;
    using LoggingLibrary.Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Sinks.MSSqlServer;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var logDB = @"Server=localhost; Database= Logging; Integrated Security=True;";
            var logTable = "Logs";
            var options = new ColumnOptions();
            options.Store.Remove(StandardColumn.Properties);
            options.Store.Add(StandardColumn.LogEvent);
            options.LogEvent.DataLength = 2048;
            options.PrimaryKey = options.TimeStamp;
            options.TimeStamp.NonClusteredIndex = true;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    connectionString: logDB,
                    tableName: logTable,
                    autoCreateSqlTable: true,
                    columnOptions: options
                ).WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                                .CreateLogger();

            services.AddSingleton<ILoggerSerilog, LoggerSerilog>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
