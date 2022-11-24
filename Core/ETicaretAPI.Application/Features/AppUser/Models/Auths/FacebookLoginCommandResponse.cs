using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.AppUser.Models.Auths
{
    public class FacebookLoginCommandResponse
    {
        public Token Token { get; set; }
    }
}
