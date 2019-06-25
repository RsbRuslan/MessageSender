using LiteDbOrm.Models;

namespace LiteDbOrm.Interfaces
{
    public interface IRepositoryBase<TDto>
    {
        TDomain GetItem(string id);
        string CreateItem(TDomain domainObject);
        void UpdateItem(TDomain domainObject);
    }
}