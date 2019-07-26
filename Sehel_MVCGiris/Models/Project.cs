using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sehel_MVCGiris.Models
{
    [RouteArea("Admin")]
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Başlık")]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Description{get;set;}

        [DataType(DataType.Html)]
        public string Body { get; set; }

        [Display(Name = "Resim")]
        public string Photo { get; set; }


        public int? CategoryId { get; set;}

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }


    }
}