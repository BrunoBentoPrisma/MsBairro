using MsBairro.Dtos;
using MsBairro.Repositorys.Entidades;
using MsBairro.Repositorys.Interfaces;
using MsBairro.Services.Interfaces;

namespace MsBairro.Services.Service
{
    public class BairroService : IBairroService
    {
        private readonly IRepositoryBairro _repositoryBairro;
        public BairroService(IRepositoryBairro repositoryBairro)
        {
            _repositoryBairro = repositoryBairro;
        }
        public async Task<bool> AddBairro(BairroDto bairroDto)
        {
            try
            {
                if (bairroDto == null) throw new Exception("Dados do bairro enviados nulos.");
                if (string.IsNullOrEmpty(bairroDto.Nome.Trim())) throw new Exception("Nome do bairro é obrigatório.");

                var bairro = new Bairro()
                {
                    Id = 0,
                    Nome = bairroDto.Nome
                };

                await this._repositoryBairro.AddBairro(bairro);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteBairro(int id)
        {
            try
            {
                if (id <= 0) throw new Exception("Id inválido");

                var bairro = await this._repositoryBairro.GetById(id);

                if (bairro == null) throw new Exception("Não foi possível localizar o bairro");

                var result = await this._repositoryBairro.DeleteBairro(bairro);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Bairro> GetById(int id)
        {
            try
            {
                if (id <= 0) throw new Exception("Id inválido");

                var bairro = await this._repositoryBairro.GetById(id);

                if (bairro == null) throw new Exception("Não foi possível localizar o bairro");

                return bairro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginacaoDto> GetPaginacao(int pagina, string query)
        {
            try
            {
                var paginacaoDto = await this._repositoryBairro.GetPaginacao(pagina, query);

                if (paginacaoDto == null) throw new Exception("Ocorreu um erro interno ao listar os bairros.");

                return paginacaoDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Bairro>> ListaBairro()
        {
            try
            {
                var bairros = await this._repositoryBairro.GetAll();

                if (bairros == null) throw new Exception("Ocorreu um erro interno ao listar os bairro, tente novamente mais tarde.");

                return bairros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateBairro(Bairro bairro)
        {
            try
            {
                if (bairro == null) throw new Exception("Dados do bairro enviados nulos.");
                if (string.IsNullOrEmpty(bairro.Nome.Trim())) throw new Exception("Nome do bairro é obrigatório.");

                await this._repositoryBairro.UpdateBairro(bairro);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
