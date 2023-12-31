﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    // Popričali smo o 4 različite varijante kako je moguće
    // implementirati korisnike. Odabrali smo ovu kako biste
    // probali nasleđivanje u C# i utvrdili polimorfizam. 
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
