using Org.BouncyCastle.Asn1.Pkcs;
namespace fashion.Interfaces
{
    public interface IEmailService
    {
        void SendRegistrationEmail(string email);
    }
}
