using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using sachem.Models.DataAccess;

namespace sachem.Classes_Sachem
{
    public class Liste
    {
        private static readonly IDataRepository DataRepository = new BdRepository();
        private static readonly SACHEMEntities _db = new SACHEMEntities();
        private const int Brouillon = 2;

        public static List<SelectListItem> ListeSession(int session = 0)
        {
            var lSessions = DataRepository.GetSessions();
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", session));

            return slSession;
        }

        public static SelectList ListeSexe()
        {
            return new SelectList(_db.p_Sexe, "id_Sexe", "Sexe");
        }

        public static List<SelectListItem> ListePersonne(int idSession, int idPers)
        {
            var lPersonne = (from p in _db.Personne
                join c in _db.Groupe on p.id_Pers equals c.id_Enseignant
                where (p.id_TypeUsag == (int) TypeUsagers.Enseignant ||
                       p.id_TypeUsag == (int) TypeUsagers.Responsable) &&
                      p.Actif &&
                      c.id_Sess == (idSession == 0 ? c.id_Sess : idSession)
                orderby p.Nom, p.Prenom
                select p).Distinct();

            var slPersonne = new List<SelectListItem>();
            slPersonne.AddRange(new SelectList(lPersonne, "id_Pers", "NomPrenom", idPers));

            return slPersonne;
        }

        public static List<SelectListItem> ListeCours(int cours = 0)
        {
            var lCours = DataRepository.GetCours();
            var slCours = new List<SelectListItem>();
            slCours.AddRange(new SelectList(lCours, "id_Cours", "CodeNom", cours));
            return slCours;
        }

        public static List<SelectListItem> ListeCollege(int college = 0)
        {
            var lCollege = DataRepository.GetCollege();
            var slCollege = new List<SelectListItem>();
            slCollege.AddRange(new SelectList(lCollege, "id_College", "College", college));

            return slCollege;
        }

        public static List<SelectListItem> ListeStatutCours(int statut = 0)
        {
            var lStatut = DataRepository.GetStatut();
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lStatut, "id_Statut", "Statut", statut));

            return slStatut;
        }

        public static List<SelectListItem> ListeEnseignant(int enseignant = 0)
        {
            var lEnseignant = DataRepository.AllEnseignantOrdered();
            var slEnseignant = new List<SelectListItem>();
            slEnseignant.AddRange(new SelectList(lEnseignant, "id_Pers", "Nom", enseignant));

            return slEnseignant;
        }

        public static List<SelectListItem> ListeSuperviseur(int superviseur = 0)
        {
            var lstEnseignant = from p in _db.Personne
                where p.id_TypeUsag == 2 && p.Actif
                orderby p.Nom, p.Prenom
                select p;
            var slEnseignant = new List<SelectListItem>();
            slEnseignant.AddRange(new SelectList(lstEnseignant, "id_Pers", "NomPrenom", superviseur));
            return slEnseignant;
        }

        public static List<SelectListItem> ListeTypeInscription(int typeInscription = 0)
        {
            var lInscriptions = _db.p_TypeInscription.AsNoTracking().OrderBy(i => i.TypeInscription);
            var slInscription = new List<SelectListItem>();
            slInscription.AddRange(new SelectList(lInscriptions, "id_TypeInscription", "TypeInscription", typeInscription));

            return slInscription;
        }

        public static List<SelectListItem> ListeStatutInscriptionSansBrouillon(int statut = 0)
        {
            var lStatut = from s in _db.p_StatutInscription where s.id_Statut != Brouillon select s;
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lStatut, "id_Statut", "Statut", statut));

            return slStatut;
        }
      
        public static List<string> ListeJours()
        {
            List<string> jours = new List<string>();
            for (int i = (int)Semaine.Lundi; i < (int)Semaine.Samedi; i++)
            {
                jours.Add(((Semaine)i).ToString());
            }
            return jours.ToList();
        }

        public static List<SelectListItem> ListeStatutCours()
        {
            var lstStatut = from c in _db.p_StatutCours orderby c.id_Statut select c;
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lstStatut, "id_Statut", "Statut"));
            return slStatut;
        }
      
        public static IEnumerable<Cours> ListeCoursSelonSession(int session)
        {
            return _db.Cours.AsNoTracking()
                .Where(c => c.Groupe.Any(g => (g.id_Sess == session || session == 0)))
                .OrderBy(c => c.Nom)
                .AsEnumerable();
        }

        public static IEnumerable<Groupe> ListeGroupeSelonSessionEtCours(int cours, int session)
        {
            return _db.Groupe.AsNoTracking()
                .Where(p => (p.id_Sess == session || session == 0) && (p.id_Cours == cours || cours == 0))
                .OrderBy(p => p.NoGroupe);
        }

        public static List<SelectListItem> ListeTypesCourriels(int typeCourriel = 0)
        {
            var lCourriel = _db.p_TypeCourriel.AsNoTracking().OrderBy(i => i.id_TypeCourriel);
            var slCourriel = new List<SelectListItem>();
            slCourriel.AddRange(new SelectList(lCourriel, "id_TypeCourriel", "TypeCourriel", typeCourriel));

            return slCourriel;
        }
    }
}
