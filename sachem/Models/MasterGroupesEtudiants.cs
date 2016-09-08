using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sachem.Models
{
    public class MasterGroupesEtudiants
    {
        public Groupe groupe;
        public IEnumerable<Personne> personnes;
    }
}