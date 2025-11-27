using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AVANI.Client.Models;

namespace AVANI.Client.Services
{
    /// <summary>
    /// Minimal Firebase REST client for Blazor WebAssembly.
    /// Usage: register `FirebaseService` in DI and pass the Firebase base URL
    /// (e.g. "https://{project}.firebaseio.com") as the second parameter, or
    /// set the HttpClient.BaseAddress to the database base URL.
    /// All endpoints append ".json" as required by the Firebase REST API.
    /// </summary>
    public class FirebaseService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl; // no trailing slash
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public FirebaseService(HttpClient http, string baseUrl = null)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));

            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                _baseUrl = baseUrl.TrimEnd('/');
            }
            else if (http.BaseAddress is not null)
            {
                _baseUrl = http.BaseAddress.ToString().TrimEnd('/');
            }
            else
            {
                _baseUrl = string.Empty;
            }
        }

        private string BuildUrl(string path)
        {
            // path should not start with a slash
            var p = path?.TrimStart('/') ?? string.Empty;
            if (string.IsNullOrEmpty(_baseUrl))
                return p + ".json";
            return $"{_baseUrl}/{p}.json";
        }

        public async Task<UserModel?> GetUserById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            var url = BuildUrl($"users/{id}");
            try
            {
                var resp = await _http.GetAsync(url).ConfigureAwait(false);
                if (!resp.IsSuccessStatusCode) return null;
                var stream = await resp.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var user = await JsonSerializer.DeserializeAsync<UserModel>(stream, _jsonOptions).ConfigureAwait(false);
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task SaveUser(UserModel user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.Id)) throw new ArgumentException("User.Id must be provided for SaveUser.", nameof(user));

            var url = BuildUrl($"users/{user.Id}");
            var content = JsonContent.Create(user, options: _jsonOptions);
            var resp = await _http.PutAsync(url, content).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateCoins(string userId, int newAmount)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            var url = BuildUrl($"users/{userId}");
            // Use PATCH to only update the Coins property
            var patch = new { Coins = newAmount };
            var content = JsonContent.Create(patch, options: _jsonOptions);
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) { Content = content };
            var resp = await _http.SendAsync(request).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }

        public async Task AddGoal(string userId, GoalModel goal)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            if (goal is null) throw new ArgumentNullException(nameof(goal));

            if (string.IsNullOrWhiteSpace(goal.GoalId))
            {
                goal.GoalId = Guid.NewGuid().ToString();
            }

            // Place the goal under users/{userId}/Goals/{goalId}
            var url = BuildUrl($"users/{userId}/Goals/{goal.GoalId}");
            var content = JsonContent.Create(goal, options: _jsonOptions);
            var resp = await _http.PutAsync(url, content).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }
    }
}
using System.Threading.Tasks;

namespace AVANI.Client.Services
{
    public class FirebaseService
    {
        // Minimal placeholder for Firebase interactions
        public FirebaseService()
        {
            // TODO: Initialize Firebase client
        }

        public Task<string> UploadAvatarAsync(byte[] data)
        {
            // TODO: implement
            return Task.FromResult(string.Empty);
        }
    }
}