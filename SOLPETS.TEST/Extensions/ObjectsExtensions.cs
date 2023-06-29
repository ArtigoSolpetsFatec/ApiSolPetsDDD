using APISolPets.Domain.Models;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;

namespace Teste.Extensions
{
    public static class ObjectsExtensions
    {
        public static Cliente ClienteMock()
        {
            return new Cliente()
            {
                IdCliente = 1,
                NomeCliente = "Cliente lindo teste",
                DataNascimentoCliente = new DateTime(1997, 12, 08),
                DHUltimaAtualizacao = new DateTime(2022, 09, 23),
                CnpjCliente = string.Empty,
                ContatosCliente = new List<Contato>(),
                CpfCliente = "12345678912",
                EnderecosCliente = new List<Endereco>(),
                NomeEmpresaCliente = string.Empty,
                PetsCliente = new List<Pet>(),
                RgCliente = "021569874",
                UfRg = "SP",
                SexoCliente = 'M'
            };
        }

        public static List<Cliente> ListClienteMock()
        {
            return new List<Cliente>() { ClienteMock() };
        }
    }
}
