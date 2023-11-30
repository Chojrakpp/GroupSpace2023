using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GroupSpace2023.Data;
using Microsoft.AspNetCore.Identity;
using GroupSpace2023.Areas.Identity.Data;
namespace GroupSpace2023
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = (builder.Configuration.GetConnectionString("GroupSpace2023Context"));

            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string 'GroupSpace2023Context' not found.")));

            builder.Services.AddDefaultIdentity<GroupSpace2023User>(options => options.SignIn.RequireConfirmedAccount = false) // zet op true om confirmatie via mail te krijgen
                .AddRoles<IdentityRole>() // zet zelf erbij
                .AddEntityFrameworkStores<MyDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // ADD CONFIGURATIE OP CANVAS STAAT UITGELEGT

            //builder.Services.Configure();
            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); // zelf bij gezet anders app werkte niet

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope()) // Zelf geschreven door Waldo 
            {
                var services = scope.ServiceProvider;
                MyDbContext context = new MyDbContext(services.GetRequiredService<DbContextOptions<MyDbContext>>());
                var UserManager = services.GetRequiredService<UserManager<GroupSpace2023User>>();
                await MyDbContext.DataInitializer(context, UserManager); // maak async op main om eerst dit te maken voor de run
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages(); //zelf schrijven omdat geen controller in Identity mvvm 
            app.Run();
        }
    }
}
