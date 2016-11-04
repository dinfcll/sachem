using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;

namespace sachemTests
{
    [TestClass]
    public class CoursControllerTest
    {
        [TestMethod]
        public void ReferenceTest()
        {
            var controller = new CoursController();

            var result = controller.Edit(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
        }
    }
}
