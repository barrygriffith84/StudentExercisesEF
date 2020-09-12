using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentExercises_EF.Data;
using StudentExercises_EF.Models;
using StudentExercises_EF.Models.ViewModels;

namespace StudentExercises_EF.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Student.Include(s => s.Cohort);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Cohort)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            //ViewData tells us we need a view model
            //ViewData["CohortId"] = new SelectList(_context.Cohort, "Id", "Id");
           
            //Instantiate a view model
            StudentCohortViewModel vm = new StudentCohortViewModel();

            //Get all of the Cohorts from the database and select each cohort and turn it into a selectlistitem and then change the return into a list
            vm.cohorts = _context.Cohort.Select(c => new SelectListItem { 
            //The value has to be a string because values in HTML are all strings
            Value = c.Id.ToString(), 
            Text = c.Name
            }).ToList();

            //This is a benefit of a view model.  We were able to add a default option in the cohorts dropdown.
            //Create a select list item with a value of 0 and insert it at position 0.
            vm.cohorts.Insert(0, new SelectListItem() { Value = "0", Text = "Please Choose a Cohort" });


            return View(vm);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,SlackHandle,CohortId,Grade")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CohortId"] = new SelectList(_context.Cohort, "Id", "Id", student.CohortId);
            return View(student);
        }




        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            StudentCohortViewModel vm = new StudentCohortViewModel();
            
            // Get a single student from the database where the students ID matches the ID in the browser
            vm.student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);

            //Get all of the Cohorts from the database and select each cohort and turn it into a selectlistitem and then change the return into a list
            vm.cohorts = _context.Cohort.Select(c => new SelectListItem
            {
                //The value has to be a string because values in HTML are all strings
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            //This is a benefit of a view model.  We were able to add a default option in the cohorts dropdown.
            //Create a select list item with a value of 0 and insert it at position 0.
            vm.cohorts.Insert(0, new SelectListItem() { Value = "0", Text = "Please Choose a Cohort" });


            return View(vm);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,SlackHandle,CohortId,Grade")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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

            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Cohort)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
