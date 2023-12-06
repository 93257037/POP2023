using HotelReservations.Model;
using HotelReservations.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                user.Id = userRepository.Insert(user);
                Hotel.GetInstance().Users.Add(user);
            }
            else
            {
                userRepository.Update(user);
                var index = Hotel.GetInstance().Users.FindIndex(u => u.Id == user.Id);
                Hotel.GetInstance().Users[index] = user;
            }
        }

        public void DeleteUser(int userId)
        {
            userRepository.Delete(userId);

            var users = Hotel.GetInstance().Users;
            var userToRemove = users.FirstOrDefault(u => u.Id == userId);

            if (userToRemove != null)
            {
                users.Remove(userToRemove);
            }
        }
    }
}
