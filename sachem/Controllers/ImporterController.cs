using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using System.Web.WebPages;
using sachem.Models;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class ImporterController : Controller
    {
        private readonly string _importedfilesdirectory = ConfigurationManager.AppSettings["RepertoireATraiter"];
        private const int Maxfiles = 10;
        private const int Maxfilesize = 20;
        private const string Fileextension = ".csv";

        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Index()
        {
            ViewBag.MAXFILES = Maxfiles;
            ViewBag.MAXFILESIZE = Maxfilesize;
            ViewBag.FILEEXTENSION = Fileextension;

            return View();
        }

        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            var fName = "";
            var message = "";

            try
            {
                foreach (string fileName in Request.Files)
                {
                    var file = Request.Files[fileName];
                    if (file != null)
                    {
                        fName = file.FileName;
                        var originalDirectory = new DirectoryInfo(string.Format("{0}"+_importedfilesdirectory, Server.MapPath(@"\"+ConfigurationManager.AppSettings["RepertoireFichier"]+@"\")));
                        var pathString = Path.Combine(originalDirectory.ToString());
                        var isDirExists = Directory.Exists(pathString);
                        var isFileExist = System.IO.File.Exists(pathString+"\\"+fName);

                        if (isFileExist)
                        {
                            message = Messages.ImporterFichierAvecLeMemeNomExisteDeja(fName);
                        }
                        else
                        {
                            if (!isDirExists)
                            {
                                Directory.CreateDirectory(pathString);
                            }

                            var path = $"{pathString}\\{file.FileName}";
                            file.SaveAs(path);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                message = Messages.ImporterErreurTransfertFichier(fName)+"\n"+ex.Message;
            }

            if (message.IsEmpty())
            {
                return Json(new[] { new object() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.ClearHeaders();
                Response.ClearContent();
                Response.StatusCode = 500;
                Response.StatusDescription = "Erreur interne";
                return Json(new { errorMessage = message, JsonRequestBehavior.AllowGet });
            }
        }
    }
}

