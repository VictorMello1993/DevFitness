using DevFitness.Domain.Entities;
using DevFitness.Domain.Repositories;
using DevFitness.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFitnessDbContext _dbContext;

        public UserRepository(DevFitnessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);            
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.Where(u => u.Active).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
