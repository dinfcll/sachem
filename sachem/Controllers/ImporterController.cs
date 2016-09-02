using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using sachem.Models;

namespace sachem.Controllers
{
    
    public class ImporterController : Controller
    {
        //nom du repertoire de depot pour le fichier importé.
        private string IMPORTEDFILESDIRECTORY = "Fichier_a_traiter";
        private int MAXFILES = 1;
        private long MAXFILESIZE = -1;
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
            bool isSavedSuccessfully = true;
            bool isExtAuthorized = true;
            bool isFileNotThere = true;
            bool isValidedSize = true;
            string Max = "";
            string fName = "";
            string fExt = "";
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

                        bool isFileExist = System.IO.File.Exists(pathString+fName);

                        if (isFileExist)
                        {
                            isFileNotThere = false;
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
                            isExtAuthorized = false;
                        }

                    }
                    else if(!(file.ContentLength > MINFILESIZE && file.ContentLength < MAXFILESIZE))
                    {
                        if (MAXFILESIZE != -1)
                            Max = " et inférieur à " + MAXFILESIZE.ToString();
                        isValidedSize = false;
                    }
                    else
                    {
                        isSavedSuccessfully = false;
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                ViewBag.Success = string.Format(Messages.I_033(fName));
                return Json(new { Message = fName });
            }
            else
            {
                ViewBag.Erreur = string.Format(Messages.I_034(fName));
            }

            if (!isValidedSize)
            {
                ViewBag.Erreur = string.Format(Messages.I_037(fName, Max));
            }

            if (!isExtAuthorized)
            {
                ViewBag.Erreur = string.Format(Messages.C_007(fExt));
            }
            if (!isFileNotThere)
            {
                ViewBag.Erreur = string.Format(Messages.I_035(fName));
            }
            return Json(new {Message = ""});
        }

    }

}

