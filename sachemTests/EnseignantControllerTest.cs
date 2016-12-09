using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;
using System.Collections.Generic;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class EnseignantControllerTest
    {
        const int ID_PERS = 4;
        Personne enseignant = new Personne
        {
            Actif = true,
            id_Pers = ID_PERS,
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
            var EnsController = new EnseignantController(testRepository);
            EnsController.Create(enseignant);
            var rechercheEnsaignant = testRepository.FindEnseignant(enseignant.id_Pers);
            Assert.IsNotNull(rechercheEnsaignant);

        }
        [TestMethod]
        public void ModifierEnseignantQuiExisteFonctionne()
        {
            var testRepository = new TestRepository();
            var EnsController = new EnseignantController(testRepository);
            EnsController.Create(enseignant);
            enseignant.Nom = "patate";
            EnsController.Edit(enseignant);
            var rechercheEnseignant = testRepository.FindEnseignant(enseignant.id_Pers);
            Assert.AreEqual("patate", rechercheEnseignant.Nom);
        }
    }
}
