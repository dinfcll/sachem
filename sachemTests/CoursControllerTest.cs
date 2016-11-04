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

            controller.Edit(null);
        }
    }
}
