﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sachem.Controllers
{
    public class ImporterController : Controller
    {
        // GET: Importer
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult SaveUploadedFile()
        //{
        //    bool isSavedSuccessfully = true;
        //    string fName = "";
        //    try
        //    {
        //        foreach (string fileName in Request.Files)
        //        {
        //            HttpPostedFileBase file = Request.Files[fileName];
        //            //Save file content goes here
        //            fName = file.FileName;
        //            if (file != null && file.ContentLength > 0)
        //            {

        //                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

        //                string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

        //                var fileName1 = Path.GetFileName(file.FileName);

        //                bool isExists = System.IO.Directory.Exists(pathString);

        //                if (!isExists)
        //                    System.IO.Directory.CreateDirectory(pathString);

        //                var path = string.Format("{0}\\{1}", pathString, file.FileName);
        //                file.SaveAs(path);

        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        isSavedSuccessfully = false;
        //    }


        //    if (isSavedSuccessfully)
        //    {
        //        return Json(new { Message = fName });
        //    }
        //    else
        //    {
        //        return Json(new { Message = "Error in saving file" });
        //    }
        //}
    }

}

