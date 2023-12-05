using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
        public User()
        {

            Orders = new HashSet<Order>();
        }
        public override bool Equals(object obj)
        {
            User other = obj as User;
            if (other == null)
            {
                return false;
            }
            else
            {
                return this.UserId == other.UserId
                    &&
                this.Username == other.Username;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Username, this.UserId);
        }


    }
}
