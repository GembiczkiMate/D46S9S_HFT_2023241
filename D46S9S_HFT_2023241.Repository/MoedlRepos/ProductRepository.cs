using D46S9S_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Repository
{
    public class ProductRepository:Repository<Product>,IRepository<Product>
    {
        public ProductRepository(OrderDB db) : base(db)
        {
        }

        public override Product Read(int id)
        {
            return db.products.FirstOrDefault(t => t.ProductId == id);
        }

        public override void Update(Product item)
        {
            var old = Read(item.ProductId);
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
