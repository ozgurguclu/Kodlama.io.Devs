﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Dtos
{
    public class GitHubProfileListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GitHubAddress { get; set; }
    }
}
