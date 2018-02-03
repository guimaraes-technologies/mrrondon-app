using System;
using System.Text;

namespace MrRondon.Repository
{
    public class UserRepository : RepositoryBase<Entities.User>, IDisposable
    {
        public Entities.User GetLocal()
        {
            return Conexao.Table<Entities.User>().FirstOrDefault();
        }
    }
}