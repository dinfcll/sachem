using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;
using System.Collections.Generic;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class PersonneControllerTest
    {
        private Personne pers = new Personne
        {
            Actif = true,
            Nom = "Heure",
            Prenom = "Taist",
            NomUsager = "heuret",
            MP = "test123",
            Courriel = "test@123.Huston",
            Telephone = "9999999999",
            DateNais = new System.DateTime(1111, 11, 11)
        };
        [TestMethod]
        public void SupprimePersonneExistante()
        {
            const int id_PersonneCree = 1500;
            var testrepository = new TestRepository();
            testrepository.AddPersonne(new Personne
            {
                Actif = true,
                Nom = "Carel",
                Prenom = "Ford",
                id_Pers = id_PersonneCree,
                id_TypeUsag = 1,
                Matricule = "201639488"
            });
            var personneController = new PersonnesController(testrepository);

            var resultat = personneController.Delete(id_PersonneCree) as ViewResult;

            Assert.AreEqual(typeof(Personne), resultat.Model.GetType());
            Assert.AreEqual(id_PersonneCree, ((Personne)resultat.Model).id_Pers);
        }
        public void RetourFormatTelephonneEnDixChiffres()
        {
            string NoTelephone = "(418)831-2390";
            var retour = SachemIdentite.FormatTelephone(NoTelephone);
            Assert.AreEqual("4188312390", retour);
        }
        public void testEncryptionMPPersonne()
        {
            string test123Encryptee = "cc03e747a6afbbcbf8be7668acfebee5";

            SachemIdentite.encrypterMPPersonne(ref pers);
            Assert.AreEqual(test123Encryptee, pers.MP);
        }
    }
}
