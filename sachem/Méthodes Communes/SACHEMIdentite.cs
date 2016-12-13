using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using sachem.Models.DataAccess;

namespace sachem.Models
{
    public enum TypeUsagers { Aucun = 0, Etudiant = 1, Enseignant = 2, Responsable = 3, Super = 4, Eleve = 5, Tuteur = 6 } //Enum contenant les types d'usagers du SACHEM

    

    
    public class SachemIdentite
    {
        /*******************************************************/
        /**Cette classe est grandement inspirée du projet PAM.**/
        /************Crédits aux auteurs originaux.*************/
        /*******************************************************/
        public static List<TypeUsagers> TypeListeAdmin = new List<TypeUsagers> { TypeUsagers.Responsable, TypeUsagers.Super }; //Enum des types ayant pouvoirs d'admin
        public static List<TypeUsagers> TypeListeProf = new List<TypeUsagers> { TypeUsagers.Enseignant, TypeUsagers.Responsable, TypeUsagers.Super }; //Enum des types ayant pouvoirs d'admin                                                                                                                                                       
        #pragma warning disable 0618 

        public static TypeUsagers ObtenirTypeUsager(HttpSessionStateBase session)
        {
            //Switch case pour déterminer le type d'usager
            if(session["id_TypeUsag"] == null)
                return TypeUsagers.Aucun;
            switch ((int)session["id_TypeUsag"])
            {
                case 1:
                    return TypeUsagers.Etudiant;
                case 2:
                    return TypeUsagers.Enseignant;
                case 3:
                    return TypeUsagers.Responsable;
                case 4:
                    return TypeUsagers.Super;
                case 5:
                    return TypeUsagers.Eleve;
                case 6:
                    return TypeUsagers.Tuteur;
                default:
                    return TypeUsagers.Aucun;
            }
        }

        public static bool ValiderRoleAcces(List<TypeUsagers> listeRoles, HttpSessionStateBase session)
        {
            int idRole = (int?) session["id_TypeUsag"] ?? 0;
            return listeRoles.Contains((TypeUsagers)idRole);
        }

        public static string FormatTelephone(string s)
        {
            var charsToRemove = new string[] { ".", "-", "(", " ", ")" };
            return charsToRemove.Aggregate(s, (current, c) => current.Replace(c, string.Empty));
        }
        
        public static string RemettreTel(string a)

        {
            var modif = a.Insert(0, "(");
            modif = modif.Insert(4, ")");
            modif = modif.Insert(5, " ");
            modif = modif.Insert(9, "-");
            return modif;
        }
        //Permet de calculer un hash MD5 pour stocker/comparer les mots de passe sur une personne
        public static void EncrypterMpPersonne(ref Personne personne)
        {
            personne.ConfirmPassword = EncrypterChaine(personne.ConfirmPassword);
            personne.MP = EncrypterChaine(personne.MP);
        }

        //Permet l'encryption d'une chaine 
        public static string EncrypterChaine(string Chaine)
        {
            var provider = new MD5CryptoServiceProvider();
            var buffer = Encoding.UTF8.GetBytes(Chaine);
            return BitConverter.ToString(provider.ComputeHash(buffer)).Replace("-", "").ToLower();
        }
    }
    
    public sealed class SessionBag : DynamicObject
    {
        // Classe scellée pour le HttpSession héritant du DynamicObject
        //Article sur l'accès des données dans un objet dynamique (session) en asp.net mvc5 (.NET 4.0+)
        //http://www.codeproject.com/Articles/191422/Accessing-ASP-NET-Session-Data-Using-Dynamics

        private static readonly SessionBag sessionBag;

        static SessionBag()
        {
            sessionBag = new SessionBag();
        }

        private SessionBag()
        {
        }

        private HttpSessionStateBase Session
        {
            get { return new HttpSessionStateWrapper(HttpContext.Current.Session); }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = Session[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Session[binder.Name] = value;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder
               binder, object[] indexes, out object result)
        {
            int index = (int)indexes[0];
            result = Session[index];
            return result != null;
        }

        public override bool TrySetIndex(SetIndexBinder binder,
               object[] indexes, object value)
        {
            int index = (int)indexes[0];
            Session[index] = value;
            return true;
        }

        public static dynamic Current
        {
            get { return sessionBag; }
        }
    }

    public class Liste
    {
        private static readonly IDataRepository DataRepository = new BdRepository();
        private static readonly SACHEMEntities Db = new SACHEMEntities();
        private const int Brouillon = 2;

        public static List<SelectListItem> ListeSession(int session = 0)
        {
            var lSessions = DataRepository.GetSessions();
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", session));

            return slSession;
        }

        public static List<SelectListItem> ListePersonne(int idSession, int idPers)
        {
            var lPersonne = (from p in Db.Personne
                             join c in Db.Groupe on p.id_Pers equals c.id_Enseignant
                             where (p.id_TypeUsag == (int)TypeUsagers.Enseignant ||
                                    p.id_TypeUsag == (int)TypeUsagers.Responsable) &&
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
            var lstEnseignant = from p in Db.Personne
                                where p.id_TypeUsag == 2 && p.Actif
                                orderby p.Nom, p.Prenom
                                select p;
            var slEnseignant = new List<SelectListItem>();
            slEnseignant.AddRange(new SelectList(lstEnseignant, "id_Pers", "NomPrenom", superviseur));
            return slEnseignant;
        }

        public static List<SelectListItem> ListeTypeInscription(int typeInscription = 0)
        {
            var lInscriptions = Db.p_TypeInscription.AsNoTracking().OrderBy(i => i.TypeInscription);
            var slInscription = new List<SelectListItem>();
            slInscription.AddRange(new SelectList(lInscriptions, "id_TypeInscription", "TypeInscription", typeInscription));

            return slInscription;
        }

        public static List<SelectListItem> ListeStatutInscriptionSansBrouillon(int statut = 0)
        {
            var lStatut = from s in Db.p_StatutInscription where s.id_Statut != Brouillon select s;
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lStatut, "id_Statut", "Statut", statut));

            return slStatut;
        }

        public static List<string> ListeJours()
        {
            var jours = new List<string>();
            for (var i = (int)Semaine.Lundi; i < (int)Semaine.Samedi; i++)
            {
                jours.Add(((Semaine)i).ToString());
            }
            return jours.ToList();
        }

        public static IEnumerable<Cours> ListeCoursSelonSession(int session)
        {
            return Db.Cours.AsNoTracking()
                .Where(c => c.Groupe.Any(g => (g.id_Sess == session || session == 0)))
                .OrderBy(c => c.Nom)
                .AsEnumerable();
        }

        public static IEnumerable<Groupe> ListeGroupeSelonSessionEtCours(int cours, int session)
        {
            return Db.Groupe.AsNoTracking()
                .Where(p => (p.id_Sess == session || session == 0) && (p.id_Cours == cours || cours == 0))
                .OrderBy(p => p.NoGroupe);
        }
    }

    public class MotDePasse
    {
        public bool VerificationMotDePasseEtConfirmation(string MotDePasse, string MotDePasseConfirmation)
        {
            return MotDePasse == MotDePasseConfirmation;
        }
    }
}
