using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUploadUsingDemo.Controllers
{
    public class FileUploadController : Controller
    {

        /// <summary>  
        /// 上传文件  
        /// 可自定义其他参数，查找自定义位置  
        /// </summary>  
        /// <param name="file"></param>  
        /// <returns></returns>  
        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase file)
        {
            //保存到临时文件夹  
            string urlPath = "/Resource/Uploads";
            string filePathName = string.Empty;

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Resource/Uploads");
            if (Request.Files.Count == 0)
            {
                return Json(new { status = 0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));

            return Json(new
            {
                status = 0,
                filePath = filePathName
            });

        }


        /// <summary>  
        /// 删除文件  
        /// </summary>  
        /// <param name="photoName">文件名</param>  
        /// <returns></returns>  
        public ActionResult DeletePhoto(string photoName)
        {

            string urlPath = "/Resource/Uploads/" + photoName;
            string localPath = HttpRuntime.AppDomainAppPath + urlPath;

            if (System.IO.File.Exists(localPath))
            {
                FileInfo fi = new FileInfo(localPath);

                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;

                System.IO.File.Delete(localPath);

                //返回删除状态  
                return Json(true);
            }

            return Json(false);
        }

        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        // GET: FileUpload/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FileUpload/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileUpload/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FileUpload/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FileUpload/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FileUpload/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FileUpload/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
