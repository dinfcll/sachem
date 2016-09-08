﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Dynamic;

/*******************************************************/
/**Cette classe est grandement inspirée du projet PAM.**/
/************Crédits aux auteurs originaux.*************/
/*******************************************************/

namespace sachem.Models
{
    public enum TypeUsagers { Aucun = 0, Etudiant = 1, Enseignant = 2, Responsable = 3, Super = 4 } //Enum contenant les types d'usagers du SACHEM

    public class SachemIdentite
    {
        //Pour l'encryption du cookie (MachineCode)
        #pragma warning disable 0618 //Extrait du projet PAM: Pour l'encryption du cookie (MachineCode)

        public static TypeUsagers ObtenirTypeUsager(HttpSessionStateBase Session)
        {
            if (Session["id_TypeUsag"] == null)
                return TypeUsagers.Aucun;
            else if ((int)Session["id_TypeUsag"] == 1)
                return TypeUsagers.Etudiant;
            else if ((int)Session["id_TypeUsag"] == 2)
                return TypeUsagers.Enseignant;
            else if ((int)Session["id_TypeUsag"] == 3)
                return TypeUsagers.Responsable;
            else if ((int)Session["id_TypeUsag"] == 4)
                return TypeUsagers.Super;
            else
                return TypeUsagers.Aucun;
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
    // Classe scellée pour le HttpSession héritant du DynamicObject
    public sealed class SessionBag : DynamicObject
    {
        private HttpSessionStateBase Session
        {
            get { return new HttpSessionStateWrapper(HttpContext.Current.Session); }
        }
        private SessionBag()
        {

        }
        //
        //http://www.codeproject.com/Articles/191422/Accessing-ASP-NET-Session-Data-Using-Dynamics
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
    }
}
