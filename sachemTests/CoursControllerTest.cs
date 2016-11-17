using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;
using System.Collections.Generic;
using sachem.Models;

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

        [TestMethod]
        public void EditNonExistingCoursReturnsNotFound()
        {
            var coursController = new CoursController(new TestRepository());

            var result = coursController.Edit(1);

            Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [TestMethod]
        public void EditExistingCours()
        {
            const int NO_COURS = 44;
            var testRepository = new TestRepository();
            testRepository.AddCours(new Cours { Actif = true, Code = "ABC", Groupe = new List<Groupe>(),
                id_Cours = NO_COURS, Nom = "Josée Lainesse" });
            var coursController = new CoursController(testRepository);

            var result = coursController.Edit(NO_COURS) as ViewResult;

            Assert.AreEqual(typeof(Cours), result.Model.GetType());
            Assert.AreEqual(NO_COURS, ((Cours)result.Model).id_Cours);
        }
    }
}
