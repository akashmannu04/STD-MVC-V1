using Microsoft.AspNetCore.Mvc;
using TopSpeed.Web.Data;
using TopSpeed.Web.Models;

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
    }
}
