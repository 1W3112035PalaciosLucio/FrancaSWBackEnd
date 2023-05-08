using Azure;
using FrancaSW.Commands;
using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;

namespace FrancaSW.Services
{
    public class ServiceStockMateriaPrima : IServiceStockMateriaPrima
    {
        private readonly FrancaSwContext context;
        private readonly ISecurityService securityService;

        public ServiceStockMateriaPrima(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService = _securityService;
        }
        public async Task<List<DtoListadoStockMateriaPrima>> GetListadoStockMateriaPrima()
        {
            return await context.StockMateriasPrimas.AsNoTracking().
                Select(x => new DtoListadoStockMateriaPrima
                {
                    IdStockMateriaPrima = x.IdStockMateriaPrima,
                    IdMateriaPrima = x.IdMateriaPrima,
                    Descripcion = x.IdMateriaPrimaNavigation.Descripcion,
                    Cantidad = x.Cantidad,
                    Precio = x.Precio,
                    StockMinimo = x.StockMinimo,
                    StockInicial = x.StockInicial,
                    FechaUltimoPrecio = x.FechaUltimoPrecio,
                    FechaUltimaActualizacion = x.FechaUltimaActualizacion

                }).ToListAsync();
        }

        public async Task<DtoStockMP> GetStockMateriaPrimaById(int id)
        {
            try
            {
                StockMateriasPrima stock = await context.StockMateriasPrimas.Where(c => c.IdStockMateriaPrima.Equals(id)).FirstOrDefaultAsync();
                DtoStockMP dto = new DtoStockMP();
                dto.IdStockMateriaPrima = stock.IdStockMateriaPrima;
                dto.IdMateriaPrima = stock.IdMateriaPrima;
                dto.Cantidad = stock.Cantidad;
                dto.Precio = stock.Precio;
                dto.StockInicial = stock.StockInicial;
                dto.StockMinimo = stock.StockMinimo;
                dto.FechaUltimaActualizacion = stock.FechaUltimaActualizacion;
                dto.FechaUltimoPrecio = stock.FechaUltimoPrecio;
                return dto;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<ResultBase> PostStockMP(StockMateriasPrima stockMp)
        {
            ResultBase resultado = new ResultBase();

            var stockMpExist = await context.StockMateriasPrimas.FindAsync(stockMp.IdMateriaPrima);
            if (stockMpExist != null)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Ya hay registros de esta materia prima en el stock";
                return resultado;
            }

            // Verificar si la materia prima existe en la base de datos
            if (await context.MateriasPrimas.FindAsync(stockMp.IdMateriaPrima) == null)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "La materia prima no existe en la base de datos";
                return resultado;
            }
            else
            {

                try
                {
                    await context.AddAsync(stockMp);
                    await context.SaveChangesAsync();



                    // Crear una nueva instancia de Historial_Stock_Materia_Prima
                    var historialStockMp = new HistorialStockMateriaPrima
                    {
                        Cantidad = stockMp.Cantidad,
                        Precio = stockMp.Precio,
                        FechaUltimaActualizacion = DateTime.Now,
                        IdMateriaPrima = stockMp.IdMateriaPrima,
                        TipoMovimiento = "NUEVO" // O cualquier otro valor que desees asignar
                    };

                    // Guardar la instancia de Historial_Stock_Materia_Prima en la base de datos
                    context.HistorialStockMateriaPrimas.Add(historialStockMp);
                    await context.SaveChangesAsync();

                    // Crear una nueva instancia de Historial_X_StockMP para relacionar los registros
                    var historialXStockMp = new HistorialXStockMp
                    {
                        IdHistorial = historialStockMp.IdHistorial,
                        IdStockMateriaPrima = stockMp.IdStockMateriaPrima
                    };

                    // Guardar la instancia de Historial_X_StockMP en la base de datos
                    context.HistorialXStockMps.Add(historialXStockMp);
                    await context.SaveChangesAsync();


                    resultado.Ok = true;
                    resultado.CodigoEstado = 200;
                    resultado.Message = "El stock fue registrado con éxito";
                    return resultado;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "Error al registrar el stock";
                    return resultado;
                }
            }
        }
        public async Task<ResultBase> PutStockMP(CommandStockMateriaPrima stockMp)
        {
            ResultBase resultado = new ResultBase();

            var stockMpExist = await context.StockMateriasPrimas
                .FirstOrDefaultAsync(c => c.IdStockMateriaPrima == stockMp.IdStockMateriaPrima);

            if (stockMpExist == null)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "El stock no existe en la base de datos";
                return resultado;
            }

            try
            {
                stockMpExist.IdMateriaPrima = stockMp.IdMateriaPrima;
                stockMpExist.StockMinimo = stockMp.StockMinimo;
                stockMpExist.StockInicial = stockMp.StockInicial;
                stockMpExist.Cantidad = stockMp.Cantidad;
                stockMpExist.Precio = stockMp.Precio;
                stockMpExist.FechaUltimaActualizacion = stockMp.FechaUltimaActualizacion;
                stockMpExist.FechaUltimoPrecio = stockMp.FechaUltimoPrecio;

                context.Update(stockMpExist);  
                await context.SaveChangesAsync();

                // Crear una nueva instancia de Historial_Stock_Materia_Prima
                var historialStockMp = new HistorialStockMateriaPrima
                {
                    Cantidad = stockMp.Cantidad,
                    Precio = stockMp.Precio,
                    FechaUltimaActualizacion = DateTime.Now,
                    IdMateriaPrima = stockMp.IdMateriaPrima,
                    TipoMovimiento = stockMp.TipoMovimiento // O cualquier otro valor que desees asignar
                };

                // Guardar la instancia de Historial_Stock_Materia_Prima en la base de datos
                context.HistorialStockMateriaPrimas.Add(historialStockMp);
                await context.SaveChangesAsync();

                // Crear una nueva instancia de Historial_X_StockMP para relacionar los registros
                var historialXStockMp = new HistorialXStockMp
                {
                    IdHistorial = historialStockMp.IdHistorial,
                    IdStockMateriaPrima = stockMp.IdStockMateriaPrima
                };

                // Guardar la instancia de Historial_X_StockMP en la base de datos
                context.HistorialXStockMps.Add(historialXStockMp);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El stock fue actualizado con éxito";
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al actualizar el stock";
                Console.WriteLine(ex);
                return resultado;
            }
        }
    }

}
