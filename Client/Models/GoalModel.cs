using System;

namespace AVANI.Client.Models
{
    public class GoalModel
    {
        public string GoalId { get; set; }

        public string Title { get; set; }

        public int PriceCoins { get; set; }

        // "Active", "Completed", "Rejected"
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}