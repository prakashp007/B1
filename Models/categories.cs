using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Expense_Tracker.Models
{
    public class categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long cid { get; set; }

        [Required(ErrorMessage ="please enter categorie name")]
        [MaxLength(50)]

        public string cname { get; set; }

        [Required(ErrorMessage ="Please enter your expense limit")]

        public long cexpenselimit { get; set; }
    }
}