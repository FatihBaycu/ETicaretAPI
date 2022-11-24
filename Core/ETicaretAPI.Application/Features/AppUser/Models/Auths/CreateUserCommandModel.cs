using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.AppUser.Models.Auths
{
    public class CreateUserCommandModel
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }

    }
}
