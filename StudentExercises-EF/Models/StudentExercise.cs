using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises_EF.Models
{
    public class StudentExercise
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        
    }
}
