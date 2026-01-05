using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using gestion_de_magasin.Models;

namespace gestion_de_magasin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // --- 1. LISTE DES UTILISATEURS (READ) ---
        // GET: /Admin/Users/Index
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // --- 2. CRÉATION D'UN COMPTE (GET) ---
        // GET: /Admin/Users/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // --- 3. CRÉATION D'UN COMPTE (POST) ---
        // POST: /Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string email, string password, string nomComplet, string role)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "L'email et le mot de passe sont obligatoires.");
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                NomComplet = nomComplet,
                Role = role,
                EmailConfirmed = true // Permet la connexion immédiate sans confirmation d'email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Vérifie si le rôle existe dans la base, sinon le crée
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                // Affectation du rôle au compte créé
                await _userManager.AddToRoleAsync(user, role);

                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        // --- 4. CHANGEMENT DE RÔLE (UPDATE) ---
        // POST: /Admin/Users/ChangeRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            // Sécurité : ne pas modifier l'admin principal par erreur
            if (user.Email == "admin@magasin.com" && newRole != "Admin")
            {
                TempData["Error"] = "Impossible de modifier le rôle de l'administrateur principal.";
                return RedirectToAction(nameof(Index));
            }

            // Supprimer les rôles actuels
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Ajouter le nouveau rôle
            await _userManager.AddToRoleAsync(user, newRole);

            // Mettre à jour la propriété personnalisée dans ApplicationUser
            user.Role = newRole;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        // --- 5. SUPPRESSION D'UN UTILISATEUR (DELETE) ---
        // POST: /Admin/Users/DeleteUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            // Sécurité : Empêcher la suppression de soi-même ou de l'admin principal
            if (user != null && user.Email != "admin@magasin.com")
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}