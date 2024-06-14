using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace Bookcase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<AppSettings>(Configuration.GetSection(AppSettings.SectionName));
            services.AddControllersWithViews();
            services.AddSingleton<IConfiguration>(Configuration);
            

            //services.AddMvc(options => options.EnableEndpointRouting = false);
            //services.AddOptions();

            services.AddRazorPages();
            services.AddSession();

            services.AddSignalR();

         

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        /* json daki ilk harlerin küçük olmamasý için kod parçacýðý */
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver();                       
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {                

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            });

            app.UseRouting();

            var cultureInfo = new CultureInfo("en-US")
            {
                NumberFormat =
                {
                    CurrencyDecimalSeparator = ",",
                    CurrencyGroupSeparator = ".",

                    NumberDecimalSeparator = ",",
                    NumberGroupSeparator = ".",

                    PercentDecimalSeparator = ",",
                    PercentGroupSeparator = "."
                }
            };

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
