using ETicaretAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.AppUser.Models
{
    public class RefreshTokenLoginCommandResponse
    {
        public Token Token{ get; set; }
    }

}
