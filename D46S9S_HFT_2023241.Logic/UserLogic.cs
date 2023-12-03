using D46S9S_HFT_2023241.Models;
using D46S9S_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Logic
{
    public class UserLogic : IUserLogic
    {

        IRepository<User> rep;

        public UserLogic(IRepository<User> rep)
        {


            this.rep = rep;
        }
        public void Create(User ord)
        {

            if (ord.Username == string.Empty)
            {
                throw new ArgumentException("Not possible");
            }
            this.rep.Create(ord);

        }
        public void Delete(int id)
        {
            this.rep.Delete(id);

        }
        public User Read(int id)
        {
           return this.rep.Read(id);

        }
        public IQueryable<User> ReadAll()
        {
            return this.rep.ReadAll();


        }
        public void Update(User ord)
        {
            this.rep.Update(ord);

        }
    }
}
