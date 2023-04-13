using FrancaSW.Models;
using FrancaSW.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Services
{
    public interface IServiceLogin
    {
        Task<List<Usuario>> GetUsuarios();
        Task<CommandLogin> Login([FromBody] CommandLogin command);
    }
}
