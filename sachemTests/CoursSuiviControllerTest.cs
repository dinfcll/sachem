using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;

namespace sachemTests
{
    [TestClass]
    public class CoursSuiviControllerTest
    {
        [TestMethod]
        public void DeleteNonExistingCoursSuivi()
        {
            var coursSuiviController = new CoursSuiviController();
            var test = new TestRepository();

            var result = coursSuiviController.Delete(null,1);
            test.RemoveCoursSuivi(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
        }
        [TestMethod]
        public void AddCoursSuiviToNonExistingPersonne()
        {
            var coursSuiviController = new CoursSuiviController();
            var test = new TestRepository();

            var result = coursSuiviController.Create(null);
            test.AddCoursSuivi(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
        }
    }
}
