using System;
using System.Collections.Generic;

namespace sachem.Models
{
    public struct DisponibiliteStruct
    {
        public string Jour { get; set; }

        public int Minutes { get; set; }

        public string NomCase { get; set; }

        public string NomCaseComplete { get; set; }

        public TimeSpan HeureDebut { get; set; }

        public TimeSpan HeureFin { get; set; }

        public bool EstDispo { get; set; }

        public bool EstDispoMaisJumele { get; set; }

        public int NbreUsagerMemeDispo { get; set; }

        public bool EstConsecutiveDonc3hrs { get; set; }

        public bool EstDispoEtCompatible { get; set; }

        public bool EstDispoEtCompatibleEtConsecutif { get; set; }
    }

    public enum Semaine
    {
        Dimanche = 1,
        Lundi,
        Mardi,
        Mercredi,
        Jeudi,
        Vendredi,
        Samedi
    }
}
