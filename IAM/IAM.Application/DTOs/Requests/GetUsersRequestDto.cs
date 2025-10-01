using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Application.DTOs.Requests
{
    public class GetUsersRequestDto
    {
        public string? Role { get; set; }
        public string? FIO { get; set; }

    }
}
