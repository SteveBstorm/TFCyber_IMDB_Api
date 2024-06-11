
namespace Toolbox.RepoTools
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        IEnumerable<TEntity> GetAll(string tableName = "");
        int Create(TEntity entity, string tableName = "");

        TEntity GetById(int id, string tableName="");
        void Edit(TEntity entity, string tableName = "");
        void Delete(int id, string tableName = "");
    }
}