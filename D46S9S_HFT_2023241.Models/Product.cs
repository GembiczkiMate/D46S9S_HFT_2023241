using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public int Price { get; set; }

        [Required]
        public string ProductName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
        public Product() 
        {
            Orders = new HashSet<Order>();
        }

        public override bool Equals(object obj)
        {
            Product other = obj as Product;
            if (other == null)
            {
                return false;
            }
            else
            {
                return this.ProductName == other.ProductName
                && this.Price == other.Price
                && this.ProductId == other.ProductId;
               
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Price, this.ProductName, this.ProductId);
        }


    }



}
