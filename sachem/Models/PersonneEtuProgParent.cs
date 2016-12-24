using System.Collections.Generic;


namespace sachem.Models
{
    public class PersonneEtuProgParent
    {
        public Personne personne;
        public IEnumerable<EtuProgEtude> EtuProgEtu;
    }
}