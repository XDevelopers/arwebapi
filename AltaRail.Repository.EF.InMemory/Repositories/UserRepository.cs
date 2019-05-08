using AltaRail.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AltaRail.Repository.EF.InMemory.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(GenericContext context): base(context)
        {
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var user = await FindAsync(u => u.Username == username && u.Password == password);
            return user != null;
        }
    }
}
