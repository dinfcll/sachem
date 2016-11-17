using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Web.Mvc;
using sachem.Controllers;
using System.Collections.Generic;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class SACHEMTestGuillaumeP
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
        public void RetourFormatTelephonneEnDixChiffres()
        {
            string NoTelephone = "(418)831-2390";
            var retour = SachemIdentite.FormatTelephone(NoTelephone);
            Assert.AreEqual("4188312390", retour);
        }
        [TestMethod]
        public void TestControllerSupprimerEtudiantNull()
        {
            var Etudiant = new EtudiantController();
            var resultat = Etudiant.Delete(null);
            Assert.AreEqual(typeof(HttpStatusCodeResult), resultat.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)resultat).StatusCode);
        }
        [TestMethod]
        public void testEncryptionMPPersonne()
        {
            string test123Encryptee = "cc03e747a6afbbcbf8be7668acfebee5";

            SachemIdentite.encrypterMPPersonne(ref pers);
            Assert.AreEqual(test123Encryptee, pers.MP);
        }
    }
}
