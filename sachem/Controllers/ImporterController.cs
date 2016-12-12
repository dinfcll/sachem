using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using sachem.Models;
using static sachem.Classes_Sachem.ValidationAcces;

namespace sachem.Controllers
{
    public class ImporterController : Controller
    {
        private string IMPORTEDFILESDIRECTORY = ConfigurationManager.AppSettings["RepertoireATraiter"];
        private int MAXFILES = 10;//nbre de fichier telechargeable
        private int MAXFILESIZE =20; //la taille maximale du fichier en mb.
        private string FILEEXTENSION = ".csv"; //en minuscule seulement

        // GET: Importer
        [ValidationAccesSuper]
        public ActionResult Index()
        {
            ViewBag.MAXFILES = MAXFILES;
            ViewBag.MAXFILESIZE = MAXFILESIZE;
            ViewBag.FILEEXTENSION = FILEEXTENSION;
            return View();
        }

        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            string fName = "";
            string message = "";

            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    var originalDirectory = new DirectoryInfo(string.Format("{0}"+IMPORTEDFILESDIRECTORY, Server.MapPath(@"\"+ConfigurationManager.AppSettings["RepertoireFichier"]+@"\")));
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString());
                    var fileName1 = Path.GetFileName(file.FileName);
                    bool isDirExists = System.IO.Directory.Exists(pathString);
                    bool isFileExist = System.IO.File.Exists(pathString+"\\"+fName);

                    if (isFileExist)
                    {
                        message = Messages.I_035(fName);
                    }
                    else
                    {
                        if (!isDirExists)
                        {
                            System.IO.Directory.CreateDirectory(pathString);
                        }

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);
                    }
                }

            }
            catch (Exception ex)
            {
                message = Messages.I_034(fName)+"\n"+ex.Message;
            }

            if (message.IsEmpty())
            {
                return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
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

