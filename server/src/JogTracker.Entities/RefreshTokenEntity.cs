using System;

namespace JogTracker.Entities
{
    public class RefreshTokenEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
