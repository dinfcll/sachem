using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace sachem.Controllers
{
    
    public class ImporterController : Controller
    {
        //nom du repertoire de depot pour le fichier importé.
        private string IMPORTEDFILESDIRECTORY = "Fichier_a_traiter";
        private int MAXFILES = 1;
        private int MAXFILESIZE = 5;
        private string EXTENSIONFILE = "csv";
        // GET: Importer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string fExt = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    fName = file.FileName;
                    fExt = file.FileName.TrimStart('.');
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}"+IMPORTEDFILESDIRECTORY, Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

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
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

    }

}

