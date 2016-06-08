using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace GolfSlayer.Models
{
    public class SignIn
    {
        [Required(ErrorMessage = "PIN is required")]
        public string PIN { get; set; }
    }
}
