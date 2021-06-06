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
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userRepository.GetAll();
            var usersViewModel = users.Select(u => new UserViewModel(u.Id, u.FullName, u.Height, u.Weight, u.BirthDate));            

            return Ok(usersViewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel(user.Id, user.FullName, user.Height, user.Weight, user.BirthDate);

            return Ok(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserInputModel inputModel)
        {            
            var newUser = new User(inputModel.FullName, inputModel.Height, inputModel.Weight, inputModel.BirthDate);

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, inputModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserInputModel inputModel)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            user.Update(inputModel.Height, inputModel.Weight);

            await _userRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            user.Deactivate();

            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
