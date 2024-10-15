// using BaseLibrary.DTOs;
// using BaseLibrary.Responses;

namespace DemoEmployeeManagementSolution
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register user);

        Task<LoginResponse> SignInAsync(Login user);


    } 
}