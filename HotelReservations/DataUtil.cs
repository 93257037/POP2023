using HotelReservations.Exceptions;
using HotelReservations.Model;
using HotelReservations.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations
{
    public class DataUtil
    {
        public static void LoadData()
        {
            Hotel hotel = Hotel.GetInstance();
            hotel.Id = 1;
            hotel.Name = "Hotel Park";
            hotel.Address = "Kod Futoskog parka...";

            var singleBedRoom = new RoomType()
            {
                Id = 1,
                Name = "Singe Bed"
            };

            var doubleBedRoom = new RoomType()
            {
                Id = 2,
                Name = "Double Bed"
            };

            var room1 = new Room()
            {
                Id = 1,
                RoomNumber = "02",
                HasTV = false,
                HasMiniBar = true,
                RoomType = singleBedRoom,
            };

            var room2 = new Room()
            {
                Id = 2,
                RoomNumber = "01",
                HasTV = true,
                HasMiniBar = true,
                RoomType = doubleBedRoom,
            };

            hotel.RoomTypes.Add(singleBedRoom);
            hotel.RoomTypes.Add(doubleBedRoom);

            hotel.Rooms.Add(room1);
            hotel.Rooms.Add(room2);

            Receptionist receptionist1 = new Receptionist()
            {
                Id = 1,
                JMBG = "10101990123456",
                FirstName = "Petar",
                LastName = "Perić",
                Username = "pera",
                Password = "password",
            };
            Receptionist receptionist2 = new Receptionist()
            {
                Id = 2,
                JMBG = "09091999654321",
                FirstName = "Marko",
                LastName = "Marković",
                Username = "marko",
                Password = "password",
            };
            Administrator administrator1 = new Administrator()
            {
                Id = 3,
                JMBG = "0809000654456",
                FirstName = "Marija",
                LastName = "Marić",
                Username = "marija",
                Password = "password",
            };

            Hotel.GetInstance().Users.Add(administrator1);
            Hotel.GetInstance().Users.Add(receptionist1);
            Hotel.GetInstance().Users.Add(receptionist2);


            // Može kada znamo da postoji rooms.txt datoteka
            // Ona bi trebalo da se nađe u potfolderu projektnog foldera
            // PopProjekat/bin/Debug
            try
            {
                IRoomRepository roomRepository = new RoomRepository();
                var loadedRooms = roomRepository.GetAll();

                if (loadedRooms != null)
                {
                    Hotel.GetInstance().Rooms = loadedRooms;
                }

                //var newRoom = new Room()
                //{
                //    RoomNumber = "03",
                //    HasTV = true,
                //    HasMiniBar = false,
                //    IsActive = true,
                //    RoomType = singleBedRoom
                //};

                //newRoom.Id = roomRepository.Insert(newRoom);
                //Hotel.GetInstance().Rooms.Add(newRoom);

                // Samo za primer...
                //BinaryRoomRepository binaryRoomRepository = new BinaryRoomRepository();
                //var loadedRoomsFromBin = binaryRoomRepository.Load();
            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("An unexpected error occured", ex.Message);
            }
        }

        public static void PersistData()
        {
            try
            {
                // Kada se gasi program, čuvamo u rooms.txt
                // Posle toga će rooms.txt postojati (ako nešto ne pođe po zlu)
                IRoomRepository roomRepository = new CSVRoomRepository();
                roomRepository.Save(Hotel.GetInstance().Rooms);


                //BinaryRoomRepository binaryRoomRepository = new BinaryRoomRepository();
                //binaryRoomRepository.Save(Hotel.GetInstance().Rooms);

            }
            catch (CouldntPersistDataException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
        }
    }
}