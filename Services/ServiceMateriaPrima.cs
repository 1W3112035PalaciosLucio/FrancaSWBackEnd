using FrancaSW.Comun;
using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace FrancaSW.Services
{
    public class ServiceMateriaPrima: IServiceMateriaPrima
    {
        private readonly FrancaSwContext context;
        private readonly ISecurityService securityService;

        public ServiceMateriaPrima(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService= _securityService;
        }

        //Listado de materias primas
        public async Task<List<MateriasPrima>> GetMateriaPrima()
        {
            return await context.MateriasPrimas.AsNoTracking().ToListAsync();
        }
        //Posteo de materias primas
        public async Task<ResultBase> PostMateriaPrima(MateriasPrima mp)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                await context.AddAsync(mp);
                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "La materia prima fue registrada con exito";
                return resultado;

            }
            catch (Exception)
            {

                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al ingresar la materia prima";
                return resultado;
            }
        }
        //Modificacion de materias primas
        public async Task<ResultBase> PutMateriaPrima(DTOMateriaPrima dtoMp)
        {
            ResultBase resultado = new ResultBase();
            var materiaPrima = await context.MateriasPrimas.Where(c => c.Codigo.Equals(dtoMp.Codigo)).FirstOrDefaultAsync();
            if (materiaPrima!=null)
            {
                materiaPrima.Descripcion = dtoMp.Descripcion;
                context.Update(materiaPrima);
                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Materia prima modificada correctamente";
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Hubo un error al modificar la materia prima";
            }
            return resultado;
        }
        //Listado de materias primas por id
        public async Task<DTOMateriaPrima> GetMateriaPrimaById(int id)
        {
            try
            {
                MateriasPrima mp = await context.MateriasPrimas.Where(c => c.Codigo.Equals(id)).FirstOrDefaultAsync();
                DTOMateriaPrima dto = new DTOMateriaPrima();
                dto.Codigo = mp.Codigo;
                dto.Descripcion = mp.Descripcion;
                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }
       
    }
}
