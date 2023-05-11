using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceFormula : IServiceFormula
    {
        public readonly FrancaSwContext context;
        public readonly ISecurityService securityService;

        public ServiceFormula(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService = _securityService;
        }

        public async Task<List<DtoListadoFormula>> GetListadoFormula()
        {
            return await context.Formulas.AsNoTracking().
                Select(x => new DtoListadoFormula
                {
                    IdFormula = x.IdFormula,
                    NombreProd = x.IdProductoNavigation.Nombre,
                    NombreMatP = x.IdMateriaPrimaNavigation.Descripcion,
                    CantidadMateriaPrima = x.CantidadMateriaPrima

                }).ToListAsync();
        }

        public async Task<ResultBase> PostFormula(Formula f)
        {
            //ResultBase resultado = new ResultBase();
            //try
            //{
            //    await context.AddAsync(f);

            //    await context.SaveChangesAsync();
            //    resultado.Ok = true;
            //    resultado.CodigoEstado = 200;
            //    resultado.Message = "Formula agregada correctamente";
            //    return resultado;
            //}
            //catch (Exception)
            //{
            //    resultado.Ok = false;
            //    resultado.CodigoEstado = 400;
            //    resultado.Message = "Error al cargar la formula";
            //    return resultado;
            //}
            ResultBase resultado = new ResultBase();

            // Verificar si ya existe un producto con el mismo IdProducto
            bool productoExistente = await context.Formulas.AnyAsync(formula => formula.IdProducto == f.IdProducto);

            if (productoExistente)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "El producto ya tiene una fórmula existente, por favor seleccione otro!";
                return resultado;
            }

            try
            {
                await context.AddAsync(f);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Formula agregada correctamente";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al cargar la formula";
                return resultado;
            }
        }


        public async Task<ResultBase> PutFormula(DtoFormula dtoFormula)
        {
            ResultBase resultado = new ResultBase();

            try
            {
                if (dtoFormula == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "No se encontró una formula";
                    return resultado;
                }

                var formula = await context.Formulas.FindAsync(dtoFormula.IdFormula);

                if (formula == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró una formula con el id {dtoFormula.IdFormula}";
                    return resultado;
                }


                formula.IdProducto = dtoFormula.IdProducto;
                formula.IdMateriaPrima = dtoFormula.IdMateriaPrima;
                formula.CantidadMateriaPrima = dtoFormula.CantidadMateriaPrima;


                context.Update(formula);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "La formula fue modificada correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al modificar la formula";
                resultado.Error = ex.ToString();
            }

            return resultado;
        }


        public async Task<DtoFormula> GetFormulaById(int id)
        {
            try
            {
                Formula formula = await context.Formulas.Where(c => c.IdFormula.Equals(id)).FirstOrDefaultAsync();
                DtoFormula dto = new DtoFormula();
                dto.IdFormula = formula.IdFormula;
                dto.IdProducto = formula.IdProducto;
                dto.IdMateriaPrima = formula.IdMateriaPrima;
                dto.CantidadMateriaPrima = formula.CantidadMateriaPrima;
                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
