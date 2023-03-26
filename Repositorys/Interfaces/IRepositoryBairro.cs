using MsBairro.Dtos;
using MsBairro.Repositorys.Entidades;

namespace MsBairro.Repositorys.Interfaces
{
    public interface IRepositoryBairro
    {
        Task<bool> AddBairro(Bairro bairro);
        Task<bool> UpdateBairro(Bairro bairro);
        Task<string> DeleteBairro(Bairro bairro);
        Task<Bairro> GetById(int id);
        Task<List<Bairro>> GetAll();
        Task<PaginacaoDto> GetPaginacao(int pagina, string query);
    }
}
