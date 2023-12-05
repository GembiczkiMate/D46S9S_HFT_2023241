using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [JsonIgnore]
        public  virtual User User { get; set; }
        

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [JsonIgnore]
        
        public virtual Product Products { get; set; }

       

        public DateTime OrderDate { get; set; }

        public override bool Equals(object obj)
        {
            Order other = obj as Order;
            if (other == null)
            {
                return false;
            }
            else
            {
                return this.OrderId == other.OrderId
                && this.UserId == other.UserId
                && this.ProductId == other.ProductId
                && this.OrderDate == other.OrderDate;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.OrderId, this.UserId,this.ProductId,this.OrderDate);
        }
    }


}
