using DevFitness.Applictation.Models.InputModels;
using DevFitness.Applictation.Models.ViewModels;
using DevFitness.Domain.Entities;
using DevFitness.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Controllers
{
    [Route("api/users/{userId}/meals")]
    public class MealsController : ControllerBase
    {
        private readonly IMealRepository _mealRepository;        

        public MealsController(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            var meals = await _mealRepository.GetAllByUserId(userId);
            var mealsViewModel = meals.Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.Date));            

            return Ok(mealsViewModel);
        }

        [HttpGet("{mealId}")]
        public async Task<IActionResult> GetById(int userId, int mealId)
        {
            var meal = await _mealRepository.GetByIdAsync(userId, mealId);

            if(meal == null)
            {
                return NotFound();
            }

            var mealViewModel = new MealViewModel(meal.Id, meal.Description, meal.Calories, meal.Date);

            return Ok(mealViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int userId, [FromBody] CreateMealInputModel inputModel)
        {
            var newMeal = new Meal(inputModel.Description, inputModel.Calories, inputModel.Date, userId);

            await _mealRepository.AddAsync(newMeal);
            await _mealRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { userId, mealId= newMeal.Id }, inputModel);
        }

        [HttpPut("{mealId}")]
        public async Task<IActionResult> Put(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            var meal = await _mealRepository.GetByIdAsync(userId, mealId);

            if(meal == null)
            {
                return NotFound();
            }

            meal.Update(inputModel.Description, inputModel.Calories, inputModel.Date);

            await _mealRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{mealId}")]
        public async Task<IActionResult> Delete(int userId, int mealId)
        {
            var meal = await _mealRepository.GetByIdAsync(userId, mealId);

            if(meal == null)
            {
                return NotFound();
            }

            meal.Deactivate();

            await _mealRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
