using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThucHanh_2.Data;
using ThucHanh_2.Models;
using System.Collections.Generic;

namespace ThucHanh_2.Controllers
{
    
    public class LearnerController : Controller
    {
        
        private SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var learners= db.Learners.Include(m => m.Major).ToList();
            return View(learners);
        }
        public IActionResult Create()
        {
            var majors = new List<SelectListItem>(); //cách 1
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,

                    Value = item.MajorID.ToString()
                });

            }
            ViewBag.MajorID = majors;
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName"); //cách 2
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")]
Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //lại dùng 1 trong 2 cách tạo SelectList gửi về View để hiển thị danh sách Majors
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }
        public IActionResult Edit(int id)
        {
            if(id== null || db.Learners == null)
            {
                return NotFound();
            }
            var learner= db.Learners.Find(id);
            if(learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstName,LastName,MajorID,EnrollmentDate")] Learner learner) {
        if(id != learner.LearnerID)
            {
                return NotFound();
            }
        if(ModelState.IsValid)
            {
                try {
                    db.Update(learner);
                    db.SavedChanges();
                }
                catch(DbUpdateConcurrencyException) { 
                if(!LearnerExists(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
        ViewBag.MajorID= new SelectList(db.Majors,"MajorID","MajorName",learner.MajorID);
            return View(learner);
        }

        private bool LearnerExists(int learnerID)
        {
            throw new NotImplementedException();
        }
    }
}
