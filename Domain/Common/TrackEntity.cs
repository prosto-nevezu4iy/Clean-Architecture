using System;

namespace Domain.Common
{
    public abstract class TrackEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
