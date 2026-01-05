namespace gestion_de_magasin.Models
{
    public class Article
    {

        public int ArticleId { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }   

        // Relation => Fournisseur
        public int FournisseurId { get; set; }
        public Fournisseur Fournisseur { get; set; }

        // Relation => Ligne de commande
        public List<CommandeLigne> CommandeLignes { get; set; }
    }
    
}
