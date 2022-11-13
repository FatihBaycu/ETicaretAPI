using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.AppUser.Models
{
    public class GoogleLoginCommandResponse
    {
        public Token Token { get; set; }
    }
}
