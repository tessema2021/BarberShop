namespace BarberShop.Auth.Models
{
    public class FirebaseUser
    {
        public string Email { get; }
        public string FirebaseId { get; }

        public FirebaseUser(string email, string firebaseId)
        {
            Email = email;
            FirebaseId = firebaseId;
        }
    }
}
