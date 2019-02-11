using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Octokit;

namespace LookingForRepos.Model
{
    /// <summary>
    /// загружает данные с сайта гитхаб в соответствии со строкой поиска
    /// </summary>
    public class RepositoriesFromNet
    {
        //private const string MY_CONNECT_TOKEN = "26da4f109050a2389f48cbe0f9bfe4143c97ea63";

        private readonly string m_SearchVal;
        public List<M_Repository> RepositoriesList { get; } = new List<M_Repository>();

        public RepositoriesFromNet(string mSearchVal)
        {
            m_SearchVal = mSearchVal;
        }

        public bool Load()
        {
            if (string.IsNullOrEmpty(m_SearchVal))
            {
                return false;
            }

            #region TrueGetFromNet

            var client = new GitHubClient(new ProductHeaderValue(AppDomain.CurrentDomain.FriendlyName));
            var token = ConfigurationManager.AppSettings.Get("Token");
            var basicAuth = new Credentials(token);
            client.Credentials = basicAuth;

            var request = new SearchRepositoriesRequest(m_SearchVal)
            {
                SortField = RepoSearchSort.Updated,
                Order = SortDirection.Descending
            };

            var rez = client.Search.SearchRepo(request);
            var repos = rez.Result.Items.Take(10);
            foreach (var repository in repos)
            {
                RepositoriesList.Add(new M_Repository
                {
                    Name = repository.Name,
                    Description = repository.Description,
                    Login = repository.Owner.Login,
                    Ava = repository.Owner.AvatarUrl,
                    Reference = repository.HtmlUrl,
                    Language = repository.Language,
                    StarsCount = repository.StargazersCount,
                    ForkCount = repository.ForksCount,
                    DateLastUpdate = repository.UpdatedAt.Date
                });
            }

            #endregion



            #region Test Load

            //var avaList = new List<string>
            //{
            //    "",
            //    "https://99px.ru/sstorage/1/2019/02/image_10102191045197441160.gif",
            //    "https://99px.ru/sstorage/41/2010/10/image_412510102151256856342.jpg",
            //    "https://99px.ru/sstorage/41/2014/12/image_411912142150485111380.jpg",
            //    "https://99px.ru/sstorage/41/2018/11/image_411011180230132303834.png"
            //};

            //var rand = new Random();
            //for (int i = 0; i < 10; i++)
            //{
            //    RepositoriesList.Add(new M_Repository
            //    {
            //        Login = "Login" + i,
            //        Ava = avaList[rand.Next(0, 4)],
            //        Name = "Name" + i,
            //        Description = "突然Desc" + i,
            //        DateLastUpdate = DateTime.Now.AddDays(-i),
            //        ForkCount = i,
            //        StarsCount = i,
            //        Language = "C#",
            //        Reference = avaList[rand.Next(0, 4)]
            //    });
            //}

            #endregion

            return true;
        }
    }
}
