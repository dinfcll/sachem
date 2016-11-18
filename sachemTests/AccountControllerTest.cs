using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Controllers;
using System.Collections.Generic;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void EncryptionChaineShouldReturnMD5Hash()
        {
            string stringSecreteAHasherEnMD5 = "SomeVeryImportantStringToHide";
            var retour = SachemIdentite.encrypterChaine(stringSecreteAHasherEnMD5);
            Assert.AreEqual("d1112b3d4ed431b10e30c838121d3a22", retour);
        }
    }
}
