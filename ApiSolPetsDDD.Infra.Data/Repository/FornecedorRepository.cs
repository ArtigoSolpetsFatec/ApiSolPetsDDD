using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;
        private readonly IContatoRepository contatoRepository;

        public FornecedorRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory,
            IContatoRepository contatoRepository)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
            this.contatoRepository = contatoRepository;
        }

        public async Task<Fornecedor> CadastrarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_FORNECEDOR");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@NOME_FORNECEDOR", fornecedor.NomeFornecedor.ToUpper());
                    command.Parameters.AddWithValue("@CNPJ_FORNECEDOR", fornecedor.CnpjFornecedor);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovoFornecedor = Convert.ToInt32(await command.ExecuteScalarAsync());

                    if (fornecedor.Contatos.Count > 0)
                    {
                        foreach (var contato in fornecedor.Contatos)
                        {
                            contato.IdFornecedor = idNovoFornecedor;
                            await contatoRepository.CadastrarContato(contato);
                        }
                    }

                    var resultFornecedor = await GetFornecedorById(idNovoFornecedor);

                    return resultFornecedor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> GetFornecedorById(int idFornecedor)
        {
            var result = new Fornecedor();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_FORNECEDOR");
                    command.CommandType = CommandType.StoredProcedure;
                    if (idFornecedor == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar id do fornecedor");
                    command.Parameters.AddWithValue("@ID_FORNECEDOR", idFornecedor);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdFornecedor = Convert.ToInt32(reader["ID_FORNECEDOR"]);
                        result.NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null;
                        result.CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null;
                    }

                    result.Contatos = await contatoRepository.GetContatos(0, idFornecedor);

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

        public async Task<List<Fornecedor>> GetFornecedorByCnpj(string cnpjFornecedor)
        {
            var result = new List<Fornecedor>();
            var contatos = new List<Contato>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_FORNECEDOR_BY_CNPJ");
                    command.CommandType = CommandType.StoredProcedure;
                    if (string.IsNullOrEmpty(cnpjFornecedor))
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o CNPJ do fornecedor");
                    command.Parameters.AddWithValue("@CNPJ_FORNECEDOR", cnpjFornecedor);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {

                            var contato = new Contato()
                            {
                                IdFornecedor = reader["ID_FORNECEDOR"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FORNECEDOR"]),
                                IdContato = reader["ID_CONTATO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CONTATO"]),
                                TelefoneCelular = reader["TELEFONE_CELULAR"].ToString() ?? null,
                                OutroCelular = reader["OUTRO_CELULAR"].ToString() ?? null,
                                TelefoneFixo = reader["TELEFONE_FIXO"].ToString() ?? null,
                                EmailPrincipal = reader["EMAIL_PRINCIPAL"].ToString() ?? null,
                                EmailSecundario = reader["EMAIL_SECUNDARIO"].ToString() ?? null,
                                DHUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"])
                            };
                            contatos.Add(contato);
                            var fornecedor = new Fornecedor()
                            {
                                IdFornecedor = reader["ID_FORNECEDOR"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FORNECEDOR"]),
                                NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null,
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                                Contatos = contatos
                            };
                            result.Add(fornecedor);
                        }
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

        public async Task<int> AtualizaFornecedor(Fornecedor fornecedor)
        {
            int contatosAtualizados = 0;
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_FORNECEDOR");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    if (fornecedor.IdFornecedor > 0)
                        command.Parameters.AddWithValue("@ID_FORNECEDOR", fornecedor.IdFornecedor);

                    if (fornecedor.NomeFornecedor == null || fornecedor.NomeFornecedor == string.Empty)
                        throw new ExcecaoNegocio(@"[Exceção de negócio] - Obrigatório informar nome do fornecedor!");

                    if (fornecedor.NomeFornecedor != string.Empty)
                        command.Parameters.AddWithValue("@NOME_FORNECEDOR", fornecedor.NomeFornecedor.ToUpper());

                    if (fornecedor.CnpjFornecedor != string.Empty)
                        command.Parameters.AddWithValue("@CNPJ_FORNECEDOR", fornecedor.CnpjFornecedor.ToUpper());

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = await command.ExecuteNonQueryAsync();

                    if (fornecedor.Contatos.Count > 0)
                    {
                        foreach (var contato in fornecedor.Contatos)
                        {
                            contato.IdFornecedor = fornecedor.IdFornecedor;
                            await contatoRepository.AtualizaContato(contato);
                            contatosAtualizados++;
                        }
                    }

                    return (linhasAfetadas + contatosAtualizados);
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

        public async Task<int> ExcluirFornecedor(int idFornecedor)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    var asContato = await contatoRepository.GetContatos(0, idFornecedor);
                    if (asContato.Count > 0)
                    {
                        foreach (var contato in asContato)
                        {
                            var contatosExcluidos = await contatoRepository.ExcluirContato(0, contato.IdContato, contato.IdFornecedor);
                        }
                    }
                    using var command = ComWrapperFac.CreateCommand("sp_EXCLUIR_INFO_FORNECEDOR");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    if (idFornecedor > 0)
                        command.Parameters.AddWithValue("@ID_FORNECEDOR", idFornecedor);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = await command.ExecuteNonQueryAsync();


                    return linhasAfetadas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Fornecedor>> GetAllFornecedores()
        {
            var result = new List<Fornecedor>();
            var contatos = new List<Contato>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    string query = @"SELECT TOP (1000) * FROM FORNECEDOR FORN WITH(NOLOCK)";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var fornecedor = new Fornecedor()
                            {
                                IdFornecedor = Convert.ToInt32(reader["ID_FORNECEDOR"]),
                                NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null,
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null
                            };
                            fornecedor.Contatos = await contatoRepository.GetContatos(0, fornecedor.IdFornecedor);
                            result.Add(fornecedor);
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
    }
}
