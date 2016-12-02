using System.Collections.Generic;

namespace sachem.Models
{
    public struct DisponibiliteStruct
    {
        public string Jour;
        public int Minute;
        public Dictionary<string, int> dictionary;
        public DisponibiliteStruct(string s, int m)
        {
            Jour = s;
            Minute = m;
            dictionary = new Dictionary<string, int> { { "Lundi", 2 }, { "Mardi", 3 },
                { "Mercredi", 4 }, { "Jeudi", 5 }, { "Vendredi", 6 } };
            dictionary = new Dictionary<string, int>();
            dictionary.Add("Lundi", 2);
            dictionary.Add("Mardi", 3);
            dictionary.Add("Mercredi", 4);
            dictionary.Add("Jeudi", 5);
            dictionary.Add("Vendredi", 6);
        }
    }
}
