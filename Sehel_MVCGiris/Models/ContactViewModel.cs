using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sehel_MVCGiris.Models
{
    public class ContactViewModel
    {
        [Display(Name="Ad")]
        [MaxLength(50)]
        [Required(ErrorMessage ="Ad alanı gereklidir")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Telefon")]
        [MaxLength(50)]
        [Required]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Mesaj")]
        [MaxLength(4050)]
        [Required]
        public string Message { get; set; }

        [Display(Name = "Kvkk")]
        [Required]
        public bool PrivacyPolicyAccepted { get; set; }

    }
}