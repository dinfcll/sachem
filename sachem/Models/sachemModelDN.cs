using System;

namespace sachem.Models
{
    //redéfinition de la classe partielle
    public partial class Session
    {
        //Description de la session
        public string NomSession => $"{p_Saison.Saison} {Annee}";
    }

    //redéfinition de la classe partielle
    public partial class Personne
    {
        public int idpEtu;
        //Nom complet de l'enseignant formatté

        public string PrenomNom => $"{Prenom} {Nom}";

        public string NomPrenom => Prenom == null ? Nom : $"{Nom}, {Prenom}";

        public int Age
        {
            get
            {
                var age = 0;
                if (DateNais != null)
                {
                    var datedujour = DateTime.Today;
                    age = datedujour.Year - DateNais.Value.Year;
                    if (DateNais > datedujour.AddYears(-age)) age--;
                }
                return age;
            }
        }

        public string ProgEtu { get; set; }

        public string Matricule7//couper le matricule pour avoir 7 de long
        {
            get
            {
                return Matricule?.Substring(2) ?? "";
            }
            set
            {
                Matricule = DateTime.Now.Year.ToString().Substring(0, 2) + value;//pour avoir un matricule de la forme 201334110
            }
        }
    }

    //redéfinition de la classe partielle
    public partial class ProgrammeEtude
    {
        //concaténation du code et du nom de programme formatté
        public string CodeNomProgramme
        {
            get
            {
                return string.Format("{0}-{1}", Code, NomProg);
            }
        }
    }

    public partial class Cours
    {
        //Nom complet de l'enseignant formatté
        public string CodeNom => $"{Code}-{Nom}";
    }

    public partial class Groupe
    {
        //public string nbPersonneGroupe;
        //public int nbPersonne;
    }

    public partial class PersonneProgEtu
    {
        public Personne personne;
        public ProgrammeEtude progEtuActif;
    }
}