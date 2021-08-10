using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext _context;

        public HomeController(JobDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = _context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            AddJobViewModel newModel = new AddJobViewModel(_context.Employers.ToList(), _context.Skills.ToList());
            return View(newModel);
        }

        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel newModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Employer empl = _context.Employers.Find(newModel.EmployerId);
                Job newJob = new Job
                {
                    Name = newModel.Name,
                    EmployerId = newModel.EmployerId,
                    Employer = empl
                };
                foreach (string item in selectedSkills)
                {
                    int newId = Int32.Parse(item);
                    JobSkill finalJob = new JobSkill
                    {
                        Job = newJob,
                        SkillId = newId
                    };
                    _context.Add(finalJob);
                }
                _context.Jobs.Add(newJob);
                _context.SaveChanges();
                return Redirect("/Home");
            }
            return View("/Home/Add", newModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = _context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = _context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
