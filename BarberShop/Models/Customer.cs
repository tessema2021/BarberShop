using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    public class Customer
    {
        
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter the date.")]
        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime CreateDateTime { get; set; }
        public string Address { get; set; }
        public List<Service> Services { get; set; }
        public UserProfile UserProfile { get; set; }
        [DisplayName("Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
