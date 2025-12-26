namespace gestion_de_magasin.Models
{
    public class Fournisseur
    {
       
            public int FournisseurId { get; set; }
            public string Nom { get; set; }
       
            public string Telephone { get; set; }
            public string Adresse { get; set; }

        // Relation : un fournisseur possède des articles
        public List<Article> Articles { get; set; }

    }
}
