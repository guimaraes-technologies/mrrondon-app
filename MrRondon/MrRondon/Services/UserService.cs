using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Behaviors;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Pages.Account;
using MrRondon.Services.Rest;
using MrRondon.ViewModels;

namespace MrRondon.Services
{
    public class UserService
    {
        public async Task<bool> Authenticate(LoginPageModel login)
        {
            ValidateLogin(login);
            var tokenService = new TokenRest();
            var token = await tokenService.Login(login);
            if (token == null) throw new Exception("Usuário ou Senha incorreta");

            var userService = new UserRest();
            var userVm = await userService.GetInformationAsync(token.AccessToken);

            if (userVm == null) throw new Exception("Usuário ou Senha incorreta");

            var userToken = new UserTokenVm
            {
                Token = token,
                User = userVm
            };
            Login(userToken);
            return true;
        }

        public async Task<User> GetInformationAsync()
        {
            var service = new UserRest();
            var user = await service.GetInformationAsync(Account.Current.Token.AccessToken);

            return user;
        }

        public async Task<IList<Event>> GetFavoriteEventsAsync()
        {
            var service = new UserRest();
            var items = await service.GetFavoriteEventsAsync();

            return items.OrderBy(o => o.Name).ToList();
        }

        public async Task<User> Register(RegisterPageModel register)
        {
            if (string.IsNullOrWhiteSpace(register.FirstName)) throw new Exception("Campo Nome é obrigatório");
            if (string.IsNullOrWhiteSpace(register.LastName)) throw new Exception("Campo Sobrenome é obrigatório");
            if (string.IsNullOrWhiteSpace(register.Telephone) || string.IsNullOrWhiteSpace(register.Telephone)) throw new Exception("Informe pelo menos um número de contato");

            if (!PhoneNumberValidatorBehavior.IsValidPhoneNumber(register.Telephone))
                throw new Exception("Campo Telefone é inválido");
            if (!PhoneNumberValidatorBehavior.IsValidPhoneNumber(register.CellPhone))
                throw new Exception("Campo Celular é inválido");

            if (string.IsNullOrWhiteSpace(register.Cpf)) throw new Exception("Campo CPF é obrigatório");
            if (!CpfValidatorBehavior.IsValidCpf(register.Cpf)) throw new Exception("Campo CPF é inválido");
            if (string.IsNullOrWhiteSpace(register.Email)) throw new Exception("Campo Email é obrigatório");
            if (!EmailHelper.IsEmail(register.Email)) throw new Exception("Email inválido");
            if (string.IsNullOrWhiteSpace(register.Password)) throw new Exception("Campo Senha é obrigatório");
            if (string.IsNullOrWhiteSpace(register.ConfirmPassword)) throw new Exception("Campo Confirmação de Senha é obrigatório");
            if (!string.Equals(register.Password, register.ConfirmPassword)) throw new Exception("A senha e confirmação não confere");

            var userRest = new UserRest();
            var user = await userRest.Register(register);
            var tokenRest = new TokenRest();
            var token = await tokenRest.Login(new LoginPageModel { UserName = user.Cpf, Password = register.Password });
            var userToken = new UserTokenVm
            {
                User = user,
                Token = token
            };
            Login(userToken);
            return userToken.User;
        }

        public void ValidateLogin(LoginPageModel login)
        {
            if (string.IsNullOrWhiteSpace(login.UserName)) throw new Exception("- O Login é obrigatório");
            if (string.IsNullOrWhiteSpace(login.Password)) throw new Exception("- A Senha é obrigatória");
        }

        public void Login(UserTokenVm userToken)
        {
            Logout();
            ApplicationManager<UserTokenVm>.AddOrUpdate("userToken", userToken);
            //_repo.Insert(user);
        }

        public void Logout()
        {
            ApplicationManager<UserTokenVm>.Remove("userToken");
            //_repo.DeleteAll();
        }
    }
}