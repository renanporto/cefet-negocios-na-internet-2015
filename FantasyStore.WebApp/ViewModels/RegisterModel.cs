using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyStore.WebApp.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome é obrigatório")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo Data de nascimento é obrigatório")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Digite novamente a senha")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public string Document { get; set; }
    }
}