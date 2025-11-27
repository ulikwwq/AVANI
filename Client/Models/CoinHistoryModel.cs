using System;

namespace AVANI.Client.Models
{
    public class CoinHistoryModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}