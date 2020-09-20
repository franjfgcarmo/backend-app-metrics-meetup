using System.Collections.Generic;
using WebMetrics.Api.Auth.Entities;

namespace WebMetrics.Api.Auth.Sevices
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User GetById(int id);
        IEnumerable<User> GetAll();
    }
}
