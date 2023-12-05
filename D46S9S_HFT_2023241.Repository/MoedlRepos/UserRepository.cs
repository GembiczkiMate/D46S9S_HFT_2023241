using D46S9S_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Repository
{
    public class UserRepository:Repository<User>,IRepository<User>
    {
        public UserRepository(OrderDB db) : base(db)
        {
        }

        public override User Read(int id)
        {
            return db.users.FirstOrDefault(t => t.UserId == id);
        }

        public override void Update(User item)
        {
            User old = Read(item.UserId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            db.SaveChanges();
        }

        
    }
}
