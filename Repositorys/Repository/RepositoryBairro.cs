using MsBairro.MsContext;
using MsBairro.Repositorys.Entidades;
using MsBairro.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MsBairro.Dtos;

namespace MsBairro.Repositorys.Repository
{
    public class RepositoryBairro : IRepositoryBairro
    {
        protected MsBairroContext _dbContext;
        public RepositoryBairro(MsBairroContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddBairro(Bairro bairro)
        {
            try
            {
                _dbContext.Set<Bairro>().Add(bairro);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<string> DeleteBairro(Bairro bairro)
        {
            var message = string.Empty;
            try
            {
                _dbContext.Set<Bairro>().Remove(bairro);
                await _dbContext.SaveChangesAsync();
                message = "Registro excluído com sucesso !";

            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException exDb)
            {
                var innerException = exDb.InnerException;
                while (innerException != null)
                {

                    if (innerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "23503")
                    {
                        throw new Exception($"Não é possível excluir o registro porque há registros relacionados na tabela : {pgEx.TableName}", pgEx);
                    }
                    else
                    {
                        throw new Exception($"Ocorreu um erro interno : {innerException.Message}", innerException);
                    }

                }

            }

            return message;
        }

        public async Task<List<Bairro>> GetAll()
        {
            return await _dbContext.Set<Bairro>().ToListAsync();
        }

        public async Task<Bairro> GetById(int id)
        {
            var bairro = new Bairro();

            try
            {
                bairro = await _dbContext.Set<Bairro>().FindAsync(id);
            }
            catch (Exception)
            {
                bairro = null;
            }

            return bairro;
        }

        public async Task<PaginacaoDto> GetPaginacao(int pagina, string query)
        {
            var paginacao = new PaginacaoDto();

            try
            {

                IQueryable<Bairro> queryable = _dbContext.Bairro;

                if (!string.IsNullOrEmpty(query.Trim()))
                {
                    queryable = queryable.Where(x => EF.Functions.ILike(
                        EF.Functions.Unaccent(x.Nome),
                        $"%{query}%"))
                        .OrderBy(x => EF.Functions.ILike(
                        EF.Functions.Unaccent(x.Nome),
                        $"{query}%") ? 0 : 1)
                        .ThenBy(x => x.Nome);
                }
                else
                {
                    queryable = queryable.OrderByDescending(x => x.Id);
                }

                var total = await queryable.CountAsync();
                var totalPages = (int)Math.Ceiling(total / 10.0);
                pagina = Math.Min(Math.Max(1, pagina), totalPages);

                paginacao.Lista = await queryable.Skip((pagina - 1) * 10)
                                                 .Take(10)
                                                 .ToListAsync();

                paginacao.Count = totalPages;

            }
            catch (Exception)
            {
                paginacao = null;
            }

            return paginacao;
        }

        public async Task<bool> UpdateBairro(Bairro bairro)
        {
            try
            {
                _dbContext.Bairro.Update(bairro);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
