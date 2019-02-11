using System.Data.Entity;

namespace LookingForRepos.Model
{
    public partial class githubdbEntities : DbContext
    {
        public githubdbEntities(string connectionString) : base(connectionString)
        {
            
        }
    }
}
