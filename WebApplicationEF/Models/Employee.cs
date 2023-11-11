using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationEF.Models
{
   
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        public string Address { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }




        public decimal Salary { get; set; }
        

        public bool Permanent { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.ImageUrl)]

        public string ImageUrl { get; set; }


        
        [NotMapped]
        [DataType(DataType.Upload)]
        [ScaffoldColumn(true)]

        public HttpPostedFileBase ImageUpload { get; set; }


        public virtual IList<Experience> Experiences { get; set; }



        public Employee()
        {
            Experiences = new List<Experience>();
        }

    }
}