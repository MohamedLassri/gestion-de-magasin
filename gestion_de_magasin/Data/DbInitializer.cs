using Microsoft.AspNetCore.Identity;
using gestion_de_magasin.Models;

namespace gestion_de_magasin.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            // Initialisation des gestionnaires de rôles et d'utilisateurs
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // 1. Liste des rôles à créer
            string[] roleNames = { "Admin", "Gerant", "Vendeur", "Client" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Crée le rôle s'il n'existe pas encore
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Créer l'administrateur par défaut
            var adminEmail = "admin@magasin.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    NomComplet = "Administrateur Système",
                    Role = "Admin", // Ton champ personnalisé
                    EmailConfirmed = true
                };

                // Création de l'user avec un mot de passe sécurisé
                var createPowerUser = await userManager.CreateAsync(user, "Admin123!");
                if (createPowerUser.Succeeded)
                {
                    // Affectation du rôle Admin dans Identity
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}