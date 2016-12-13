using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Models;

namespace sachemTests
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void EncryptionChaineShouldReturnMd5Hash()
        {
            const string stringSecreteAHasherEnMd5 = "SomeVeryImportantStringToHide";
            var retour = SachemIdentite.EncrypterChaine(stringSecreteAHasherEnMd5);
            Assert.AreEqual("d1112b3d4ed431b10e30c838121d3a22", retour);
        }
    }
}
