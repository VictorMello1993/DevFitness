using DevFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFitness.Domain.Repositories
{
    public interface IMealRepository
    {
        Task AddAsync(Meal meal);
        Task<List<Meal>> GetAllByUserId(int userId);
        Task<Meal> GetByIdAsync(int userId, int mealId);
        Task SaveChangesAsync();

    }
}
