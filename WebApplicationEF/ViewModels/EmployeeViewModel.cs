using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using WebApplicationEF.Models;

namespace WebApplicationEF.ViewModels
{
    public class EmployeeViewModel
    {

        //[NotMapped]
        //[DataType(DataType.Upload)]

        //public HttpPostedFileBase ImageUpload { get; set; }

        public string Command { get; set; }
    }
}