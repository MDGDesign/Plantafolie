using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Plantafolie.Models.Produit
{
    public class Produit : IValidatableObject
    {

        /* 
             Le prix de vente jamais supérieur au prix demandé
             Il est possible de retrouver un produit dans plusieurs catégories (Ex.: Plante d'ombre et plante facile ou rare)
             Il est possible de retrouver un produit dans plusieurs états (Ex.: Plante est nouvelle et est en liquidation)
             L'ordre de trie des champs ProduitID, Categorie et Date de création est fait via la table (Double clique dessus pour avoir les option de la table)
 
        */



        [Column(Order =1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProduitID { get; set; }

        
        [Required(ErrorMessage ="Le champs nom ne peut être vide!")]
        public string Nom { get; set; }


        [Required(ErrorMessage ="Le champs description doit au moins contenir une brève description!")]
        public string Description { get; set; }


        // Par défaut
        [DisplayName("Prix demandé"), DataType(DataType.Currency)]
        [Range(0.01, 10000.00, ErrorMessage = "Le {0} doit être compris entre {1} et {2}.")]
        // [Required(ErrorMessage = "Le prix demandé est obligatoire!")]
        public decimal PrixDemande { get; set; }     


        // Prix de vente doit être inférieur au prix demandé voir la méthode de validation du model
        [DisplayName("Prix de vente"), DataType(DataType.Currency)]
        [Range(0.01, 10000.00, ErrorMessage = "Le {0} doit être compris entre {1} et {2}.")]
        public decimal PrixDeVente { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [DisplayName("Image")]
        public string ImageName { get; set; }


        // Insertion de la date automatiquement dans le controlleur - Non modifiable
        [Column(Order = 1)]
        [DisplayName("Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyy}")]
        [ReadOnly(false)]
        public DateTime DateDeCreation { get; set; }


        [DisplayName("Quantité")]
        public int Quantite { get; set; }


        [DisplayName("Disponible")]
        public bool Disponible { get; set; }


        [DisplayName("Poids")]
        public decimal Poids { get; set; }


        [DisplayName("État")]
        public int EtatID { get; set; }
        public virtual Etat Etat { get; set; }      // DBset


        [Column(Order = 1)]
        [DisplayName("Catégorie")]
        public int CategorieID { get; set; }
        public virtual Categorie Categorie { get; set; }  // DBset ->




        // Validation coté serveur seulement pour le prix de vente et le prix demandé
        // Pour la tentative de validation côté client via le ICLientModelValidation voir la classe PrixProduitAttribute
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PrixDeVente >= PrixDemande)
            {
                yield return new ValidationResult("Le prix de vente doit être inférieur au prix demandé.");
            }
            
        }
    }
}
