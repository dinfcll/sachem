using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;

namespace sachemTests
{
    [TestClass]
    public class JumelageControllerTest
    {
        [TestMethod]
        public void TestRetourneNbreJumelageEtudiant()
        {
            var jumelageController = new JumelageController();
            string statut;
            var i = 0;

            while(i <= 3)
            {
                statut = jumelageController.RetourneNbreJumelageEtudiant(i);
                Assert.IsNotNull(statut);
                i++;
            }

            statut = jumelageController.RetourneNbreJumelageEtudiant(0);
            Assert.AreEqual(statut, "Non jumelé");
        }

        [TestMethod]
        public void TestRetourneListeJoursSemaine()
        {
            var jumelageController = new JumelageController();

            var joursSem = jumelageController.RetourneListeJoursSemaine();

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
            var test = new TestRepository();
            
            var result = jumelageController.Details(null);
            var jumelage = test.FindJumelage(-1);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
            Assert.IsNull(jumelage);
        }


    }
}