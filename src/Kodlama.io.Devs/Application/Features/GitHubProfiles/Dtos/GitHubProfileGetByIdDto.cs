using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Dtos
{
    public class GitHubProfileGetByIdDto
    {
        public int Id { get; set; }
        public string GitHubAddress { get; set; }
    }
}
