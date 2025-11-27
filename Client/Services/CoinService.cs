using System.Collections.Generic;
using System.Threading.Tasks;
using AVANI.Client.Models;

namespace AVANI.Client.Services
{
    public class CoinService
    {
        private readonly List<CoinHistoryModel> _history = new();

        public Task<int> GetBalanceAsync()
        {
            int balance = 0;
            foreach (var h in _history) balance += h.Amount;
            return Task.FromResult(balance);
        }

        public Task AddHistoryAsync(CoinHistoryModel entry)
        {
            _history.Add(entry);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<CoinHistoryModel>> GetHistoryAsync()
        {
            return Task.FromResult<IEnumerable<CoinHistoryModel>>(_history);
        }

        public async Task<bool> PenalizeCoins(string userId, int amount, string reason)
        {
            if (amount <= 0) return false;

            var entry = new CoinHistoryModel
            {
                Id = _history.Count + 1,
                Amount = -amount,
                Reason = reason ?? "Штраф",
                Timestamp = System.DateTime.UtcNow
            };

            await AddHistoryAsync(entry);
            return true;
        }
    }
}