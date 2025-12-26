using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using gestion_de_magasin.Models;

namespace gestion_de_magasin.Areas.Admin.Controllers
{
    [Area("Admin")] // Indique que c'est dans l'Area Admin
    [Authorize(Roles = "Admin")] // Seul l'admin peut entrer
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Récupère tous les utilisateurs pour les afficher
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }



        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            // 1. Supprimer l'ancien rôle
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            // 2. Ajouter le nouveau rôle
            await _userManager.AddToRoleAsync(user, newRole);

            // 3. Mettre à jour la propriété Role dans ton modèle ApplicationUser
            user.Role = newRole;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}