using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage.Metiers
{
    [Table ("AgencesDeVoyages")]
    public class AgenceVoyage
    {
        public int Id { get; set; }
        public string Nom { get; set; }
       

    }
}
