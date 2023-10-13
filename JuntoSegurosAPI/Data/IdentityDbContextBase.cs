using Microsoft.EntityFrameworkCore;

namespace JuntoSegurosAPI.Data
{
    public class IdentityDbContextBase
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
    }
}