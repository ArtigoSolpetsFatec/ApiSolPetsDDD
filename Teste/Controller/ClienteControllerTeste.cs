using APISolPets.Domain.Models;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.WebAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Extensions;

namespace Teste.Controller
{
    [TestFixture]
    public class ClienteControllerTeste
    {
        private readonly Mock<IClienteService> clienteServiceMock = new();
        private ClienteController subject;

        public ClienteControllerTeste()
        {
            subject = new(clienteServiceMock.Object);
        }

        [Test]
        public async Task GetClienteWithIdClienteShouldReturnOkWithResult()
        {
            //Arrange
            var idCliente = 1;
            var cliente = ObjectsExtensions.ListClienteMock();

            clienteServiceMock.Setup(_ => _.GetCliente(0, idCliente, null))
                .Returns(Task.FromResult(cliente)).Verifiable();

            //Action
            var result = await subject.GetCliente(0, idCliente, null);

            //Assert
            ((ObjectResult)result).StatusCode.Should().Be(200);
            ((ObjectResult)result).Value.Should().Be("");

        }
    }
}
