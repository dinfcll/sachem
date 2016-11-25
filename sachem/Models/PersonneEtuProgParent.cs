using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sachem.Models
{
    public class PersonneEtuProgParent
    {
        public Personne personne;
        public IEnumerable<EtuProgEtude> EtuProgEtu;
    }
}