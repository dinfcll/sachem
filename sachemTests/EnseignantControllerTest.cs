using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class EnseignantControllerTest
    {
        private const int IdPers = 4;

        private readonly Personne _enseignant = new Personne
        {
            Actif = true,
            id_Pers = IdPers,
            id_Sexe = 1,
            id_TypeUsag = 2,
            Nom = "Heure",
            Prenom = "Taist",
            NomUsager = "heuret",
            Courriel = "Test@123.ca",
            Telephone = "1234567890",
            MP = "1234",
            ConfirmPassword = "1234",
            DateNais = new System.DateTime(1111, 11, 11)
        };

        [TestMethod]
        public void AjoutEnseignantRemplisFonctionne()
        {
            var testRepository = new TestRepository();
            var ensController = new EnseignantController(testRepository);
            ensController.Create(_enseignant);
            var rechercheEnsaignant = testRepository.FindEnseignant(_enseignant.id_Pers);
            Assert.IsNotNull(rechercheEnsaignant);

        }
        [TestMethod]
        public void ModifierEnseignantQuiExisteFonctionne()
        {
            var testRepository = new TestRepository();
            var ensController = new EnseignantController(testRepository);
            ensController.Create(_enseignant);
            _enseignant.Nom = "patate";
            ensController.Edit(_enseignant);
            var rechercheEnseignant = testRepository.FindEnseignant(_enseignant.id_Pers);
            Assert.AreEqual("patate", rechercheEnseignant.Nom);
        }
    }
}
