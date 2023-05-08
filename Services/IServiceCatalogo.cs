using FrancaSW.Commands;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceCatalogo
    {
        Task<List<Catalogo>> GetCatalogo();
        Task<List<DtoCatalogoCard>> GetCatalogoCard();
        Task<ResultBase> PostCatalogo(Catalogo catalogo);
        Task<ResultBase> PosttCatalogo(DtoAgregarCatalogo catalogo);
        Task<ResultBase> PutCatalogo(CommandCatalogo dtoCatalogo);
        Task<DtoCatalogo> GetCatalogoById(int id);
        Task<DtoListadoCatalogoProd> GetListaCatalogoProdById(int id);
        Task<List<DtoListadoCatalogo>> GetListadoCatalogo();
        Task<DtoCatalogoProd> GetListadoCatalogoProdd(int id);
        Task<ResultBase> eliminarCatalogo(int id);


    }
}
