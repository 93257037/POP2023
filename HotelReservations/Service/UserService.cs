
using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Service
{
    public class UserService
    {
        public List<User> GetAllUsers()
        {
            return Hotel.GetInstance().Users;
        }
    }
}
