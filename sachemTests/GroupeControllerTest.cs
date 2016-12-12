using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;

namespace sachemTests
{
    [TestClass]
    public class GroupeControllerTest
    {
        [TestMethod]
        public void DeplacerIdNull()
        {
            var groupecontroller = new GroupesController();

            var resultat = groupecontroller.Deplacer(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), resultat.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)resultat).StatusCode);
        }
        [TestMethod]
        public void DeleteGroupeNull()
        {
            var controller = new GroupesController();
            var result = controller.Delete(null);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
        }
    }
}
