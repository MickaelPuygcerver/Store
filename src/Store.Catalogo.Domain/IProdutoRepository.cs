using Store.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Catalogo.Domain
{
    // Se vamos ter repositório na camada de Domain a interface deve ficar no mesmo projeto, por questão de DI
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);

        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
    }
}
