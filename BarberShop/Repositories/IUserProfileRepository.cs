using BarberShop.Models;

namespace BarberShop.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseId(string firebaseId);
        UserProfile GetById(int id);
    }
}
