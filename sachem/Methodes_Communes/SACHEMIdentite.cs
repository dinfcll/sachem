using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Methodes_Communes
{
    public enum TypeUsager { Aucun = 0, Etudiant, Enseignant, Responsable, Super }
    public enum TypeInscription { Aucun = 0, EleveAide, TuteurCours, TuteurBenevole, TuteurRemunere }
    public class SachemIdentite
    {
        /*******************************************************/
        /**Cette classe est grandement inspirée du projet PAM.**/
        /************Crédits aux auteurs originaux.*************/
        /*******************************************************/
        public static List<TypeUsager> TypeListeAdmin = new List<TypeUsager> { TypeUsager.Responsable, TypeUsager.Super }; //Enum des types ayant pouvoirs d'admin
        public static List<TypeUsager> TypeListeProf = new List<TypeUsager> { TypeUsager.Enseignant, TypeUsager.Responsable, TypeUsager.Super }; //Enum des types ayant pouvoirs d'admin                                                                                                                                                       
        #pragma warning disable 0618 

        public static TypeUsager ObtenirTypeUsager(HttpSessionStateBase browserSession)
        {
            if(browserSession["TypeUsager"] == null)
                return TypeUsager.Aucun;
            switch ((int)browserSession["TypeUsager"])
            {
                case 1:
                    return TypeUsager.Etudiant;
                case 2:
                    return TypeUsager.Enseignant;
                case 3:
                    return TypeUsager.Responsable;
                case 4:
                    return TypeUsager.Super;
                default:
                    return TypeUsager.Aucun;
            }
        }

        public static TypeInscription ObtenirTypeInscription(HttpSessionStateBase browserSession)
        {
            if (browserSession["TypeInscription"] == null)
                return TypeInscription.Aucun;
            switch ((int)browserSession["TypeInscription"])
            {
                case 1:
                    return TypeInscription.EleveAide;
                case 2:
                    return TypeInscription.TuteurCours;
                case 3:
                    return TypeInscription.TuteurBenevole;
                case 4:
                    return TypeInscription.TuteurRemunere;
                default:
                    return TypeInscription.Aucun;
            }
        }

        public static bool ValiderRoleAcces(List<TypeUsager> listeRoles, HttpSessionStateBase browserSession)
        {
            var idRole = browserSession["TypeUsager"] == null ? 0 : (int)browserSession["TypeUsager"]; //ne pas modifier
            return listeRoles.Contains((TypeUsager)idRole);
        }

        public static bool ValiderRoleAccesSuperieurAEtudiant(List<TypeUsager> listeRoles, HttpSessionStateBase browserSession)
        {
            var idRole = browserSession["TypeUsager"] == null ? 0 : (int)browserSession["TypeUsager"];
            return listeRoles.Contains((TypeUsager)idRole) && idRole > (int)TypeUsager.Etudiant;
        }

        public static bool ValiderEtudiantTypeAcces(List<TypeInscription> listeEtudiantRoles, HttpSessionStateBase browserSession)
        {
            var idRole = browserSession["TypeInscription"] == null ? 0 : (int)browserSession["TypeInscription"]; //ne pas modifier
            return listeEtudiantRoles.Contains((TypeInscription)idRole);
        }

        public static string FormatTelephone(string s)
        {
            var charsToRemove = new[] { ".", "-", "(", " ", ")" };
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
        public static string EncrypterChaine(string chaine)
        {
            var provider = new MD5CryptoServiceProvider();
            var buffer = Encoding.UTF8.GetBytes(chaine);
            return BitConverter.ToString(provider.ComputeHash(buffer)).Replace("-", "").ToLower();
        }
    }
    
    public sealed class BrowserSessionBag : DynamicObject
    {
        // Classe scellée pour le HttpSession héritant du DynamicObject
        //Article sur l'accès des données dans un objet dynamique (session) en asp.net mvc5 (.NET 4.0+)
        //http://www.codeproject.com/Articles/191422/Accessing-ASP-NET-Session-Data-Using-Dynamics

        private static readonly BrowserSessionBag browserSessionBag;

        static BrowserSessionBag()
        {
            browserSessionBag = new BrowserSessionBag();
        }

        private BrowserSessionBag()
        {
        }

        private HttpSessionStateBase BrowserSession => new HttpSessionStateWrapper(HttpContext.Current.Session);

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = BrowserSession[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            BrowserSession[binder.Name] = value;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder
               binder, object[] indexes, out object result)
        {
            var index = (int)indexes[0];
            result = BrowserSession[index];
            return result != null;
        }

        public override bool TrySetIndex(SetIndexBinder binder,
               object[] indexes, object value)
        {
            var index = (int)indexes[0];
            BrowserSession[index] = value;
            return true;
        }

        public static dynamic Current => browserSessionBag;
    }

    public class AutreMethode
    {
        public bool VerificationMotDePasseEtConfirmation(string motDePasse, string motDePasseConfirmation)
        {
            return motDePasse == motDePasseConfirmation;
        }

        public HttpStatusCodeResult CheckSiIdEstNull(int? id)
        {
            return id == null ? new HttpStatusCodeResult(HttpStatusCode.BadRequest) : null;
        }
    }
}
