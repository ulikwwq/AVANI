using System.Collections.Generic;
using System.Threading.Tasks;
using AVANI.Client.Models;

namespace AVANI.Client.Services
{
    public class GoalService
    {
        private readonly List<GoalModel> _goals = new();

        public Task<IEnumerable<GoalModel>> GetGoalsAsync()
        {
            return Task.FromResult<IEnumerable<GoalModel>>(_goals);
        }

        public Task CreateGoalAsync(GoalModel goal)
        {
            _goals.Add(goal);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Simple heuristic "AI" price calculator for a goal title.
        /// Returns an approximate coin cost based on keywords and length.
        /// </summary>
        public Task<int> CalculateGoalPrice(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return Task.FromResult(5);

            var lower = title.ToLowerInvariant();
            int basePrice = 10;

            // Keyword boosts for common items
            if (lower.Contains("велосипед") || lower.Contains("bike")) basePrice += 120;
            if (lower.Contains("телефон") || lower.Contains("phone")) basePrice += 200;
            if (lower.Contains("коньки") || lower.Contains("skates")) basePrice += 80;
            if (lower.Contains("игрушк") || lower.Contains("lego") || lower.Contains("lego")) basePrice += 30;
            if (lower.Contains("компьютер") || lower.Contains("pc") || lower.Contains("laptop")) basePrice += 300;

            // Add small amount per word / character to reflect complexity
            var words = lower.Split(new char[] { ' ', '\t', '\n', '.', ',' }, StringSplitOptions.RemoveEmptyEntries).Length;
            basePrice += Math.Min(50, words * 4);

            var chars = lower.Length;
            basePrice += Math.Min(40, chars / 20);

            // Make sure price is at least 5
            basePrice = Math.Max(5, basePrice);
            return Task.FromResult(basePrice);
        }

        public Task<bool> UpdateGoalStatusAsync(string goalId, string newStatus)
        {
            var g = _goals.Find(x => x.GoalId == goalId);
            if (g is null) return Task.FromResult(false);
            g.Status = newStatus;
            return Task.FromResult(true);
        }
    }
}