using DevFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFitness.Domain.Repositories
{
    public interface IUserRepository 
    {
        Task <List<User>> GetAll();
        Task AddAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
