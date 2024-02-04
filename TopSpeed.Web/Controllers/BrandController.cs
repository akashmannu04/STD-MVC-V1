using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopSpeed.Application.ApplicationConstant;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Common;

namespace TopSpeed.Web.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnivironment;// Adding one interface for root path
        public BrandController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnivironment) //Injected in constructor as well
        {
            _dbContext = dbContext;
            _webHostEnivironment = webHostEnivironment;// It handles. when we are submitting images
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Brandv2> brands=_dbContext.Brandv2.ToList(); //Adding our collection list to DB
            return View(brands);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brandv2 brandv2)
        {
            string webRootPath = _webHostEnivironment.WebRootPath; //It helps to access the path
            var file = HttpContext.Request.Form.Files;//It helps to get image request through form
            if (file.Count > 0)
            {
                string newFileName=Guid.NewGuid().ToString();//It is used to assign name for file
                var upload = Path.Combine(webRootPath, @"images\brand");//It is used to specify the path to upload a file
                var extension = Path.GetExtension(file[0].FileName);//It is used to get file extension
                using(var fileStream=new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create)) //It is used to copy the image
                {
                    file[0].CopyTo(fileStream); //respective file will copy in the respective folder
                }
                brandv2.BrandLogo = @"\images\brand\" + newFileName + extension;// It helps to store the path in the Brand model which we already specified
            }

            if (ModelState.IsValid)
            {
                _dbContext.Brandv2.Add(brandv2);
                _dbContext.SaveChanges();
                TempData["success"] = CommonMessage.RecordCreated;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            Brandv2 brandv2 = _dbContext.Brandv2.FirstOrDefault(x => x.Id == id);//It compares Db ID value and Brand ID value.
                                                                                 //If matches the both value.
                                                                                 //It will fetch the details to user 
            return View(brandv2);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Brandv2 brandv2 = _dbContext.Brandv2.FirstOrDefault(x => x.Id == id);
            return View(brandv2);
        }
        [HttpPost]
        public IActionResult Edit(Brandv2 brandv2)
        {
            string webRootPath = _webHostEnivironment.WebRootPath; //It helps to access the path
            var file = HttpContext.Request.Form.Files;//It helps to get image request through form
            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();//It is used to assign name for file
                var upload = Path.Combine(webRootPath, @"images\brand");//It is used to specify the path to upload a file
                var extension = Path.GetExtension(file[0].FileName);//It is used to get file extension
                //delete old image
                var objFromDb=_dbContext.Brandv2.AsNoTracking().FirstOrDefault(x=>x.Id==brandv2.Id);//It is used to find brand details and we are taking object from DB
                //AsNoTracking() -> It is used to avoid clash. Because we are taking image from EF core and delete the image. Similar time we are updating the same object 
                if(objFromDb != null) 
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));//We need to find old image path and trims the slash
                    if(System.IO.File.Exists(oldImagePath))//We need to check file is exist or not
                    {
                        System.IO.File.Delete(oldImagePath);//It is used to delete the old image
                    }
                
                }

                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create)) //It is used to copy the image
                {
                    file[0].CopyTo(fileStream); //respective file will copy in the respective folder
                }
                brandv2.BrandLogo = @"\images\brand\" + newFileName + extension;// It helps to store the path in the Brand model which we already specified
            }

            if (ModelState.IsValid)
            {
                var objFromDb = _dbContext.Brandv2.AsNoTracking().FirstOrDefault(x => x.Id == brandv2.Id);
                objFromDb.Name=brandv2.Name;
                objFromDb.EstablishedYear=brandv2.EstablishedYear;
                if(brandv2.BrandLogo!=null)
                {
                    objFromDb.BrandLogo = brandv2.BrandLogo;
                }

                _dbContext.Brandv2.Update(objFromDb); 
                _dbContext.SaveChanges();

                TempData["warning"] = CommonMessage.RecordUpdated;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(Guid id)
        {
            Brandv2 brandv2 = _dbContext.Brandv2.FirstOrDefault(x => x.Id == id);
            return View(brandv2);
        }
        [HttpPost]
        public IActionResult Delete(Brandv2 brandv2) {
            string webRootPath=_webHostEnivironment.WebRootPath;
            if (!string.IsNullOrEmpty(brandv2.BrandLogo))
            {
                var objFromDb=_dbContext.Brandv2.AsNoTracking().FirstOrDefault(x=>x.Id== brandv2.Id);
                if(objFromDb!=null)
                {
                    var oldImagePath=Path.Combine(webRootPath, brandv2.BrandLogo.Trim('\\'));
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }
            _dbContext.Brandv2.Remove(brandv2);
            _dbContext.SaveChanges();
            TempData["error"] = CommonMessage.RecordDeleted;
            return RedirectToAction(nameof(Index));
        }
    }
}
