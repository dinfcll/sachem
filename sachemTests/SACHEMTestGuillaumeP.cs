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
        [TestMethod]
        public void RetourFormatTelephonneEnDixChiffres()
        {
            string NoTelephone = "(418)831-2390";
            var retour =  SACHEMIdentite.FormatTelephone(NoTelephone);
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
    }
}
