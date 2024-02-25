﻿using Microsoft.AspNetCore.Identity;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using System.Text;

namespace SPG.Application.Login
{
  public class LoginService(SignInManager<IdentityUser> signInManager) : ILoginService
  {
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;

    public async Task<SignInResult> AuthenticateUser(LoginDto loginDto)
    {
      byte[] bytes = Convert.FromBase64String(loginDto.Password);
      string password = Encoding.UTF8.GetString(bytes);
      
      return await _signInManager.PasswordSignInAsync(
        loginDto.Username,
        password,
        isPersistent: false,
        lockoutOnFailure: false);
    }
  }
}