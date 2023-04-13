using FrancaSW.Models;
using FrancaSW.Results;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Services
{
    public interface IServiceRegister
    {
        Task<ResultBase> PostRegister([FromBody] Usuario u);
       // Task<ResultBase> PutUsuario([FromBody] Usuario u);
    }
}
