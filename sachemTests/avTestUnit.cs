using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;
using System.Collections.Generic;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class avTestUnit
    {
        [TestMethod]
        public void DeleteNonExistingCoursSuivi()
        {
            var coursSuiviController = new CoursSuiviController();

            var result = coursSuiviController.Delete(null, 1);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void AddCoursSuiviToNonExistingPersonne()
        {
            var coursSuiviController = new CoursSuiviController();

            var result = coursSuiviController.Create(null);

            Assert.AreEqual(typeof(HttpStatusCodeResult), result.GetType());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((HttpStatusCodeResult)result).StatusCode);
        }

        //[TestMethod]
        //public void EAlexsadsdas()
        //{
        //    const int NO_SUIVI = 66;
        //    const int NO_PERS = 66;            
        //    var testRepository = new TestRepository();

        //    var coursSuiviController = new CoursSuiviController();

        //    coursSuiviController.Create(new CoursSuivi
        //    {
        //        id_CoursReussi=NO_SUIVI,
        //        id_Sess=1,
        //        id_Pers=NO_PERS,
        //        id_College=1,
        //        id_Statut=1,
        //        id_Cours=1,
        //        resultat=88,
        //        autre_College=null,
        //        autre_Cours=null
        //    }, NO_PERS);

        //    var result = coursSuiviController.Edit(NO_PERS, NO_SUIVI) as ViewResult;

        //    Assert.AreEqual(typeof(Cours), result.Model.GetType());
        //    Assert.AreEqual(NO_SUIVI, ((CoursSuivi)result.Model).id_CoursReussi);
        //    Assert.AreEqual(NO_PERS, ((CoursSuivi)result.Model).id_Pers);
        //}
    }
}
