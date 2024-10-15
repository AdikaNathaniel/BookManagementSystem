// using BaseLibrary.Responses;

namespace DemoEmployeeManagementSolution
{ 
    public record LoginResponse(
    bool Flag,
    string Message = null!,
    string Token = null!,
    string RefreshToken = null!);
}