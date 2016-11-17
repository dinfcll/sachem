using sachem.Controllers;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Net;
using sachem.Models;

namespace sachemTests
{
    /// <summary>
    /// Description résumée pour TestsCristianZubieta
    /// </summary>
    [TestClass]
    public class TestsCristianZubieta
    {
        public TestsCristianZubieta()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void DeplacerIdNull()
        {
            var groupecontroller = new GroupesController();

            var resultat = groupecontroller.Deplacer(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), resultat.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)resultat).StatusCode);
        }
        [TestMethod]
        public void SupprimePersonneExistante()
        {
            const int id_PersonneCree = 1500;
            var testrepository = new TestRepository();
            testrepository.AddPersonne(new Personne {Actif = true,Nom = "Carel",Prenom = "Ford",
                id_Pers = id_PersonneCree,id_TypeUsag = 1,Matricule = "201639488"});
            var personneController = new PersonnesController(testrepository);

            var result = personneController.Delete(id_PersonneCree) as ViewResult; 

            Assert.AreEqual(typeof(Personne), result.Model.GetType());
            Assert.AreEqual(id_PersonneCree, ((Personne)result.Model).id_Pers);

        }
    }
}
