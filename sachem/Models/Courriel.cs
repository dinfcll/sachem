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
    
    public partial class Courriel
    {
        public int id_Courriel { get; set; }
        public int id_TypeCourriel { get; set; }
        public string Titre { get; set; }
        public string Courriel1 { get; set; }
        public Nullable<System.DateTime> DateDebut { get; set; }
        public Nullable<System.DateTime> DateFin { get; set; }
    
        public virtual p_TypeCourriel p_TypeCourriel { get; set; }
    }
}
