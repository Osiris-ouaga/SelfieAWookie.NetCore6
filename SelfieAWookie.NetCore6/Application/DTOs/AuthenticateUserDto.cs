﻿namespace SelfieAWookie.NetCore6.Application.DTOs
{
    public class AuthenticateUserDto
    {
        public string Login { get; set; }   

        public string Password { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }
    }
}
