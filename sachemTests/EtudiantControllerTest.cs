using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;
using System.Collections.Generic;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class EtudiantControllerTest
    {
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
