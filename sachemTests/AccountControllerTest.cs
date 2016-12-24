using Microsoft.VisualStudio.TestTools.UnitTesting;
using sachem.Methodes_Communes;

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

        [TestMethod]
        public void CryptoProcedureShouldReturnHashedThenPlainText()
        {
            const string stringDecrypte = "Admin123*";
            const string stringCrypte = "iQQrRrrExWxcdlrYSuyrGA==";

            var mpCrypte = Crypto.Encrypt(stringDecrypte, "P2m49sdJj8sdCChj213A4h2F1349ga7hs");
            var mpDecrypte = Crypto.Decrypt(mpCrypte, "P2m49sdJj8sdCChj213A4h2F1349ga7hs");

            Assert.AreEqual(stringCrypte, mpCrypte);
            Assert.AreEqual(stringDecrypte, mpDecrypte);
        }
    }
}
