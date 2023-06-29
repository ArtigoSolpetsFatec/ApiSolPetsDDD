using ApiSolPetsDDD.Application.Services;
using ApiSolPetsDDD.Domain.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Teste.Extensions;

namespace SOLPETS.TEST._2___Application
{
    [TestFixture]
    public class ClienteServiceTeste
    {
        private readonly Mock<IClienteRepository> clienteRepositoryMock = new();
        private readonly ClienteService clienteService;

        public ClienteServiceTeste()
        {
            clienteService = new(clienteRepositoryMock.Object);
        }

        [Test]
        public async Task GetClienteByNomeShouldReturnExpectedValues()
        {
            //Arrange
            var nomeCliente = "Cliente lindo teste";
            var cliente = ObjectsExtensions.ListClienteMock();

            clienteRepositoryMock.Setup(_ => _.GetClienteNome(nomeCliente))
                .Returns(Task.FromResult(cliente)).Verifiable();
            //Action
            var result = await clienteService.GetClienteByNome(nomeCliente);

            //Assert
            result.Count.Should().Be(1);
            result[0].NomeCliente.Should().Be("Cliente lindo teste");
            result[0].IdCliente.Should().Be(1);
            result[0].CpfCliente.Should().Be("12345678912");
            clienteRepositoryMock.VerifyAll();
        }

        [Test]
        public async Task GetClienteByIdShouldReturnExpectedValues()
        {
            //Arrange
            var cliente = ObjectsExtensions.ClienteMock();
            var idCliente = cliente.IdCliente;

            clienteRepositoryMock.Setup(_ => _.GetInfoCliente(idCliente))
                .Returns(Task.FromResult(cliente)).Verifiable();
            //Action
            var result = await clienteService.GetClienteById(idCliente);

            //Assert
            result.Should().Be(1);
            result.NomeCliente.Should().Be("Cliente lindo teste");
            result.IdCliente.Should().Be(1);
            result.CpfCliente.Should().Be("12345678912");
            clienteRepositoryMock.VerifyAll();
        }

        [Test]
        public async Task GetClienteWithLimitShouldReturnExpectedValues()
        {
            //Arrange
            int limit = 2;
            var cliente = ObjectsExtensions.ListClienteMock();

            clienteRepositoryMock.Setup(_ => _.GetInfoClientes(limit))
                .Returns(Task.FromResult(cliente)).Verifiable();
            //Action
            var result = await clienteService.GetClientes(limit);

            //Assert
            result.Count.Should().Be(1);
            result[0].NomeCliente.Should().Be("Cliente lindo teste");
            result[0].IdCliente.Should().Be(1);
            result[0].CpfCliente.Should().Be("12345678912");
            clienteRepositoryMock.VerifyAll();
        }
    }
}
