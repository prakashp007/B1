using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Expense_Tracker.Models
{
    public class TotalLimit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tid { get; set; }
        [Required]
        public long totalExpenselimit { get; set; }


    }
}