using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace JuntoSegurosAPI.Data
{
    public class IdentityInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            // Verifica se a role "Admin" já existe
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                // Cria a role "Admin"
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Verifica se o usuário "admin" já existe
            if (await _userManager.FindByNameAsync("admin") == null)
            {
                // Cria o usuário "admin"
                var user = new IdentityUser { UserName = "admin", Email = "admin@example.com" };
                var result = await _userManager.CreateAsync(user, "Admin@123");

                // Adiciona o usuário "admin" à role "Admin"
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

    }
}
