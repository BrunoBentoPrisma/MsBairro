using MsBairro.Repositorys.Entidades;

namespace MsBairro.Dtos
{
    public class PaginacaoDto
    {
        public int Count { get; set; }
        public List<Bairro> Lista { get; set; } = new List<Bairro>();
    }
}
