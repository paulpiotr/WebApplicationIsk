using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationIsk.Models
{
    public partial class User
    {
        public int Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(32)]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(64)]
        public string Email { get; set; }
        [Required]
        [MaxLength(32)]
        public string Role { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(32)]
        public string Login { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(32)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime Created { get; set; }
    }
}
