using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public Product() 
        {
            Orders = new HashSet<Order>();
        }


    }



}
