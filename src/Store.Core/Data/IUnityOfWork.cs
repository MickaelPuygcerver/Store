using System.Threading.Tasks;

namespace Store.Core.Data
{
    public interface IUnityOfWork
    {
        Task<bool> Commit();
    }
}
