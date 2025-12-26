namespace gestion_de_magasin.Models
{
    public class Paiement
    {
        public int PaiementId { get; set; }
        public decimal Montant { get; set; }
        public string ModePaiement { get; set; }   // Ex : CB, Cash, PayPal
        public DateTime DatePaiement { get; set; }

        public int CommandeId { get; set; }
        public Commande Commande { get; set; }
    }
}
