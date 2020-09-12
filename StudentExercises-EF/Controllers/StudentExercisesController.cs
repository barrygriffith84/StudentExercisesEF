using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentExercises_EF.Data;
using StudentExercises_EF.Models;

namespace StudentExercises_EF.Controllers
{
    public class StudentExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentExercises
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentExercise.Include(s => s.Exercise).Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentExercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExercise = await _context.StudentExercise
                .Include(s => s.Exercise)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentExercise == null)
            {
                return NotFound();
            }

            return View(studentExercise);
        }

        // GET: StudentExercises/Create
        public IActionResult Create()
        {
            ViewData["ExerciseId"] = new SelectList(_context.Exercise, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id");
            return View();
        }

        // POST: StudentExercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,ExerciseId")] StudentExercise studentExercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercise, "Id", "Id", studentExercise.ExerciseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", studentExercise.StudentId);
            return View(studentExercise);
        }

        // GET: StudentExercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExercise = await _context.StudentExercise.FindAsync(id);
            if (studentExercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercise, "Id", "Id", studentExercise.ExerciseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", studentExercise.StudentId);
            return View(studentExercise);
        }

        // POST: StudentExercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,ExerciseId")] StudentExercise studentExercise)
        {
            if (id != studentExercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExerciseExists(studentExercise.Id))
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
            ViewData["ExerciseId"] = new SelectList(_context.Exercise, "Id", "Id", studentExercise.ExerciseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", studentExercise.StudentId);
            return View(studentExercise);
        }

        // GET: StudentExercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExercise = await _context.StudentExercise
                .Include(s => s.Exercise)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentExercise == null)
            {
                return NotFound();
            }

            return View(studentExercise);
        }

        // POST: StudentExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentExercise = await _context.StudentExercise.FindAsync(id);
            _context.StudentExercise.Remove(studentExercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExerciseExists(int id)
        {
            return _context.StudentExercise.Any(e => e.Id == id);
        }
    }
}
