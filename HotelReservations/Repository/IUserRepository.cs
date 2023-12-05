using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Repository
{
    public interface IUserRepository
    {
        List<User> GetAll();
        int Insert(User user);
        void Update(User user);
        void Delete(int userId);
    }
}
