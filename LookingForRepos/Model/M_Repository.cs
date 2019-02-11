using System;

namespace LookingForRepos.Model
{
    public class M_Repository
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Login { get; set; }

        public string Ava { get; set; }

        public string Reference { get; set; }

        public string Language { get; set; }

        public int StarsCount { get; set; }

        public int ForkCount { get; set; }

        public DateTime DateLastUpdate { get; set; }
    }
}
