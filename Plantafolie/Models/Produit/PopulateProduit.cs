using Plantafolie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantafolie.Models.Produit
{
    public class PopulateProduit
    {


        public static void FillDB(ApplicationDbContext context)
        {

            if (!context.Produits.Any())
            {
                context.Produits.AddRange(
                    
                    
                    
                    );
            }


            if (!context.Categories.Any()) { }
            {
                context.Categories.AddRange(
                    
                    
                    
                    );
            }


            if (!context.Etats.Any())
            {
                context.Etats.AddRange(
                    
                    
                    );
            }




        }

























    }
}
