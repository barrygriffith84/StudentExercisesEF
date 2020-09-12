using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentExercises_EF.Models;

namespace StudentExercises_EF.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<StudentExercises_EF.Models.Cohort> Cohort { get; set; }
        public DbSet<StudentExercises_EF.Models.Exercise> Exercise { get; set; }
        public DbSet<StudentExercises_EF.Models.Instructor> Instructor { get; set; }
        public DbSet<StudentExercises_EF.Models.Student> Student { get; set; }
        public DbSet<StudentExercises_EF.Models.StudentExercise> StudentExercise { get; set; }
    }
}
