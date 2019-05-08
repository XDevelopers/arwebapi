using AltaRail.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltaRail.Application.Services
{
    public interface ITokenService
    {
        string CreateToken(CreateTokenDto createToken);
    }
}
