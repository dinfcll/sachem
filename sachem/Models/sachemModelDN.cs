using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sachem.Models
{
    //redéfinition de la classe partielle
    public partial class Session
    {
        //Description de la session
        public string NomSession
        {
            get
            {
                return string.Format("{0} {1}", p_Saison.Saison, Annee.ToString());
            }
        }
    }

    //redéfinition de la classe partielle
    public partial class Personne
    {
        private string sProgEtu;
        //Nom complet de l'enseignant formatté

        public string PrenomNom
        {
            get
            {
                return string.Format("{0} {1}", Prenom, Nom);
            }
        }
        public string NomPrenom
        {
            get
            {
                if (Prenom == null)
                    return Nom;
                else
                    return string.Format("{0}, {1}", Nom, Prenom);
            }
        }
        public int Age
        {
            get
            {
                int age = 0;
                if (DateNais != null)
                {
                    DateTime datedujour = DateTime.Today;
                    age = datedujour.Year - DateNais.Value.Year;
                    if (DateNais > datedujour.AddYears(-age)) age--;
                }
                return age;
            }
        }

        public string ProgEtu
        {
            get
            {
                return sProgEtu;
            }
            set
            {
                sProgEtu = value;
            }
        }

        public string Matricule7//couper le matricule pour avoir 7 de long
        {
            get
            {
                if (Matricule != null)
                    return this.Matricule.Substring(2);
                return "";
            }
            set
            {
                this.Matricule = DateTime.Now.Year.ToString().Substring(0, 2) + value;//pour avoir un matricule de la forme 201334110
            }
        }
    }

    public partial class Cours
    {
        //Nom complet de l'enseignant formatté
        public string CodeNom
        {
            get
            {
                return string.Format("{0}-{1}", Code, Nom);
            }
        }
    }
}