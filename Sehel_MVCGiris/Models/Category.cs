using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sehel_MVCGiris.Models
{
    [RouteArea("Admin")]
    public class Category
    {
        
        public  int Id { get; set; }

        [Display(Name="Kategori İsmi:")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        [Required]
        [MaxLength(4000)]
        [Display(Name="Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set;}

        public virtual ICollection<Project> Projects { get; set; }

       

    }
}