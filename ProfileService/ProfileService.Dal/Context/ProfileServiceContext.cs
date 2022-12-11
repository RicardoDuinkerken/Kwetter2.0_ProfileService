using Microsoft.EntityFrameworkCore;
using ProfileService.Dal.Models;

namespace ProfileService.Dal.Context;

public class ProfileServiceContext : DbContext
{

    public DbSet<Profile> Profiles { get; set; }

    public ProfileServiceContext(DbContextOptions<ProfileServiceContext> options) : base(options)
    {
        
    }

}