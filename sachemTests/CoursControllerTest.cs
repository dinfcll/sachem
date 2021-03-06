﻿using System.Net;
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
            const int noCours = 42;
            var testRepository = new TestRepository();
            testRepository.AddCours(new Cours { Actif = true, Code = "ABC", Groupe = new List<Groupe>(),
                id_Cours = noCours, Nom = "Josée Lainesse" });
            var coursController = new CoursController(testRepository);

            var result = coursController.Edit(noCours) as ViewResult;

            Assert.AreEqual(typeof(Cours), result?.Model.GetType());
            Assert.AreEqual(noCours, ((Cours)result?.Model).id_Cours);
        }

        [TestMethod]
        public void SupprimerUnObjetNull()
        {
            var coursController = new CoursController();

            var result = coursController.Delete(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void VerifierSiAjouterUnCoursPourEnsuiteLeSupprimerRedirigeALaBonneVue()
        {
            const int idCours = 69;
            var testRepository = new TestRepository();

            testRepository.AddCours(new Cours { Actif = true, Code = "Test", Groupe = new List<Groupe>(), id_Cours = idCours, Nom = "Patate Cosmique" });
            var coursController = new CoursController(testRepository);

            coursController.Create(testRepository.FindCours(idCours), 0);
            var resultSuppression = coursController.Delete(idCours) as ViewResult;


            Assert.AreEqual("Delete", resultSuppression?.ViewName);
        }


        [TestMethod]
        public void DeleteNonExistingCoursReturnsNotFound()
        {
            var coursController = new CoursController(new TestRepository());

            var result = coursController.Delete(100000);

            Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
        }
        [TestMethod]
        public void RechercherCoursNull()
        {
            var repo = new TestRepository();
            var result = repo.FindCours(-1);
            Assert.AreEqual(null, result);
        }
    }
}
