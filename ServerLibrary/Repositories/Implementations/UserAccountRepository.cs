using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
// using  Constants = ServerLibrary.Helpers.Constants;
// // using BaseLibrary.DTOs;
// using  BaseLibrary.Responses;
// using ServerLibrary.Repositories.Contracts;

// using BaseLibrary.DTOs;



namespace DemoEmployeeManagementSolution
{
    //Injection of AppDbContext is done here
    public class UserAccountRepository(IOptions<JwtSection> config,AppDbContext appDbContext) : IUserAccount
    {
        
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
             if(user is null) return new GeneralResponse(false,"Model is Empty");

             var checkUser = await FindUserByEmail(user.Email!);
             if(checkUser != null) return new GeneralResponse(false,"User registered Already");

             //Save the user
             var applicationUser = await AddToDatabase(new ApplicationUser()
             {  
                 Fullname = user.FullName,
                 Email = user.Email,
                 Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
             });

             // Check,create and assign roles
              var checkAdminRole =await appDbContext.SystemRoles.FirstOrDefaultAsync(_=>_.Name!.Equals(Constants.Admin));
              if(checkAdminRole is null)
              {
                var createAdminRole = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
                await AddToDatabase(new UserRole(){ RoleId = createAdminRole.Id, UserId = applicationUser.Id});
                return new GeneralResponse(true,"Account Created!");
              }

              var checkUserRole =await appDbContext.SystemRoles.FirstOrDefaultAsync(_=>_.Name!.Equals(Constants.User));
              SystemRole response = new();
              if(checkUserRole is null)
              {
                 response  = await AddToDatabase(new SystemRole() { Name = Constants.User });
                await AddToDatabase(new UserRole(){ RoleId = response.Id, UserId = applicationUser.Id});
              }
              else
              {
                await AddToDatabase(new UserRole() { RoleId = checkUserRole.Id, UserId = applicationUser.Id });
              }
              return new GeneralResponse(true,"Account Created!");
        }

        public Task<LoginResponse> SignInAsync(Login user)
        {
            throw new NotImplementedException();
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
           await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_=>_.Email!.ToLower()!.Equals(email!.ToLower()));


           private async Task<T> AddToDatabase<T>(T model)
           {
              var result = appDbContext.Add(model!);
              await appDbContext.SaveChangesAsync();
              return (T)result.Entity;
           }
    }
}