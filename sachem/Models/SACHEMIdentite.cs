using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

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
}
