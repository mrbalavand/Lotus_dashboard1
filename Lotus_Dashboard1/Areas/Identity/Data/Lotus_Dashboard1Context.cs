using Lotus_Dashboard1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lotus_Dashboard1.Data;

public class Lotus_Dashboard1Context : IdentityDbContext<Lotus_Dashboard1User>
{
    public Lotus_Dashboard1Context(DbContextOptions<Lotus_Dashboard1Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
