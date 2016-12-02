using System.Collections.Generic;

namespace sachem.Models
{
    public struct DisponibiliteStruct
    {
        string jour;
        int minutes;
        string nomCase;
        bool estDispo;
        bool estDispoMaisJumele;
        int nbreUsagerMemeDispo;
        bool estConsecutiveDonc3hrs;

        public string Jour { get { return jour; } set { jour = value; } }
        public int Minutes { get { return minutes; } set { minutes = value; } }
        public string NomCase { get { return nomCase; } set { nomCase = value; } }
        public bool EstDispo { get { return estDispo; } set { estDispo = value; } }
        public bool EstDispoMaisJumele { get { return estDispoMaisJumele; } set { estDispoMaisJumele = value; } }
        public int NbreUsagerMemeDispo { get { return nbreUsagerMemeDispo; } set { nbreUsagerMemeDispo = value; } }
        public bool EstConsecutiveDonc3hrs { get { return estConsecutiveDonc3hrs; } set { estConsecutiveDonc3hrs = value; } }
    }

    public enum Semaine
    {
        Dimanche = 0,
        Lundi,
        Mardi,
        Mercredi,
        Jeudi,
        Vendredi,
        Samedi
    }
}
