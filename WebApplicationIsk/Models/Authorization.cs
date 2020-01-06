using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationIsk.Models
{
    public partial class Authorization
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string Login { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(32)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(32)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
