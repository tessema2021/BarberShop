using System.Threading.Tasks;
using BarberShop.Auth.Models;

namespace BarberShop.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}