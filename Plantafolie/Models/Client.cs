using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Plantafolie.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string Province { get; set; }
        public string Pays { get; set; }
        public string Telephone { get; set; }
        public string Cellulaire { get; set; }
        [DisplayName("Mot de passe")]
        public string MotDePasse { get; set; }
        public string Courriel { get; set; }
    }
}
