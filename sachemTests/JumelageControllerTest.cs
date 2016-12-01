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
        public void RetourneNbreJumelageEtudiant()
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
    }
}