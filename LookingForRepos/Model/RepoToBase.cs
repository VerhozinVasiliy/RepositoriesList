using System.Collections.Generic;
using System.Linq;

namespace LookingForRepos.Model
{
    /// <summary>
    /// сохраняет список репозиториев в БД
    /// </summary>
    public class RepoToBase
    {
        private readonly IEnumerable<M_Repository> m_RepoList;

        public RepoToBase(IEnumerable<M_Repository> mRepoList)
        {
            m_RepoList = mRepoList;
        }

        /// <summary>
        /// общее сохранение
        /// </summary>
        public void Save()
        {
            if (!m_RepoList.Any())
            {
                return;
            }
            using (var dc = new githubdbEntities())
            {
                //почистим таблички для начала
                var queryRepo = dc.T_REPO.ToArray();
                var queryAuthors = dc.T_AUTHORS.ToArray();
                if (queryRepo.Length > 0 || queryAuthors.Length > 0)
                {
                    dc.T_AUTHORS.RemoveRange(queryAuthors);
                    dc.T_REPO.RemoveRange(queryRepo);
                    dc.SaveChanges();
                }

                SaveAuthors();

                var repoListToSave = new List<T_REPO>();
                foreach (var mRepository in m_RepoList)
                {
                    // занесем репозиторий
                    repoListToSave.Add(new T_REPO
                    {
                        AUTHOR_ID = dc.T_AUTHORS.Where(w=>w.LOGIN == mRepository.Login).Select(s=>s.ID_AUTHOR).First(),
                        DATE_UPDATE = mRepository.DateLastUpdate,
                        FORKS_COUNT = mRepository.ForkCount,
                        LANGUAGE = mRepository.Language,
                        REPO_DESC = mRepository.Description,
                        REPO_NAME = mRepository.Name,
                        REPO_REF = mRepository.Reference,
                        STARS_COUNT = mRepository.StarsCount,
                    });
                    
                }
                dc.T_REPO.AddRange(repoListToSave);
                dc.SaveChanges();
            }
        }

        /// <summary>
        /// сохраним авторов в отдельную табличку
        /// </summary>
        private void SaveAuthors()
        {
            using (var dc = new githubdbEntities())
            {
                var authorListToSave = new List<T_AUTHORS>();
                foreach (var mRepository in m_RepoList)
                {
                    // сначала занесем в таблицу автора
                    var queryAddAuthor = dc.T_AUTHORS.FirstOrDefault(w => w.LOGIN == mRepository.Login);
                    if (queryAddAuthor == null)
                    {
                        authorListToSave.Add(new T_AUTHORS
                        {
                            AVATAR = mRepository.Ava,
                            LOGIN = mRepository.Login,
                        });
                    }
                }
                dc.T_AUTHORS.AddRange(authorListToSave);
                dc.SaveChanges();
            }
        }
    }
}
