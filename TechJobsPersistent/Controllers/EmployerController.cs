using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext _context;
        public EmployerController (JobDbContext dbContext)
        {
            _context = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = _context.Employers.ToList();
            return View(employers);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel addModel = new AddEmployerViewModel();
            return View(addModel);
        }

        [HttpPost]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel newModel)
        {
            if (ModelState.IsValid)
            {
                Employer newEmployer = new Employer
                {
                    Name = newModel.Name,
                    Location = newModel.Location
                };
                _context.Employers.Add(newEmployer);
                _context.SaveChanges();
                return Redirect("/Employer");
            }

            return View("/Employer/Add",newModel);
        }

        public IActionResult About(int id)
        {
            Employer selected = _context.Employers.Find(id);
            return View(selected);
        }
    }
}
