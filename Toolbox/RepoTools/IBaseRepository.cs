
namespace Toolbox.RepoTools
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        IEnumerable<TEntity> GetAll(string tableName = "");
    }
}