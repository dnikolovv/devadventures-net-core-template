using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyProject.Business.Extensions;
using MyProject.Core;
using MyProject.Core.Identity;
using MyProject.Core.Models;
using MyProject.Core.Services;
using MyProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Optional;

namespace MyProject.Business.Services
{
    public class UsersService : IUsersService
    {
        public UsersService(
            UserManager<User> userManager,
            IJwtFactory jwtFactory,
            IMapper mapper)
        {
            UserManager = userManager;
            JwtFactory = jwtFactory;
            Mapper = mapper;
        }

        protected UserManager<User> UserManager { get; }
        protected IJwtFactory JwtFactory { get; }
        protected IMapper Mapper { get; }

        public async Task<Option<JwtModel, Error>> Login(LoginUserModel model)
        {
            var loginResult = await (await UserManager.FindByEmailAsync(model.Email))
                .SomeNotNull()
                .FilterAsync(async user => await UserManager.CheckPasswordAsync(user, model.Password));

            return loginResult.Match(
                user =>
                {
                    return new JwtModel
                    {
                        TokenString = JwtFactory.GenerateEncodedToken(user.Id, user.Email)
                    }.Some<JwtModel, Error>();
                },
                () => Option.None<JwtModel, Error>(new Error("Invalid credentials.")));
        }

        public async Task<Option<UserModel, Error>> Register(RegisterUserModel model)
        {
            var user = Mapper.Map<User>(model);

            var creationResult = await UserManager.CreateAsync(user, model.Password);

            return creationResult.Succeeded ?
                Mapper.Map<UserModel>(user).Some<UserModel, Error>() :
                Option.None<UserModel, Error>(new Error(creationResult.Errors.Select(e => e.Description)));
        }
    }
}
