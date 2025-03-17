using System.Diagnostics;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newkhandalapp.Data;
using newkhandalapp.Models;

namespace newkhandalapp.Controllers
{
    //[Route("api/Home")]
    //[ApiController]
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Index()
        {
            return View(_db.Users.ToList());
        }
        //[HttpGet]
        public IActionResult UserForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(User model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    //Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    Gotra = model.Gotra,
                    Address = model.Address,
                    Zipcode = model.Zipcode,
                    MaritalStatus = model.MaritalStatus,
                    Education = model.Education,
                    Occupation = model.Occupation
                };
                 // Save to database (if using Entity Framework)
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("UserForm");
        }
        //[HttpGet]
        public IActionResult Admin()
        {
            var admins = _db.Admins.ToList();
            return View(admins);
        }
        //[HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult CreateAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _db.Admins.Add(admin);
                _db.SaveChanges();
                return RedirectToAction("Admin"); // Redirect to Admin List
            }
            return View(admin);
        }
        //[HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(Admin admin)
        {
            admin = _db.Admins.FirstOrDefault(a => a.FirstName == admin.FirstName && a.Password == admin.Password);
            var users = _db.Users.Where(u => u.Zipcode == admin.Pincode).ToList();
            return View("ApprovalPage",users);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveUser(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Status = "Approved"; // Update status
            await _db.SaveChangesAsync(); // Save changes to DB

            return RedirectToAction("Index"); // Redirect back to the user list
        }
        //[HttpPatch("{Id:int}")]
        //public IActionResult ApprovalPage(int Id)
        //{
        //    var user = _db.Users.Find(Id);
        //    user.Status = "Approved";
        //    _db.Entry(user).State = EntityState.Modified;
        //    _db.Users.Update(user);
        //    _db.SaveChanges(); 
        //    return RedirectToAction(); 
        //}
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
