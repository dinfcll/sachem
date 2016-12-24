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
    
    public partial class Inscription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inscription()
        {
            this.CoursInteret = new HashSet<CoursInteret>();
            this.Disponibilite = new HashSet<Disponibilite>();
            this.Evaluation = new HashSet<Evaluation>();
            this.Jumelage = new HashSet<Jumelage>();
            this.Jumelage1 = new HashSet<Jumelage>();
            this.ReponseQuestion = new HashSet<ReponseQuestion>();
        }
    
        public int id_Inscription { get; set; }
        public int id_Sess { get; set; }
        public int id_Pers { get; set; }
        public int id_Statut { get; set; }
        public int id_TypeInscription { get; set; }
        public bool? TransmettreInfoTuteur { get; set; }
        public string NoteSup { get; set; }
        public bool? ContratEngagement { get; set; }
        public bool? BonEchange { get; set; }
        public DateTime DateInscription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoursInteret> CoursInteret { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disponibilite> Disponibilite { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluation> Evaluation { get; set; }
        public virtual p_StatutInscription p_StatutInscription { get; set; }
        public virtual p_TypeInscription p_TypeInscription { get; set; }
        public virtual Personne Personne { get; set; }
        public virtual Session Session { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jumelage> Jumelage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jumelage> Jumelage1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReponseQuestion> ReponseQuestion { get; set; }
        public virtual CoursInteret CoursInteret1 { get; set; }
    }
}
