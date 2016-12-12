using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;

namespace sachemTests
{
    [TestClass]
    public class EtudiantControllerTest
    {
        [TestMethod]
        public void TestControllerSupprimerEtudiantNull()
        {
            var etudiant = new EtudiantController();
            var resultat = etudiant.Delete(null);
            Assert.AreEqual(typeof(HttpStatusCodeResult), resultat.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)resultat).StatusCode);
        }
    }
}
