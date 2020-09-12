using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises_EF.Models.ViewModels
{
    public class StudentCohortViewModel
    {
        public Student student { get; set; }

        public List<SelectListItem> cohorts { get; set; } = new List<SelectListItem>();
    }
}
