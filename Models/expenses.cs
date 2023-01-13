using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Expense_Tracker.Models
{
    public class expenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long eid { get; set; }
        [Required(ErrorMessage = "Pleasse insert the Title")]
        [StringLength(30, ErrorMessage = "Title not more than 30 character")]

        public string title { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Description not  more than 50 character")]
        public string description { get; set; }
        [Required(ErrorMessage = "Plese enter your amount")]

        

        public long amount { get; set; }

        [Display(Name = "categories")]

        public virtual long cid { get; set; }

        [ForeignKey("cid")]

        public virtual  categories  cat_name {get;set;}

        public DateTime date { get; set; }

    }
}