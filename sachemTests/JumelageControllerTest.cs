using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;
using System.Collections.Generic;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class JumelageControllerTest
    {
        [TestMethod]
        public void TestRetourneNbreJumelageEtudiant()
        {
            var jumelageController = new JumelageController();
            var statut = "";
            var i = 0;

            while(i > 3)
            {
                statut = jumelageController.RetourneNbreJumelageEtudiant(i);
                Assert.IsNotNull(statut);
            }

            statut = jumelageController.RetourneNbreJumelageEtudiant(3);
            Assert.AreEqual(statut, string.Empty);
        }

        [TestMethod]
        public void TestRetourneListeJoursSemaine()
        {
            var jumelageController = new JumelageController();
            List<string> joursSem;

            joursSem = jumelageController.RetourneListeJoursSemaine();

            Assert.AreEqual(joursSem[0], "Lundi");
            Assert.AreEqual(joursSem[1], "Mardi");
            Assert.AreEqual(joursSem[2], "Mercredi");
            Assert.AreEqual(joursSem[3], "Jeudi");
            Assert.AreEqual(joursSem[4], "Vendredi");
        }

        [TestMethod]
        public void ShowNullInscription()
        {
            var jumelageController = new JumelageController();

            var result = jumelageController.Details(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
        }


    }
}