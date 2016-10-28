using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sachem.Models
{
    public class PersEtuProg
    {
        public Personne p;
        public EtuProgEtude epe;
        public PersEtuProg(Personne pers,EtuProgEtude etuprog)
        {
            p = pers;
            epe = etuprog;
        }

    }
}