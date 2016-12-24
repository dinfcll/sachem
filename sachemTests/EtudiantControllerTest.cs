using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sachem.Controllers;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class EtudiantControllerTest
    {
        private const int IdPers = 100;

        private readonly Personne _etudiant = new Personne
        {
            Actif = true,
            id_Pers = IdPers,
            id_Sexe = 2,
            id_TypeUsag = 1,
            Nom = "Ouellet",
            Prenom = "Jose",
            NomUsager = "ouelletj",
            Matricule7 = "1242637",
            Courriel = "1242637@gmail.com",
            Telephone = "1234567890",
            MP = "etudiant123*",
            ConfirmPassword = "etudiant123*",
            DateNais = new System.DateTime(1989, 04, 04)
        };

        [TestMethod]
        public void TestControllerSupprimerEtudiantNull()
        {
            var etudiant = new EtudiantController();
            var test = new TestRepository();

            var resultat = etudiant.Delete(null);
            test.RemovePersonne(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), resultat.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)resultat).StatusCode);
        }

        [TestMethod]
        public void EtudiantAjout_ShouldReturnNotNull()
        {
            var testRepository = new TestRepository();
            var ensController = new EtudiantController(testRepository);
            const int page = 1;
            var httpContext = new Mock<HttpContextBase>();
            var routeData = new RouteData();
            NameValueCollection nv;
            httpContext.Setup(c => c.Request.RequestContext.RouteData).Returns(routeData);
            httpContext.Setup(c => c.Request.Form).Returns(delegate
            {
                nv = new NameValueCollection { { "id_ProgEtu", "5" }, { "id_Session", "1" } };
                return nv;
            });

            ensController.ControllerContext = new ControllerContext(httpContext.Object, routeData, ensController);
            ensController.Create(_etudiant, page);
            var rechercheEnseignant = testRepository.FindPersonne(_etudiant.id_Pers);

            Assert.IsNotNull(rechercheEnseignant);

        }
        [TestMethod]
        public void EtudiantModifier_ShouldReturnEtudiant()
        {
            var testRepository = new TestRepository();
            var etuController = new EtudiantController(testRepository);
            const int page = 1;
            var httpContext = new Mock<HttpContextBase>();
            var routeData = new RouteData();
            NameValueCollection nv;
            httpContext.Setup(c => c.Request.RequestContext.RouteData).Returns(routeData);
            httpContext.Setup(c => c.Request.Form).Returns(delegate
            {
                nv = new NameValueCollection { { "id_ProgEtu", "5" }, { "id_Session", "1" } };
                return nv;
            });

            etuController.ControllerContext = new ControllerContext(httpContext.Object, routeData, etuController);
            etuController.Create(_etudiant, page);
            _etudiant.Nom = "Wallet";
            etuController.Edit(_etudiant, page);
            var rechercheEtudiant = testRepository.FindPersonne(_etudiant.id_Pers);

            Assert.AreEqual("Wallet", rechercheEtudiant.Nom);
        }
    }
}
