using IAM.Application.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Application.DTOs.Responses
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public ContactsDTO Contacts { get; set; }
    }
}
