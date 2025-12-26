namespace gestion_de_magasin.Models
{
    public class CommandeLigne
    {
        public int CommandeLigneId { get; set; }

        public int CommandeId { get; set; }
        public Commande Commande { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int Quantite { get; set; }
        public decimal PrixUnitaire { get; set; }
        public decimal PrixTotal => Quantite * PrixUnitaire;
    }
}
