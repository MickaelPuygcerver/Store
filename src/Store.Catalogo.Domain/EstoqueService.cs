using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Catalogo.Domain
{
    class EstoqueService : IEstoqueService
    {
        // Serviços são cross-aggregate
        // São para casos onde a regra não cabe, não é possível faze-lá em um repositório ou uma entidade
        // DEVEMOS usar serviços de domínios para ações específicas da nossa linguaguem oblíqua 

        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            // Nesse momento poderiamos retornar uma Exceptions aqui
            if (produto == null) return false;

            if (produto.PossuiEstoque(quantidade) == false) return false;

            produto.DebitarEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            // Nesse momento poderiamos retornar uma Exceptions aqui
            if (produto == null) return false;

            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }

    }
}
