using System.Collections.Generic;
using System.Linq;

namespace LookingForRepos.Model
{
    /// <summary>
    /// подгрузка списка репозиториев из БД
    /// если они уже были там сохранены(поиск осуществлялся ранее)
    /// </summary>
    public class LoadingRepoFromDB
    {
        public readonly List<M_Repository> RepoList = new List<M_Repository>();

        public void Load()
        {
            using (var dc = new githubdbEntities())
            {
                var query = dc.T_REPO
                    .Select(s => s).ToArray();

                foreach (var mRepository in query)
                {
                    RepoList.Add(new M_Repository
                    {
                        Login = mRepository.T_AUTHORS.LOGIN,
                        Ava = mRepository.T_AUTHORS.AVATAR,
                        DateLastUpdate = mRepository.DATE_UPDATE,
                        ForkCount= mRepository.FORKS_COUNT,
                        Language = mRepository.LANGUAGE,
                        Description = mRepository.REPO_DESC,
                        Name = mRepository.REPO_NAME,
                        Reference = mRepository.REPO_REF,
                        StarsCount = mRepository.STARS_COUNT
                    });
                }
            }
        }
    }
}
