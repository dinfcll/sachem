using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Dynamic;

namespace sachem.Models
{
    public enum TypeUsagers { Aucun = 0, Etudiant = 1, Enseignant = 2, Responsable = 3, Super = 4, Eleve = 5, Tuteur = 6 } //Enum contenant les types d'usagers du SACHEM

    /*******************************************************/
    /**Cette classe est grandement inspirée du projet PAM.**/
    /************Crédits aux auteurs originaux.*************/
    /*******************************************************/

    #region ClasseIdentitaireSachem
    public class SachemIdentite
    {
        public static List<TypeUsagers> TypeListeAdmin = new List<TypeUsagers> { TypeUsagers.Responsable, TypeUsagers.Super }; //Enum des types ayant pouvoirs d'admin
        public static List<TypeUsagers> TypeListeProf = new List<TypeUsagers> { TypeUsagers.Enseignant, TypeUsagers.Responsable, TypeUsagers.Super }; //Enum des types ayant pouvoirs d'admin                                                                                                                                                       
#pragma warning disable 0618 //Extrait du projet PAM: Pour l'encryption du cookie (MachineCode)

        public static TypeUsagers ObtenirTypeUsager(HttpSessionStateBase Session)
        {
            //Switch case pour déterminer le type d'usager
            if(Session["id_TypeUsag"] == null)
                return TypeUsagers.Aucun;
            switch ((int)Session["id_TypeUsag"])
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

        public static bool ValiderRoleAcces(List<TypeUsagers> ListeRoles, HttpSessionStateBase session)
        {
            int idRole = (session["id_TypeUsag"] == null ? 0 : (int)session["id_TypeUsag"]);
            return ListeRoles.Contains((TypeUsagers)idRole);
        }

        public static string FormatTelephone(string s)
        {
            var charsToRemove = new string[] { ".", "-", "(", " ", ")" };
            foreach (var c in charsToRemove)
            {
                s = s.Replace(c, string.Empty);
            }
            return s;
        }
        
        public static string RemettreTel(string a)

        {
            string modif;
            modif = a.Insert(0, "(");
            modif = modif.Insert(4, ")");
            modif = modif.Insert(5, " ");
            modif = modif.Insert(9, "-");
            return modif;
        }
        //Permet de calculer un hash MD5 pour stocker/comparer les mots de passe sur une personne
        public static void encrypterMPPersonne(ref Personne personne)
        {
            personne.ConfirmPassword = encrypterChaine(personne.MP);
            personne.MP = encrypterChaine(personne.MP);
        }

        //Permet l'encryption d'une chaine 
        public static string encrypterChaine(string Chaine)
        {
            byte[] buffer;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            buffer = Encoding.UTF8.GetBytes(Chaine);
            return BitConverter.ToString(provider.ComputeHash(buffer)).Replace("-", "").ToLower();
        }
    }
    #endregion

    #region SessionBagCustomDylan
    // Classe scellée pour le HttpSession héritant du DynamicObject
    //Article sur l'accès des données dans un objet dynamique (session) en asp.net mvc5 (.NET 4.0+)
    //http://www.codeproject.com/Articles/191422/Accessing-ASP-NET-Session-Data-Using-Dynamics
    public sealed class SessionBag : DynamicObject
    {
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
    #endregion

}
