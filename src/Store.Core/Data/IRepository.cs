using Store.Core.DomainObjects;
using System;

namespace Store.Core.Data
{
    // Somente um repositório por raiz de agregação
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnityOfWork UnitOfWork { get; }
    }
}
