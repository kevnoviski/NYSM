using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NYSM.Data;
using NYSM.Dtos;
using NYSM.Models;

namespace NYSM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("register")]
    public class RegisterController :ControllerBase
    {
        private readonly INYSMRepo _repository;
        private readonly IMapper _mapper;

        public RegisterController(INYSMRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpPost]
        public ActionResult<User> RegisterUser(User user)
        {
            if(user == null) 
                return BadRequest();
            PasswordHasher<User> passwordHasher= new PasswordHasher<User>();        
            user.Password = passwordHasher.HashPassword(user,user.Password);
            //matches the create model to the official table model
            var userModel = _mapper.Map<User>(user);
            //adds the 'merged' model to context
            _repository.CreateObject(userModel);
            //saves it
            _repository.SaveChanges();

            //converts an officil model to a read dto model
            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return Ok(userReadDto);
        }
    }
}