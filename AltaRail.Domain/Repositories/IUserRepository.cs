using AltaRail.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AltaRail.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> Authenticate(string username, string password);
    }
}
