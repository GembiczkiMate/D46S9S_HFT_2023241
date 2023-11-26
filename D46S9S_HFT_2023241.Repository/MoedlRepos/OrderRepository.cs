using D46S9S_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Repository
{
    public class OrderRepository : Repository<Order>, IRepository<Order>
    {
        public OrderRepository(OrderDB db) : base(db)
        {
        }

        public override Order Read(int id)
        {
           return db.orders.FirstOrDefault(t => t.OrderId == id);
        }

        public override void Update(Order item)
        {
            var old = Read(item.OrderId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            db.SaveChanges();  
        }
    }
}
