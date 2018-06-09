using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoFixture;
using AutoFixture.Xunit2;
using MyProject.Business.Identity;
using MyProject.Core.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shouldly;
using Xunit;

namespace MyProject.Business.Tests.Identity
{
    public class JwtFactoryTests
    {
        private readonly JwtFactory _jwtFactory;
        private readonly JwtConfiguration _jwtConfiguration;

        public JwtFactoryTests()
        {
            var fixture = new Fixture();

            var signingKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(fixture.Create<string>()));

            _jwtConfiguration = fixture
                .Build<JwtConfiguration>()
                .Without(config => config.IssuedAt)
                .Without(config => config.NotBefore)
                .With(config => config.SigningCredentials, new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256))
                .Create();

            var configuration = Options.Create(_jwtConfiguration);

            _jwtFactory = new JwtFactory(configuration);
        }

        [Theory]
        [AutoData]
        public void GenerateEncodedToken_Should_Generate_Proper_Token(string userId, string email)
        {
            // Arrange
            // Act
            var result = _jwtFactory.GenerateEncodedToken(userId, email);

            // Assert
            var jwt = new JwtSecurityToken(result);

            jwt.Claims.ShouldContain(c => c.Type == JwtRegisteredClaimNames.Iss && c.Value == _jwtConfiguration.Issuer);
            jwt.Claims.ShouldContain(c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == userId);
            jwt.Claims.ShouldContain(c => c.Type == JwtRegisteredClaimNames.Email && c.Value == email);
            jwt.Claims.ShouldContain(c => c.Type == JwtRegisteredClaimNames.Aud && c.Value == _jwtConfiguration.Audience);

            jwt.ValidFrom.ShouldBe(_jwtConfiguration.NotBefore, TimeSpan.FromSeconds(10));
            jwt.ValidTo.ShouldBe(_jwtConfiguration.Expiration, TimeSpan.FromSeconds(10));
        }
    }
}
