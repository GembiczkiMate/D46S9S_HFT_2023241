using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public User()
        {

            Orders = new HashSet<Order>();
        }

        
    }
}
