using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fitness_Applicatie.Models;
using FitTracker.Logic;
using FitTracker.LogicInterface;
using FitTracker.LogicFactory;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using FitTracker.Interface.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Fitness_Applicatie.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel accountViewModel)
        {
            if (!ModelState.IsValid) return View(accountViewModel);

            UserCollection userCollection = new UserCollection();
            User user = ConvertUserDTO(userCollection.GetUser(accountViewModel.UserName));

            //check if user exists
            if (String.IsNullOrEmpty(user.Name))
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(accountViewModel);
            }

            //check password
            var hasher = new PasswordHasher<User>();
            if (hasher.VerifyHashedPassword(user, user.Password, accountViewModel.Password) == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Username of password is incorrect");
                return View(accountViewModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Id", user.UserID.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
            return LocalRedirect("/Home/Index");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            var hasher = new PasswordHasher<User>();
            User tempUser = new User();
            string hashedPW = hasher.HashPassword(tempUser, loginViewModel.Password);
            UserCollection userCollection = new UserCollection();
            User user = new User(loginViewModel.UserName, Guid.NewGuid(), hashedPW, null, null);
            userCollection.AddUser(ConvertUser(user));

            return LocalRedirect("/Account/Login");
        }

        private User ConvertUserDTO(UserDTO userDTO)
        {
            User user = new User(userDTO.Name, userDTO.UserID, userDTO.Password, ConvertTrainingDTOs(userDTO.GetTrainings()), ConvertExerciseDTOs(userDTO.GetExercises()));

            return user;
        }

        private UserDTO ConvertUser(User user)
        {
            UserDTO userDTO = new UserDTO(user.Name, user.UserID, user.Password, ConvertTraining(user.GetTrainings()), ConvertExercise(user.GetExercises()));
            return userDTO;
        }

        private List<TrainingDTO> ConvertTraining(List<Training> trainings)
        {
            List<TrainingDTO> trainingDTOs = new List<TrainingDTO>();
            if (trainings != null)
            {
                foreach (var training in trainings)
                {
                    TrainingDTO trainingDTO = new TrainingDTO(training.TrainingID, training.UserID, training.Date, (TrainingTypeDTO)training.TrainingType);
                    trainingDTOs.Add(trainingDTO);
                }
            }

            return trainingDTOs;
        }

        private List<Training> ConvertTrainingDTOs(List<TrainingDTO> trainingDTOs)
        {
            List<Training> trainings = new List<Training>();
            foreach (var trainingDTO in trainingDTOs)
            {
                Training training = new Training(trainingDTO.TrainingID, trainingDTO.UserID, trainingDTO.Date, (TrainingType)trainingDTO.TrainingType);
                trainings.Add(training);
            }
            return trainings;
        }

        private List<ExerciseDTO> ConvertExercise(List<Exercise> exercises)
        {
            List<ExerciseDTO> exerciseDTOs = new List<ExerciseDTO>();
            if (exercises != null)
            {
                foreach (var exercise in exercises)
                {
                    ExerciseDTO exerciseDTO = new ExerciseDTO(exercise.ExerciseID, exercise.Name, exercise.UserID, (ExerciseTypeDTO)exercise.ExerciseType);
                    exerciseDTOs.Add(exerciseDTO);
                }
            }
            return exerciseDTOs;
        }

        private List<Exercise> ConvertExerciseDTOs(List<ExerciseDTO> exerciseDTOs)
        {
            List<Exercise> exercises = new List<Exercise>();
            if (exerciseDTOs != null)
            {
                foreach (var exerciseDTO in exerciseDTOs)
                {
                    Exercise exercise = new Exercise(exerciseDTO.ExerciseID, exerciseDTO.Name, exerciseDTO.UserID, (ExerciseType)exerciseDTO.ExerciseType);
                    exercises.Add(exercise);
                }
            }

            return exercises;
        }
    }
}
