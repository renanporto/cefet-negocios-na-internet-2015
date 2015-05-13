using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyStore.WebApp.ViewModels
{
    public class ContactModel
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome é obrigatório")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O campo Tipo de solicitação é obrigatório")]
        public string RequestType { get; set; }

        [Required(ErrorMessage = "O campo Mensagem é obrigatório")]
        public string Message { get; set; }
    }
}