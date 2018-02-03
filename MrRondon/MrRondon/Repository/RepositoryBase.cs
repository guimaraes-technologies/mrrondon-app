using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MrRondon.Repository
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        protected readonly SQLite.Net.SQLiteConnection Conexao;

        public RepositoryBase()
        {
            var config = DependencyService.Get<Interfaces.IDataStore>();
            Conexao = new SQLite.Net.SQLiteConnection(config.Platform, System.IO.Path.Combine(config.Path, "MrRondon.db3"));

            Conexao.CreateTable<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            Conexao.Insert(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Conexao.Delete(entity);
        }

        public virtual void DeleteAll()
        {
            Conexao.DeleteAll<TEntity>();
        }

        public virtual void Update(TEntity entity)
        {
            Conexao.Update(entity);
        }

        public void Clean()
        {
            Conexao.DeleteAll<TEntity>();
        }

        public virtual List<TEntity> GetAll()
        {
            return Conexao.Table<TEntity>().ToList();
        }

        public void Dispose()
        {
            Conexao.Dispose();
        }
    }
}