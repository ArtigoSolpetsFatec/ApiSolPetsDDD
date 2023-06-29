using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.WebAPI.Controllers;
using Moq;
using NUnit.Framework;

namespace Teste.Controller
{
    [TestFixture]
    public class ClienteControllerTeste
    {
        private readonly Mock<IClienteService> clienteServiceMock = new();
        private readonly ClienteController clienteController;

        public ClienteControllerTeste()
        {
            clienteController = new(clienteServiceMock.Object);
        }

        //[TestCase(0, 1, null)]
        //[TestCase(0, 0, "Cliente lindo teste")]
        //[TestCase(5, 0, null)]
        //public async Task GetClienteWithCasesShouldReturnOkWithResult(int limit, int idCliente, string nomeCliente)
        //{
        //    //Arrange
        //    var cliente = ObjectsExtensions.ListClienteMock();

        //    clienteServiceMock.Setup(_ => _.GetCliente(limit, idCliente, nomeCliente))
        //        .Returns(Task.FromResult(cliente)).Verifiable();

        //    //Action
        //    var result = await clienteController.GetCliente(limit, idCliente, nomeCliente);

        //    //Assert
        //    ((OkObjectResult)result).StatusCode.Should().Be(200);
        //    ((List<Cliente>)((ObjectResult)result).Value).Count.Should().Be(1);
        //    ((List<Cliente>)((ObjectResult)result).Value)[0].IdCliente.Should().Be(1);
        //    ((List<Cliente>)((ObjectResult)result).Value)[0].NomeCliente.Should().Be("Cliente lindo teste");
        //}

    }
}
