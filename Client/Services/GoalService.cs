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

        public Task<bool> UpdateGoalStatusAsync(string goalId, string newStatus)
        {
            var g = _goals.Find(x => x.GoalId == goalId);
            if (g is null) return Task.FromResult(false);
            g.Status = newStatus;
            return Task.FromResult(true);
        }
    }
}