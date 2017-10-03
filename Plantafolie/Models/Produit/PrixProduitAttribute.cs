using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Plantafolie.Models.Produit
{

    // Classe  test custom  pour la validation du prix demandé et prix de vente
    // Non implémenté
    // Voir la classe produit pour la vérification





    public class PrixProduitAttribute : ValidationAttribute, IClientModelValidator
    {
        // Source : https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation

        private decimal _prix;

        public PrixProduitAttribute(decimal prix)
        {
            _prix = prix;
        }

       
        // Validation
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Produit produit = (Produit)validationContext.ObjectInstance;

            if (produit.PrixDeVente > _prix)
            {
                return new ValidationResult(ErrorMessage);
            }
            // return base.IsValid(value, validationContext);
            return ValidationResult.Success;
        }


        // Ajout de la validation au model
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-PrixDeVente", ErrorMessage);

            var prix = _prix.ToString(CultureInfo.InvariantCulture);
            MergeAttribute(context.Attributes, "data-val-PrixDemande", prix);

        }


        // ??? Pas trouver l'info
        private void MergeAttribute(IDictionary<string, string> attributes, string v1, string v2)
        {
            
        }
    }
}
