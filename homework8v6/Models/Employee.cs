using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace homework8v6.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FullName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Department { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Position { get; set; }
    }
}
