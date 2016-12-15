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
using sachem.Models.DataAccess;

namespace sachem.Methodes_Communes
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
            var idRole = session["id_TypeUsag"] == null ? 0 : (int)session["id_TypeUsag"]; //ne pas modifier
            return listeRoles.Contains((TypeUsagers)idRole);
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

        private HttpSessionStateBase Session => new HttpSessionStateWrapper(HttpContext.Current.Session);

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
            var index = (int)indexes[0];
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

        public static dynamic Current => sessionBag;
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
