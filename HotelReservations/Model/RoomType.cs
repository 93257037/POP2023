using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    [Serializable]
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return Name;
        }

        // rt1.Equals(null)
        // rt1.Equals(hootel)
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            
            var other = obj as RoomType;
            if(other == null) return false;

            return Id == other.Id;
        }
    }
}
