using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using sachem.Models;

namespace sachem.Controllers
{
    
    public class ImporterController : Controller
    {
        //nom du repertoire de depot pour le fichier importé.
        private string IMPORTEDFILESDIRECTORY = "Fichier_a_traiter";
        private int MAXFILES = 1;
        private long MAXFILESIZE =10485760; //long.MaxValue pour ne pas le limiter, la valeur est en byte, default : 10485760 (10mo)
        private string EXTENSIONFILE = ".csv"; //en minuscule seulement
        private long MINFILESIZE = 0;
        // GET: Importer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = false;
            string Max = "";
            string fName = "";
            string fExt = "";
            string message = "";

            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    

                    fName = file.FileName;
                    fExt = Path.GetExtension(fName).ToLower();
                    if (file != null && file.ContentLength > MINFILESIZE && file.ContentLength < MAXFILESIZE)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}"+IMPORTEDFILESDIRECTORY, Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isDirExists = System.IO.Directory.Exists(pathString);

                        bool isFileExist = System.IO.File.Exists(pathString+"\\"+fName);

                        if (isFileExist)
                        {
                           message = Messages.I_035(fName);
                        }

                        if (!isDirExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        if (fExt == EXTENSIONFILE)
                        {
                            file.SaveAs(path);

                        }
                        else
                        {
                            message = Messages.C_007(EXTENSIONFILE);
                        }

                    }
                    else if(!(file.ContentLength > MINFILESIZE && file.ContentLength < MAXFILESIZE))
                    {
                        if (MAXFILESIZE != long.MaxValue)
                            Max = " et inférieur à " + MAXFILESIZE.ToString();

                       message = Messages.I_037(fName, Max);
                    }
                }

            }
            catch (Exception ex)
            {
                message = Messages.I_034(fName);
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

