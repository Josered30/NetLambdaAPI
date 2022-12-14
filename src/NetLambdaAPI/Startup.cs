namespace NetLambdaAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(
            swaggerGenOptions =>
                swaggerGenOptions.SwaggerDoc(
                    "v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "NetLambdaAPI",
                        Version = "0.0.1"
                    }
                )
        );
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "NetLambdaAPI"));
        }

        app.UseHttpsRedirection();

        app.UsePathBase(new PathString("/api"));
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet(
                "/",
                async context =>
                {
                    await context.Response.WriteAsync(
                        "Welcome to running ASP.NET Core on AWS Lambda"
                    );
                }
            );
        });
    }
}
