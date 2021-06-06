using DevFitness.Domain.Entities;
using DevFitness.Domain.Repositories;
using DevFitness.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.Infrastructure.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly DevFitnessDbContext _dbContext;

        public MealRepository(DevFitnessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Meal meal)
        {
            await _dbContext.Meals.AddAsync(meal);
        }

        public async Task<List<Meal>> GetAllByUserId(int userId)
        {
            return await _dbContext.Meals.Where(m => m.UserId == userId && m.Active).ToListAsync();
        }

        public async Task<Meal> GetByIdAsync(int userId, int mealId)
        {
            return await _dbContext.Meals.SingleOrDefaultAsync(m => m.UserId == userId && m.Id == mealId && m.Active);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
