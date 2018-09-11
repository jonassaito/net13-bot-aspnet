using System;
namespace SimpleBot.Repository.Shared.Interfaces
{
    public interface IUserProfileRepository
    {
        UserProfile GetProfile(string id);
        void SetProfile(UserProfile profile);
    }
}
