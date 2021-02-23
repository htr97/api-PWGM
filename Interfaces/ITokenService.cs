using Entities;

namespace Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}