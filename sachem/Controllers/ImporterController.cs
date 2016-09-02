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
                            ModelState.AddModelError(string.Empty, Messages.I_035(fName));
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
                            ModelState.AddModelError(string.Empty, Messages.C_007(EXTENSIONFILE));
                        }

                    }
                    else if(!(file.ContentLength > MINFILESIZE && file.ContentLength < MAXFILESIZE))
                    {
                        if (MAXFILESIZE != long.MaxValue)
                            Max = " et inférieur à " + MAXFILESIZE.ToString();
                        ModelState.AddModelError(string.Empty, Messages.I_037(fName, Max));
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, Messages.I_034(fName));
            }

            if (ModelState.IsValid)
            {
                // ViewBag.Success = string.Format(Messages.I_033(fName));
                TempData["Success"] = string.Format(Messages.I_033(fName));
                
                //return Json(new { Message = fName });
            }
            // return View("Index");
            return RedirectToAction("Index");
        }

    }

}

