using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantafolie.Models.ProduitViewModels
{
    public class Etat
    {

        /* 
             On peut avoir une liste de produit selon l'état       
        */


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EtatID { get; set; }

        [DisplayName("Nom")]
        public string EtatNom { get; set; }

        public List<Produit> Produits { get; set; }

    }
}