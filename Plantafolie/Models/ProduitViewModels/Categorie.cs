using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantafolie.Models.ProduitViewModels
{
    public class Categorie
    {
        /* 
             On peut avoir une liste de produit selon la catégorie       
        */


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategorieID { get; set; }

        [DisplayName("Catégorie")]
        public string CategorieNom { get; set; }

        public List<Produit> Produits { get; set; }
    }
}