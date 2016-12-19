using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            ensController.Create(_etudiant, new int());
            var rechercheEnsaignant = testRepository.FindPersonne(_etudiant.id_Pers);
            Assert.IsNotNull(rechercheEnsaignant);

        }
        [TestMethod]
        public void EtudiantModifier_ShouldReturnEtudiant()
        {
            var testRepository = new TestRepository();
            var etuController = new EtudiantController(testRepository);
            etuController.Create(_etudiant, new int());
            _etudiant.Nom = "Wallet";
            etuController.Edit(_etudiant, new int());
            var rechercheEtudiant = testRepository.FindPersonne(_etudiant.id_Pers);
            Assert.AreEqual("Wallet", rechercheEtudiant.Nom);
        }
    }
}
