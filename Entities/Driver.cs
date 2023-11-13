using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace jbus_api.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // * LINK
        public int UserId { get; set; }
        public User User { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
    }
}