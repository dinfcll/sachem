using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;
using System.Collections.Generic;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class DossierEtuControllerTest
    {
        [TestMethod]
        public void DetailsAvecIdNull_DoitRetournerUn_HttpBadRequest()
        {
            int? id = null;
            var dossierEtuController = new DossierEtudiantController();

            var result = dossierEtuController.Details(id);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void DetailsAvecId0ouInexistant_DoitRetournerUn_HttpNotFound()
        {
            int? id = 0;
            var dossierEtuController = new DossierEtudiantController(new TestRepository());

            var result = dossierEtuController.Details(id);

            Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [TestMethod]
        public void NumeroTelephoneVide_DoitRetourner_Vide()
        {
            
        } 
    }
}
