//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sachem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class p_HoraireInscription
    {
        public int id_Sess { get; set; }
        public System.TimeSpan HeureDebut { get; set; }
        public System.TimeSpan HeureFin { get; set; }
        public System.DateTime DateDebut { get; set; }
        public System.DateTime DateFin { get; set; }
    
        public virtual Session Session { get; set; }
    }
}
