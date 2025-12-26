namespace gestion_de_magasin.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public DateTime DateCommande { get; set; }
        public decimal MontantTotal { get; set; }

        // Client (ApplicationUser)
        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        // Lignes de commande
        public List<CommandeLigne> Lignes { get; set; }

        // Paiement
        public Paiement Paiement { get; set; }
    }
}
