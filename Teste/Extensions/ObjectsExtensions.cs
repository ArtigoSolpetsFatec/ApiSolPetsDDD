using APISolPets.Domain.Models;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                CnpjCliente = null,
                ContatosCliente = new List<Contato>(),
                CpfCliente = "12345678912",
                EnderecosCliente = new List<Endereco>(),
                NomeEmpresaCliente = null,
                PetsCliente = new List<Pet>(),
                RgCliente = "021569874",
                SexoCliente = "M"
            };
        }

        public static List<Cliente> ListClienteMock()
        {
            return new List<Cliente>() { ClienteMock() };
        }
    }
}
