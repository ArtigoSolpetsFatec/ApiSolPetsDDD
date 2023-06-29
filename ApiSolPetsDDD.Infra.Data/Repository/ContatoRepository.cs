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
    public class ContatoRepository : IContatoRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;

        public ContatoRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
        }

        public async Task<List<Contato>> GetContatos(int idCliente = 0, int idFornecedor = 0)
        {
            var result = new List<Contato>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CONTATO_BY_ID");
                    command.CommandType = CommandType.StoredProcedure;
                    if (idCliente.ToString() != string.Empty || idCliente.ToString() != null || idCliente > 0)
                        command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    if (idFornecedor.ToString() != string.Empty || idFornecedor.ToString() != null || idFornecedor > 0)
                        command.Parameters.AddWithValue("@ID_FORNECEDOR", idFornecedor);
                    if (idCliente.ToString() == null && idFornecedor.ToString() == null
                       || idCliente.ToString() == string.Empty && idFornecedor.ToString() == string.Empty
                       || idCliente == 0 && idFornecedor == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar id do cliente ou do fornecedor!");
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
                                IdCliente = reader["codigoCliente"] == DBNull.Value ? 0 : Convert.ToInt32(reader["codigoCliente"]),
                                IdFornecedor = reader["codigoFornecedor"] == DBNull.Value ? 0 : Convert.ToInt32(reader["codigoFornecedor"]),
                                IdContato = Convert.ToInt32(reader["codigoContato"]),
                                TelefoneCelular = reader["celular"].ToString() ?? null,
                                OutroCelular = reader["outroCelular"].ToString() ?? null,
                                TelefoneFixo = reader["telefoneFixo"].ToString() ?? null,
                                EmailPrincipal = reader["emailPrincipal"].ToString() ?? null,
                                EmailSecundario = reader["emailSecundario"].ToString() ?? null,
                                DHUltimaAtualizacao = reader["dhUltimaAtualizacao"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["dhUltimaAtualizacao"])
                            };
                            result.Add(contato);
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

        public async Task<Contato> CadastrarContato(Contato contato)
        {
            var result = new Contato();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_CONTATO");
                    command.CommandType = CommandType.StoredProcedure;
                    if (contato.IdFornecedor == 0 && contato.IdCliente == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - É obrigatório informar o id do cliente ou do fornecedor!");
                    if (contato.IdCliente != 0)
                        command.Parameters.AddWithValue("@ID_CLIENTE", contato.IdCliente);
                    if (contato.IdFornecedor != 0)
                        command.Parameters.AddWithValue("@ID_FORNECEDOR", contato.IdFornecedor);
                    if (!string.IsNullOrEmpty(contato.TelefoneCelular))
                        command.Parameters.AddWithValue("@TELEFONE_CELULAR", contato.TelefoneCelular);
                    if (!string.IsNullOrEmpty(contato.OutroCelular))
                        command.Parameters.AddWithValue("@OUTRO_CELULAR", contato.OutroCelular);
                    if (!string.IsNullOrEmpty(contato.TelefoneFixo))
                        command.Parameters.AddWithValue("@TELEFONE_FIXO", contato.TelefoneFixo);
                    if (!string.IsNullOrEmpty(contato.EmailPrincipal))
                        command.Parameters.AddWithValue("@EMAIL_PRINCIPAL", contato.EmailPrincipal);
                    if (!string.IsNullOrEmpty(contato.EmailSecundario))
                        command.Parameters.AddWithValue("@EMAIL_SECUNDARIO", contato.EmailSecundario);

                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idContato = Convert.ToInt32(await command.ExecuteScalarAsync());

                    List<Contato> contatos = new();
                    if (contato.IdCliente > 0)
                        contatos = await GetContatos(contato.IdCliente, 0);
                    else
                        contatos = await GetContatos(0, contato.IdFornecedor);


                    foreach (var cont in contatos)
                    {
                        if (cont.IdContato == idContato)
                            result = cont;
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

        public async Task<int> AtualizaContato(Contato contatoCliente)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_CONTATO_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    if (contatoCliente.IdContato == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do contato!");

                    if (contatoCliente.IdContato > 0)
                        command.Parameters.AddWithValue("@ID_CONTATO", contatoCliente.IdContato);

                    if (contatoCliente.OutroCelular != string.Empty)
                        command.Parameters.AddWithValue("@OUTRO_CELULAR", contatoCliente.OutroCelular);

                    if (contatoCliente.TelefoneCelular != string.Empty)
                        command.Parameters.AddWithValue("@TELEFONE_CELULAR", contatoCliente.TelefoneCelular);

                    if (contatoCliente.TelefoneFixo != string.Empty)
                        command.Parameters.AddWithValue("@TELEFONE_FIXO", contatoCliente.TelefoneFixo);

                    if (contatoCliente.EmailPrincipal != string.Empty)
                        command.Parameters.AddWithValue("@EMAIL_PRINCIPAL", contatoCliente.EmailPrincipal);

                    if (contatoCliente.EmailSecundario != string.Empty)
                        command.Parameters.AddWithValue("@EMAIL_SECUNDARIO", contatoCliente.EmailSecundario);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = await command.ExecuteNonQueryAsync();

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

        public async Task<int> ExcluirContato(int idCliente, int idContato, int idFornecedor)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EXCLUIR_CONTATO_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    if (idCliente > 0 && idFornecedor > 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Informe apenas o id do cliente ou do fornecedor!");

                    if (idCliente > 0)
                        command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);

                    if (idFornecedor > 0)
                        command.Parameters.AddWithValue("@ID_FORNECEDOR", idFornecedor);

                    if (idContato == 0)
                        throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do contato!");

                    if (idContato > 0)
                        command.Parameters.AddWithValue("@ID_CONTATO", idContato);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    int linhasAfetadas = await command.ExecuteNonQueryAsync();

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
