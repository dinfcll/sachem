using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;

namespace sachemTests
{
    [TestClass]
    public class DossierEtuControllerTest
    {
        [TestMethod]
        public void DetailsAvecIdNull_DoitRetournerUn_HttpBadRequest()
        {
            var dossierEtuController = new DossierEtudiantController();

            var result = dossierEtuController.Details(null);

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
    }
}
