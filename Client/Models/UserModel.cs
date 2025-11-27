using System.Collections.Generic;

namespace AVANI.Client.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        // "Parent" or "Child"
        public string Role { get; set; }

        // "Male" or "Female"
        public string Gender { get; set; }

        public int Coins { get; set; }

        public List<GoalModel> Goals { get; set; }

        // Simple default constructor
        public UserModel()
        {
            Goals = new List<GoalModel>();
        }
    }
}namespace AVANI.Client.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
    }
}