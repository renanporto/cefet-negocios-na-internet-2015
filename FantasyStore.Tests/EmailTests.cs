using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Services;
using NUnit.Framework;

namespace FantasyStore.Tests
{
    public class EmailTests
    {

        [Test]
        public void test_send_email()
        {
            const string body = @"<p>Nome: Renan Porto</p>
                         <p>Email: renanoutroemail@gmail.com</p>
                         <p>Telefone de contato: 24 998110109</p>
                         <p>Tipo de solicitação: Reclamação</p>
                         <p>Mensagem: Jesus! Esse site é horrível! </p>";

            EmailService.Send("Reclamação", body);
        }
    }
}
