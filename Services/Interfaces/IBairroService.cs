using MsBairro.Dtos;
using MsBairro.Repositorys.Entidades;

namespace MsBairro.Services.Interfaces
{
    public interface IBairroService
    {
        Task<bool> AddBairro(BairroDto bairroDto);
        Task<bool> UpdateBairro(Bairro bairro);
        Task<string> DeleteBairro(int id);
        Task<Bairro> GetById(int id);
        Task<List<Bairro>> ListaBairro();
        Task<PaginacaoDto> GetPaginacao(int pagina, string query);
    }
}
