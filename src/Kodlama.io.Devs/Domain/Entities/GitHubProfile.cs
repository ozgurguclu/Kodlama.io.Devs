using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GitHubProfile : Entity
    {
        public int UserId { get; set; }
        public string GitHubAddress { get; set; }
        public virtual User? User { get; set; }

        public GitHubProfile()
        {

        }

        public GitHubProfile(int id, int userId, string gitHubAddress) : this()
        {
            Id = id;
            UserId = userId;
            GitHubAddress = gitHubAddress;
        }
    }
}
