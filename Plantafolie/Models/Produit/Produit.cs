using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Plantafolie.Models.Produit
{
    public class Produit
    {

        /* 
             Le prix de vente jamais supérieur au prix demandé
             Il est possible de retrouver un produit dans plusieurs catégories (Ex.: Plante d'ombre et plante facile ou rare)
             Il est possible de retrouver un produit dans plusieurs états (Ex.: Plante est nouvelle et est en liquidation)
             L'ordre de trie des champs ProduitID, Categorie et Date de création est fait via la table (Double clique dessus pour avoir les option de la table)

            Pour la pagination: https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
        */






        [Column(Order =1)]
        public int ProduitID { get; set; }

        
        [DisplayName("Nom")]
        public string Nom { get; set; }

        public string Description { get; set; }

        public decimal PrixDemande { get; set; }    // Par défaut

        public decimal PrixDeVente { get; set; }    // Voir commentaire plus haut

        public string ImagePath { get; set; }

        [Column(Order = 1)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-mm-yyy}")]
        // Champs caché avec insertion de la date automatiquement
        public DateTime DateDeCreation { get; set; }

        public int Quantite { get; set; }

        public bool Disponible { get; set; }

        public decimal Poids { get; set; }

        public decimal EtatID { get; set; }

        public virtual Etat Etat { get; set; }      // DBset

        [Column(Order = 1)]
        public int CategorieID { get; set; }

        public virtual Categorie Categorie { get; set; }  // DBset ->




    }
}
