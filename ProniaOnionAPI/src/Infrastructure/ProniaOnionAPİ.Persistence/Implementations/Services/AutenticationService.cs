using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.Tokens;
using ProniaOnionAPİ.Application.DTOs.User;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Persistence.Implementations.Services
{
    public class AutenticationService : IAutenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtTokenHandler _handler;

        public AutenticationService(UserManager<AppUser> userManager,IMapper mapper,IJwtTokenHandler handler)
        {
            _userManager = userManager;
            _mapper = mapper;
            _handler = handler;
        }

        

        public async Task Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName || x.Email == registerDto.Email)) throw new Exception("Email or Username have");
            AppUser user= _mapper.Map<AppUser>(registerDto);
            var result=await _userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded) 
            {
                StringBuilder builder= new StringBuilder();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }
                throw new Exception(builder.ToString());
            }
        }
        public async Task<TokenResponseDto> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
                if (user == null) throw new Exception("Email,username or password is wrong");
            }
            if(!await _userManager.CheckPasswordAsync(user, loginDto.Password)) throw new Exception("Email,username or password is wrong");

            return _handler.CreateToken(user, 60);
        }
    }
}
