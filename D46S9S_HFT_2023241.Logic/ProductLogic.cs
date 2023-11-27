using D46S9S_HFT_2023241.Models;
using D46S9S_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Logic
{
    internal class ProductLogic : IProductLogic
    {

        IRepository<Product> rep;

        public ProductLogic(IRepository<Product> rep)
        {


            this.rep = rep;
        }
        public void Create(Product ord)
        {

            if (ord.Price < 0)
            {
                throw new ArgumentException("Not possible");
            }
            this.rep.Create(ord);

        }
        public void Delete(int id)
        {
            this.rep.Delete(id);

        }
        public void Read(int id)
        {
            this.rep.Read(id);

        }
        public IQueryable<Product> ReadAll()
        {
            return this.rep.ReadAll();


        }
        public void Update(Product ord)
        {
            this.rep.Update(ord);

        }
    }
}
