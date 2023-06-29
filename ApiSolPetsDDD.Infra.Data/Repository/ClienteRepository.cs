using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using APISolPets.Domain.Models;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;
        private readonly IContatoRepository contatoRepository;

        public ClienteRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory, IContatoRepository contatoRepository)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
            this.contatoRepository = contatoRepository;
        }

        public async Task<Cliente> GetInfoCliente(int idCliente)
        {
            var result = new Cliente();
            try
            {
                var cliente = await GetCliente(idCliente);
                if (cliente.IdCliente != 0)
                {
                    result.IdCliente = cliente.IdCliente;
                    result.NomeCliente = cliente.NomeCliente;
                    result.NomeEmpresaCliente = cliente.NomeEmpresaCliente;
                    result.RgCliente = cliente.RgCliente;
                    result.CpfCliente = cliente.CpfCliente;
                    result.CnpjCliente = cliente.CnpjCliente;
                    result.DataNascimentoCliente = cliente.DataNascimentoCliente;
                    result.SexoCliente = cliente.SexoCliente;
                    result.DHUltimaAtualizacao = cliente.DHUltimaAtualizacao;

                    var petsCliente = await GetPetsCliente(cliente.IdCliente);
                    if (petsCliente.Count > 0)
                    {
                        foreach (var pet in petsCliente)
                        {
                            result.PetsCliente = new List<Pet>
                            {
                                pet
                            };
                        }
                    }

                    var contatosCliente = await contatoRepository.GetContatos(cliente.IdCliente, 0);
                    if (contatosCliente.Count > 0)
                    {
                        foreach (var contato in contatosCliente)
                        {
                            result.ContatosCliente = new List<Contato>
                            {
                                contato
                            };
                        }
                    }

                    var enderecosCliente = await GetEnderecosCliente(cliente.IdCliente);
                    if (enderecosCliente.Count > 0)
                    {
                        foreach (var endereco in enderecosCliente)
                        {
                            result.EnderecosCliente = new List<Endereco>()
                            {
                                endereco
                            };
                        }
                    }

                    return result;
                }
                else
                {
                    throw new ExcecaoNegocio("[Exceção de negócio] - idCliente inválido!");
                }

            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Pet>> GetPetNome(string nomePet)
        {
            var result = new List<Pet>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    string query = @"SELECT PET.* FROM PET_CLIENTE PET WITH (NOLOCK)
                                     WHERE PET.NOME_PET LIKE @NOME_PET";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@NOME_PET", nomePet);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var petCliente = new Pet();
                            petCliente.IdCliente = reader["ID_CLIENTE"] != DBNull.Value ? Convert.ToInt32(reader["ID_CLIENTE"]) : 0; ;
                            petCliente.IdPet = reader["ID_PET"] != DBNull.Value ? Convert.ToInt32(reader["ID_PET"]) : 0;
                            petCliente.TipoAnimalPet = reader["TIPO_ANIMAL_PET"].ToString() ?? null;
                            petCliente.RacaPet = reader["RACA_PET"].ToString() ?? null;
                            petCliente.NomePet = reader["NOME_PET"].ToString() ?? null;
                            petCliente.DataNascimentoPet = reader["DATA_NASCIMENTO_PET"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_NASCIMENTO_PET"]);
                            petCliente.DHUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                            result.Add(petCliente);
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Cliente>> GetClienteNome(string nomeCliente)
        {
            var result = new List<Cliente>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CLIENTE_BY_NOME");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NOME_CLIENTE", nomeCliente);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var client = new Cliente()
                            {
                                IdCliente = Convert.ToInt32(reader["ID_CLIENTE"]),
                                NomeCliente = reader["NOME_CLIENTE"].ToString() ?? null,
                                NomeEmpresaCliente = reader["NOME_EMPRESA_CLIENTE"].ToString() ?? null,
                                SexoCliente = reader["SEXO_CLIENTE"] == DBNull.Value ? '\0' : Convert.ToChar(reader["SEXO_CLIENTE"]),
                                RgCliente = reader["RG_CLIENTE"].ToString() ?? null,
                                CpfCliente = reader["CPF_CLIENTE"].ToString() ?? null,
                                CnpjCliente = reader["CNPJ_CLIENTE"].ToString() ?? null,
                                UfRg = reader["UF_RG"].ToString() ?? null,
                                DataNascimentoCliente = reader["DATA_NASCIMENTO_CLIENTE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_NASCIMENTO_CLIENTE"]),
                                DHUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"])
                            };
                            var enderecosCliente = await GetEnderecosCliente(client.IdCliente);
                            var contatosCliente = await contatoRepository.GetContatos(client.IdCliente, 0);
                            var petsCliente = await GetPetsCliente(client.IdCliente);
                            client.EnderecosCliente = enderecosCliente;
                            client.ContatosCliente = contatosCliente;
                            client.PetsCliente = petsCliente;

                            result.Add(client);
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Cliente>> GetClienteCpf(string cpf)
        {
            var result = new List<Cliente>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CLIENTE_BY_CPF");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CPF_CLIENTE", cpf);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var client = new Cliente()
                            {
                                IdCliente = Convert.ToInt32(reader["ID_CLIENTE"]),
                                NomeCliente = reader["NOME_CLIENTE"].ToString() ?? null,
                                NomeEmpresaCliente = reader["NOME_EMPRESA_CLIENTE"].ToString() ?? null,
                                SexoCliente = reader["SEXO_CLIENTE"] == DBNull.Value ? '\0' : Convert.ToChar(reader["SEXO_CLIENTE"]),
                                RgCliente = reader["RG_CLIENTE"].ToString() ?? null,
                                CpfCliente = reader["CPF_CLIENTE"].ToString() ?? null,
                                CnpjCliente = reader["CNPJ_CLIENTE"].ToString() ?? null,
                                UfRg = reader["UF_RG"].ToString() ?? null,
                                DataNascimentoCliente = reader["DATA_NASCIMENTO_CLIENTE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_NASCIMENTO_CLIENTE"]),
                                DHUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"])
                            };
                            var enderecosCliente = await GetEnderecosCliente(client.IdCliente);
                            var contatosCliente = await contatoRepository.GetContatos(client.IdCliente, 0);
                            var petsCliente = await GetPetsCliente(client.IdCliente);
                            client.EnderecosCliente = enderecosCliente;
                            client.ContatosCliente = contatosCliente;
                            client.PetsCliente = petsCliente;

                            result.Add(client);
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<Cliente> GetCliente(int idCliente)
        {
            var result = new Cliente();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CLIENTE_BY_ID");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdCliente = Convert.ToInt32(reader["ID_CLIENTE"]);
                        result.NomeCliente = reader["NOME_CLIENTE"].ToString() ?? null;
                        result.NomeEmpresaCliente = reader["NOME_EMPRESA_CLIENTE"].ToString() ?? null;
                        result.SexoCliente = reader["SEXO_CLIENTE"] == DBNull.Value ? '\0' : Convert.ToChar(reader["SEXO_CLIENTE"]);
                        result.RgCliente = reader["RG_CLIENTE"].ToString() ?? null;
                        result.CpfCliente = reader["CPF_CLIENTE"].ToString() ?? null;
                        result.CnpjCliente = reader["CNPJ_CLIENTE"].ToString() ?? null;
                        result.UfRg = reader["UF_RG"].ToString() ?? null;
                        result.DataNascimentoCliente = reader["DATA_NASCIMENTO_CLIENTE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_NASCIMENTO_CLIENTE"]);
                        result.DHUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);

                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<Pet>> GetPetsCliente(int idCliente)
        {
            var result = new List<Pet>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_PET_CLIENTE_BY_ID_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var petCliente = new Pet();
                            petCliente.IdCliente = Convert.ToInt32(reader["codigoCliente"]);
                            petCliente.IdPet = Convert.ToInt32(reader["codigoPet"]);
                            petCliente.TipoAnimalPet = reader["tipoAnimal"].ToString() ?? null;
                            petCliente.RacaPet = reader["raca"].ToString() ?? null;
                            petCliente.NomePet = reader["nome"].ToString() ?? null;
                            petCliente.DataNascimentoPet = reader["dhNascimento"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["dhNascimento"]);
                            petCliente.DHUltimaAtualizacao = reader["dhUltimaAtualizacao"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["dhUltimaAtualizacao"]);

                            result.Add(petCliente);
                        }

                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<Endereco>> GetEnderecosCliente(int idCliente)
        {
            var result = new List<Endereco>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_ENDERECO_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var endCliente = new Endereco()
                            {
                                IdCliente = Convert.ToInt32(reader["codigoCliente"]),
                                IdEndreco = Convert.ToInt32(reader["codigoEndereco"]),
                                Logradouro = reader["rua"].ToString() ?? null,
                                Numero = reader["numero"] == DBNull.Value ? 0 : Convert.ToInt32(reader["numero"]),
                                Complemento = reader["complemento"].ToString() ?? null,
                                Bairro = reader["bairro"].ToString() ?? null,
                                Cep = reader["cep"].ToString() ?? null,
                                UfEstado = reader["uf"].ToString() ?? null,
                                Cidade = reader["cidade"].ToString() ?? null,
                                DHUltimaAtualizacao = reader["dhUltimaAtualizacao"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["dhUltimaAtualizacao"])

                            };
                            result.Add(endCliente);
                        }

                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Cliente>> GetInfoClientes(int limit)
        {
            if (limit == 0) limit = 10;
            var result = new List<Cliente>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CLIENTES");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LIMIT", limit);

                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var idCliente = Convert.ToInt32(reader["ID_CLIENTE"]);
                            var pets = await GetPetsCliente(idCliente);
                            var contatos = await contatoRepository.GetContatos(idCliente, 0);
                            var enderecos = await GetEnderecosCliente(idCliente);
                            var cliente = new Cliente()
                            {
                                IdCliente = idCliente,
                                NomeCliente = reader["NOME_CLIENTE"].ToString() ?? null,
                                NomeEmpresaCliente = reader["NOME_EMPRESA_CLIENTE"].ToString() ?? null,
                                SexoCliente = reader["SEXO_CLIENTE"] == DBNull.Value ? '\0' : Convert.ToChar(reader["SEXO_CLIENTE"]),
                                RgCliente = reader["RG_CLIENTE"].ToString() ?? null,
                                UfRg = reader["UF_RG"].ToString() ?? null,
                                CpfCliente = reader["CPF_CLIENTE"].ToString() ?? null,
                                CnpjCliente = reader["CNPJ_CLIENTE"].ToString() ?? null,
                                DataNascimentoCliente = reader["DATA_NASCIMENTO_CLIENTE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_NASCIMENTO_CLIENTE"]),
                                DHUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]),
                                EnderecosCliente = enderecos,
                                PetsCliente = pets,
                                ContatosCliente = contatos
                            };
                            result.Add(cliente);
                        }

                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente> CadastrarCliente(Cliente cliente)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@DATA_NASCIMENTO_CLIENTE", cliente.DataNascimentoCliente);
                    command.Parameters.AddWithValue("@SEXO_CLIENTE", cliente.SexoCliente);
                    if (!string.IsNullOrEmpty(cliente.NomeCliente))
                        command.Parameters.AddWithValue("@NOME_CLIENTE", cliente.NomeCliente);
                    if (!string.IsNullOrEmpty(cliente.NomeEmpresaCliente))
                        command.Parameters.AddWithValue("@NOME_EMPRESA_CLIENTE", cliente.NomeEmpresaCliente);
                    if (!string.IsNullOrEmpty(cliente.RgCliente))
                        command.Parameters.AddWithValue("@RG_CLIENTE", cliente.RgCliente);
                    if (!string.IsNullOrEmpty(cliente.UfRg))
                        command.Parameters.AddWithValue("@UF_RG", cliente.UfRg);
                    if (!string.IsNullOrEmpty(cliente.CpfCliente))
                        command.Parameters.AddWithValue("@CPF_CLIENTE", cliente.CpfCliente);
                    if (!string.IsNullOrEmpty(cliente.CnpjCliente))
                        command.Parameters.AddWithValue("@CNPJ_CLIENTE", cliente.CnpjCliente);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovoCliente = Convert.ToInt32(await command.ExecuteScalarAsync());

                    if (cliente.ContatosCliente?.Count > 0)
                    {
                        foreach (var contato in cliente.ContatosCliente)
                        {
                            contato.IdCliente = idNovoCliente;
                            await contatoRepository.CadastrarContato(contato);
                        }
                    }
                    if (cliente.EnderecosCliente?.Count > 0)
                    {
                        foreach (var endereco in cliente.EnderecosCliente)
                        {
                            await CadastrarEnderecoCliente(endereco, idNovoCliente);
                        }
                    }
                    if (cliente.PetsCliente?.Count > 0)
                    {
                        foreach (var pet in cliente.PetsCliente)
                        {
                            await CadastrarPetCliente(pet, idNovoCliente);
                        }
                    }
                    var resultCliente = await GetInfoCliente(idNovoCliente);

                    return resultCliente;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Endereco> CadastrarEnderecoCliente(Endereco endereco, int idCliente)
        {
            var result = new Endereco();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_ENDERECO_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    if (idCliente.ToString() == null || idCliente.ToString() == string.Empty)
                        throw new ExcecaoNegocio("[Exceção de negócio] - É obrigatório informar o id do cliente");
                    if (idCliente != 0)
                        command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    if (endereco.Logradouro != string.Empty)
                        command.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro.ToUpper());
                    if (endereco.Numero.ToString() != null || endereco.Numero.ToString() != string.Empty)
                        command.Parameters.AddWithValue("@NUMERO", endereco.Numero);
                    if (endereco.Complemento != string.Empty)
                        command.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento.ToUpper());
                    if (endereco.Bairro != string.Empty)
                        command.Parameters.AddWithValue("@BAIRRO", endereco.Bairro.ToUpper());
                    if (endereco.Cep != string.Empty)
                        command.Parameters.AddWithValue("@CEP", endereco.Cep.Trim());
                    if (endereco.UfEstado != string.Empty)
                        command.Parameters.AddWithValue("@UF_ESTADO", endereco.UfEstado.ToUpper());
                    if (endereco.Cidade != string.Empty)
                        command.Parameters.AddWithValue("@CIDADE", endereco.Cidade.ToUpper());

                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idEndereco = await command.ExecuteScalarAsync();

                    var enderecos = await GetEnderecosCliente(idCliente);

                    foreach (var end in enderecos)
                    {
                        if (end.IdEndreco == idEndereco)
                            result = end;
                    }

                    return result;
                }
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pet> CadastrarPetCliente(Pet pet, int idCliente)
        {
            var result = new Pet();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_PET_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    var dhNascPet = pet.DataNascimentoPet.ToString("yyyy-MM-dd HH:mm:ss");

                    if (idCliente.ToString() == null || idCliente.ToString() == string.Empty)
                        throw new ExcecaoNegocio("[Exceção de negócio] - É obrigatório informar o id do cliente!");
                    if (idCliente.ToString() != null || idCliente.ToString() != string.Empty)
                        command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    if (pet.TipoAnimalPet != string.Empty)
                        command.Parameters.AddWithValue("@TIPO_ANIMAL_PET", pet.TipoAnimalPet.ToUpper());
                    if (pet.RacaPet != string.Empty)
                        command.Parameters.AddWithValue("@RACA_PET", pet.RacaPet.ToUpper());
                    if (pet.NomePet != string.Empty)
                        command.Parameters.AddWithValue("@NOME_PET", pet.NomePet.ToUpper());
                    if (pet.DataNascimentoPet.ToString() != string.Empty)
                        command.Parameters.AddWithValue("@DATA_NASCIMENTO_PET", DateTime.Parse(dhNascPet));

                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int idPet = Convert.ToInt32(await command.ExecuteScalarAsync());

                    var pets = await GetPetsCliente(idCliente);

                    foreach (var petCliente in pets)
                    {
                        if (petCliente.IdPet == idPet)
                            result = petCliente;
                    }

                    return result;
                }
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizaCliente(Cliente cliente)
        {
            int contatosAtualizados = 0;
            int enderecosAtualizados = 0;
            int petsAtualizados = 0;
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_CADASTRO_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@ID_CLIENTE", cliente.IdCliente);
                    if (!string.IsNullOrEmpty(cliente.NomeCliente))
                        command.Parameters.AddWithValue("@NOME_CLIENTE", cliente.NomeCliente);
                    if (!string.IsNullOrEmpty(cliente.NomeEmpresaCliente))
                        command.Parameters.AddWithValue("@NOME_EMPRESA_CLIENTE", cliente.NomeEmpresaCliente);
                    command.Parameters.AddWithValue("@DATA_NASCIMENTO_CLIENTE", cliente.DataNascimentoCliente);

                    if (!string.IsNullOrEmpty(cliente.RgCliente))
                        command.Parameters.AddWithValue("@RG_CLIENTE", cliente.RgCliente);

                    if (!string.IsNullOrEmpty(cliente.UfRg))
                        command.Parameters.AddWithValue("@UF_RG", cliente.UfRg);

                    command.Parameters.AddWithValue("@SEXO_CLIENTE", cliente.SexoCliente);

                    if (!string.IsNullOrEmpty(cliente.CpfCliente))
                        command.Parameters.AddWithValue("@CPF_CLIENTE", cliente.CpfCliente);

                    if (!string.IsNullOrEmpty(cliente.CnpjCliente))
                        command.Parameters.AddWithValue("@CNPJ_CLIENTE", cliente.CnpjCliente);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (cliente.ContatosCliente.Count > 0)
                    {
                        foreach (var contato in cliente.ContatosCliente)
                        {
                            contato.IdCliente = cliente.IdCliente;
                            await contatoRepository.AtualizaContato(contato);
                            contatosAtualizados++;
                        }
                    }
                    if (cliente.EnderecosCliente.Count > 0)
                    {
                        foreach (var endereco in cliente.EnderecosCliente)
                        {
                            endereco.IdCliente = cliente.IdCliente;
                            await AtualizaEnderecoCliente(endereco);
                            enderecosAtualizados++;
                        }
                    }
                    if (cliente.PetsCliente.Count > 0)
                    {
                        foreach (var pet in cliente.PetsCliente)
                        {
                            pet.IdCliente = cliente.IdCliente;
                            await AtualizaPetCliente(pet);
                            petsAtualizados++;
                        }
                    }

                    return (linhasAfetadas + enderecosAtualizados + contatosAtualizados + petsAtualizados);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizaEnderecoCliente(Endereco enderCliente)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_ENDERECO_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    if (enderCliente.IdCliente == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do cliente!");

                    if (enderCliente.IdCliente > 0)
                        command.Parameters.AddWithValue("@ID_CLIENTE", enderCliente.IdCliente);

                    if (enderCliente.IdEndreco == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do endereço!");

                    if (enderCliente.IdEndreco > 0)
                        command.Parameters.AddWithValue("@ID_ENDERECO", enderCliente.IdEndreco);

                    if (enderCliente.Logradouro != string.Empty)
                        command.Parameters.AddWithValue("@LOGRADOURO", enderCliente.Logradouro.ToUpper());

                    if (enderCliente.Numero.ToString() != string.Empty)
                        command.Parameters.AddWithValue("@NUMERO", enderCliente.Numero);

                    if (enderCliente.UfEstado != string.Empty)
                        command.Parameters.AddWithValue("@UF_ESTADO", enderCliente.UfEstado.ToUpper());

                    if (enderCliente.Complemento != string.Empty)
                        command.Parameters.AddWithValue("@COMPLEMENTO", enderCliente.Complemento.ToUpper());

                    if (enderCliente.Cidade != string.Empty)
                        command.Parameters.AddWithValue("@CIDADE", enderCliente.Cidade.ToUpper());

                    if (enderCliente.Bairro != string.Empty)
                        command.Parameters.AddWithValue("@BAIRRO", enderCliente.Bairro.ToUpper());

                    if (enderCliente.Cep != string.Empty)
                        command.Parameters.AddWithValue("@CEP", enderCliente.Cep);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    return linhasAfetadas;
                }
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizaPetCliente(Pet petCliente)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_PET_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    if (petCliente.IdCliente == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do cliente!");

                    if (petCliente.IdCliente > 0)
                        command.Parameters.AddWithValue("@ID_CLIENTE", petCliente.IdCliente);

                    if (petCliente.IdPet == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do pet!");

                    if (petCliente.IdPet > 0)
                        command.Parameters.AddWithValue("@ID_PET", petCliente.IdPet);

                    if (petCliente.NomePet != string.Empty)
                        command.Parameters.AddWithValue("@NOME_PET", petCliente.NomePet.ToUpper());

                    if (petCliente.RacaPet != string.Empty)
                        command.Parameters.AddWithValue("@RACA_PET", petCliente.RacaPet.ToUpper());

                    if (petCliente.TipoAnimalPet != string.Empty)
                        command.Parameters.AddWithValue("@TIPO_ANIMAL_PET", petCliente.TipoAnimalPet.ToUpper());

                    if (petCliente.DataNascimentoPet.ToString() != string.Empty)
                    {
                        var dhNascimentoPet = petCliente.DataNascimentoPet.ToString("yyyy-MM-dd HH:mm:ss");
                        command.Parameters.AddWithValue("@DATA_NASCIMENTO_PET", DateTime.Parse(dhNascimentoPet));
                    }

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    return linhasAfetadas;
                }
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ExcluirCliente(int idCliente)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EXCLUIR_INFO_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    return linhasAfetadas;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ExcluirEndeCliente(int idCliente, int idEndereco)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EXCLUIR_ENDERECO_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    if (idCliente == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do cliente!");

                    if (idCliente > 0)
                        command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);

                    if (idEndereco == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do endereço!");

                    if (idEndereco > 0)
                        command.Parameters.AddWithValue("@ID_ENDERECO", idEndereco);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    return linhasAfetadas;
                }
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ExcluirPetCliente(int idCliente, int idPet)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EXCLUIR_PET_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    if (idCliente == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do cliente!");

                    if (idCliente > 0)
                        command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);

                    if (idPet == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do pet!");

                    if (idPet > 0)
                        command.Parameters.AddWithValue("@ID_PET", idPet);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    return linhasAfetadas;
                }
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
