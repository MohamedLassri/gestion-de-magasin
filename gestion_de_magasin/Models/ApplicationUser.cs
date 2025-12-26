using Microsoft.AspNetCore.Identity;

namespace gestion_de_magasin.Models
{
   
        public class ApplicationUser : IdentityUser
        {
            public string NomComplet { get; set; }

            // Pour distinguer Admin, Géant, Vendeur, Client
            public string Role { get; set; }

            // Relation : un user peut avoir plusieurs commandes
            public List<Commande> Commandes { get; set; }
        }

    
}
