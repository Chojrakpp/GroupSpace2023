using GroupSpace2023.Areas.Identity.Data;
using GroupSpace2023.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroupSpace2023.Data;

public class MyDbContext : IdentityDbContext<GroupSpace2023User>
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    // verander de naam van normale context naar mydbcontext om een verwijzing te maken naar de data context in identity
    // NIET MEER NODIG KAN VERWIJDERD WORDEN de data dat in file zat zonder identity

    public DbSet<GroupSpace2023.Models.Groep> Groeps { get; set; } = default!;
    public DbSet<GroupSpace2023.Models.Message> Message { get; set; } = default!;

    public static async Task DataInitializer(MyDbContext context, UserManager<GroupSpace2023User> userManager) //Maak heel methode async
    {
        if (!context.Users.Any())
        {
            GroupSpace2023User dummyuser = new GroupSpace2023User
            {
                Id = "Dummy",
                Email = "dummy@dummy.xx",
                UserName = "Dummy",
                FirstName = "Dummy",
                LastName = "Dummy",
                PasswordHash = "Dummy", // dummy zal gesaved worden als hashpassword, dus na dehashen zal het een random password zijn
                LockoutEnabled = true,
                LockoutEnd = DateTime.MaxValue
            };
            context.Users.Add(dummyuser);
            context.SaveChanges();

            GroupSpace2023User adminUser = new GroupSpace2023User
            {
                Id = "admin",
                Email = "admin@dummy.xx",
                UserName = "admin",
                FirstName = "administrator",
                LastName = "GroepSpace2023",
            };
            var result = await userManager.CreateAsync(adminUser, "Abc-123"); // w8 hier in de async want dit heb je nodig om verder te gaan
        }

        GroupSpace2023User admin = context.Users.First(u => u.UserName == "admin");

        if (!context.Roles.Any())
        {
            context.Roles.AddRange(

            new IdentityRole { Name = "SystemAdministrators", Id = "SystemAdministrators" },
            new IdentityRole { Name = "User", Id = "user" }
            );

            context.UserRoles.Add(new IdentityUserRole<string> { RoleId = "SystemAdministrators", UserId = admin.Id }); // maak admin role admin

            context.SaveChanges();
        }
        AddParameters();

        if (!context.Groeps.Any())
        {
            context.Groeps.Add(new Groep { Description = "Dummy", Name = "Dummy", Ended = DateTime.Now });
            context.SaveChanges();
        }
        Groep dummyGroep = context.Groeps.FirstOrDefault(g => g.Name == "Dummy");
        if (!context.Message.Any())
        {
            context.Message.Add(new Message { Title = "Dummy", Body = "", Sent = DateTime.Now, Deleted = DateTime.Now, Recipient = dummyGroep }); // of Recipientid = dummyGroep.Id
            context.SaveChanges();
        }

    }

    static void AddParameters()
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<GroupSpace2023.Models.Parameter> Parameter { get; set; } = default!;
}
