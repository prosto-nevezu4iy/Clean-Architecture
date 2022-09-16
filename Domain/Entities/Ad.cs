using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Ad : TrackEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<AdImage> Images { get; set; }
    }
}
