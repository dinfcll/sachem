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
    
    public partial class ReponseQuestion
    {
        public int id_RepQuest { get; set; }
        public Nullable<int> id_Inscription { get; set; }
        public Nullable<int> id_Suivi { get; set; }
        public Nullable<int> id_ChoixRep { get; set; }
        public string ReponseTexte { get; set; }
        public System.DateTime DateReponse { get; set; }
        public bool Transmis { get; set; }
    
        public virtual ChoixReponse ChoixReponse { get; set; }
        public virtual Inscription Inscription { get; set; }
        public virtual Suivi Suivi { get; set; }
    }
}
