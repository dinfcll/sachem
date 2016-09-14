using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string IMPORTEDFILESDIRECTORY = ConfigurationManager.AppSettings["RepertoireATraiter"];
        private int MAXFILES = 10;//nbre de fichier telechargeable
        private int MAXFILESIZE =20; //la taille maximale du fichier en mo.
        private string FILEEXTENSION = ".csv"; //en minuscule seulement

        // GET: Importer
        public ActionResult Index()
        {
            ViewBag.MAXFILES = MAXFILES;//transfere de la donnee maxfiles a la vue
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
                foreach (string fileName in Request.Files)//pour chaque fichier transfere
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    var originalDirectory = new DirectoryInfo(string.Format("{0}"+IMPORTEDFILESDIRECTORY, Server.MapPath(@"\"+ConfigurationManager.AppSettings["RepertoireFichier"]+@"\")));//on definit le nom du repertoire
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString());//on recupere le nom du repertoire
                    var fileName1 = Path.GetFileName(file.FileName);//on recupere le nom du fichier
                    bool isDirExists = System.IO.Directory.Exists(pathString);
                    bool isFileExist = System.IO.File.Exists(pathString+"\\"+fName);

                    if (isFileExist)//si le fichier existe (a deja ete uploade) on arrete
                    {
                        message = Messages.I_035(fName);
                    }
                    else
                    {
                        if (!isDirExists)//creation du repertoire sur le serveur s'il n'existe pas
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);//ici on sauvegarde, lorsque la verification de l'extension fonctionne
                    }
                }

            }
            catch (Exception ex)
            {
                message = Messages.I_034(fName);//erreur interne-- rare
            }

            if (message.IsEmpty())
            {
                return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);//affichera un crochet valide vert si tout c'est bien passé (message de succes)

            }
            else
            {
                Response.ClearHeaders();//efface l'entete de l'erreur par defaut du module dropzone pour mettre notre message d'erreur perso
                Response.ClearContent();//efface le contenu de l'erreur par defaut du module dropzone pour mettre notre message d'erreur perso
                Response.StatusCode = 500;
                Response.StatusDescription = "Erreur interne";
                return Json(new { errorMessage = message, JsonRequestBehavior.AllowGet });//affiche message d'erreur perso
            }
        }

    }

}

