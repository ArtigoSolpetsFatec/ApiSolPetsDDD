using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class ContasRepository : IContasRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;

        public ContasRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
        }

        public async Task<PagamentoContasFixas> GetContaById(int idConta)
        {
            var result = new PagamentoContasFixas();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CONTAS_FIXAS_BY_ID");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CONTAS_FIXAS", idConta);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.Funcionario = new();
                        result.IdContas = idConta;
                        result.IdPagamento = reader["ID_PAGAMENTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PAGAMENTO"]);
                        result.Funcionario.IdFuncionario = reader["ID_FUNCIONARIO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FUNCIONARIO"]);
                        result.Funcionario.IdCargo = reader["ID_CARGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CARGO"]);
                        result.Funcionario.NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null;
                        result.Funcionario.NomeCargo = reader["NOME_CARGO"].ToString() ?? null;
                        result.DataPagamento = reader["DATA_PAGAMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_PAGAMENTO"]);
                        result.ValorConta = reader["VALOR_CONTA"] == DBNull.Value ? 0 : Convert.ToDouble(reader["VALOR_CONTA"]);
                        result.TipoConta = reader["TIPO_CONTA"].ToString() ?? null;
                        result.Empresa = reader["EMPRESA_CONTA"].ToString() ?? null;
                        result.DataVencimento = reader["DATA_VENCIMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENCIMENTO"]);
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PagamentoContasFixas>> GetContasAVencer()
        {
            var result = new List<PagamentoContasFixas>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CONTAS_FIXAS_A_VENCER");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var funcionario = new Funcionario()
                            {
                                IdFuncionario = reader["ID_FUNCIONARIO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FUNCIONARIO"]),
                                IdCargo = reader["ID_CARGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CARGO"]),
                                NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null,
                                NomeCargo = reader["NOME_CARGO"].ToString() ?? null,
                            };
                            var infoConta = new PagamentoContasFixas()
                            {
                                IdContas = reader["ID_CONTAS_FIXAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CONTAS_FIXAS"]),
                                IdPagamento = reader["ID_PAGAMENTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PAGAMENTO"]),
                                Funcionario = funcionario,
                                DataPagamento = reader["DATA_PAGAMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_PAGAMENTO"]),
                                ValorConta = reader["VALOR_CONTA"] == DBNull.Value ? 0 : Convert.ToDouble(reader["VALOR_CONTA"]),
                                TipoConta = reader["TIPO_CONTA"].ToString() ?? null,
                                Empresa = reader["EMPRESA_CONTA"].ToString() ?? null,
                                DataVencimento = reader["DATA_VENCIMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENCIMENTO"])
                            };
                            result.Add(infoConta);
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

        public async Task<List<PagamentoContasFixas>> GetContasVencidas()
        {
            var result = new List<PagamentoContasFixas>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CONTAS_FIXAS_VENCIDAS");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var funcionario = new Funcionario()
                            {
                                IdFuncionario = reader["ID_FUNCIONARIO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FUNCIONARIO"]),
                                IdCargo = reader["ID_CARGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CARGO"]),
                                NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null,
                                NomeCargo = reader["NOME_CARGO"].ToString() ?? null,
                            };
                            var infoConta = new PagamentoContasFixas()
                            {
                                IdContas = reader["ID_CONTAS_FIXAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CONTAS_FIXAS"]),
                                IdPagamento = reader["ID_PAGAMENTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PAGAMENTO"]),
                                Funcionario = funcionario,
                                DataPagamento = reader["DATA_PAGAMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_PAGAMENTO"]),
                                ValorConta = reader["VALOR_CONTA"] == DBNull.Value ? 0 : Convert.ToDouble(reader["VALOR_CONTA"]),
                                TipoConta = reader["TIPO_CONTA"].ToString() ?? null,
                                Empresa = reader["EMPRESA_CONTA"].ToString() ?? null,
                                DataVencimento = reader["DATA_VENCIMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENCIMENTO"])
                            };
                            result.Add(infoConta);
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

        public async Task<List<PagamentoContasFixas>> GetContasAVencerMesCorrente()
        {
            var result = new List<PagamentoContasFixas>();
            var mesCorrente = DateTime.Now.Month;
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CONTAS_FIXAS_MES");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MES_CONSULTA", mesCorrente);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var funcionario = new Funcionario()
                            {
                                IdFuncionario = reader["ID_FUNCIONARIO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FUNCIONARIO"]),
                                IdCargo = reader["ID_CARGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CARGO"]),
                                NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null,
                                NomeCargo = reader["NOME_CARGO"].ToString() ?? null,
                            };
                            var infoConta = new PagamentoContasFixas()
                            {
                                IdContas = reader["ID_CONTAS_FIXAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CONTAS_FIXAS"]),
                                IdPagamento = reader["ID_PAGAMENTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PAGAMENTO"]),
                                Funcionario = funcionario,
                                DataPagamento = reader["DATA_PAGAMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_PAGAMENTO"]),
                                ValorConta = reader["VALOR_CONTA"] == DBNull.Value ? 0 : Convert.ToDouble(reader["VALOR_CONTA"]),
                                TipoConta = reader["TIPO_CONTA"].ToString() ?? null,
                                Empresa = reader["EMPRESA_CONTA"].ToString() ?? null,
                                DataVencimento = reader["DATA_VENCIMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENCIMENTO"])
                            };
                            result.Add(infoConta);
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

        public async Task<ContasFixas> CadastrarConta(ContasFixas contas)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_CONTA");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    var dhVencimento = contas.DataVencimento.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.AddWithValue("@DATA_VENCIMENTO", DateTime.Parse(dhVencimento));
                    command.Parameters.AddWithValue("@TIPO_CONTA", contas.TipoConta);
                    command.Parameters.AddWithValue("@EMPRESA_CONTA", contas.Empresa);
                    command.Parameters.AddWithValue("@VALOR_CONTA", contas.ValorConta);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovaConta = Convert.ToInt32(await command.ExecuteScalarAsync());

                    var result = await GetContaById(idNovaConta);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagamentoContasFixas> GetContaPagaById(int idPagamentoConta)
        {
            var result = new PagamentoContasFixas();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PAGAMENTO_CONTAS_FIXAS_BY_ID");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_PAGAMENTO", idPagamentoConta);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdContas = reader["ID_CONTAS_FIXAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CONTAS_FIXAS"]);
                        result.IdPagamento = idPagamentoConta;
                        result.Funcionario.IdFuncionario = reader["ID_FUNCIONARIO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FUNCIONARIO"]);
                        result.Funcionario.IdCargo = reader["ID_CARGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CARGO"]);
                        result.Funcionario.NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null;
                        result.Funcionario.NomeCargo = reader["NOME_CARGO"].ToString() ?? null;
                        result.DataPagamento = reader["DATA_PAGAMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_PAGAMENTO"]);
                        result.ValorConta = reader["VALOR_CONTA"] == DBNull.Value ? 0 : Convert.ToDouble(reader["VALOR_CONTA"]);
                        result.TipoConta = reader["TIPO_CONTA"].ToString() ?? null;
                        result.Empresa = reader["EMPRESA_CONTA"].ToString() ?? null;
                        result.DataVencimento = reader["DATA_VENCIMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENCIMENTO"]);
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagamentoContasFixas> CadastrarPagamentoConta(PagamentoContasFixas pagConta)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_PAGAMENTO_CONTA");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    var dhPagamento = pagConta.DataPagamento.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.AddWithValue("@DATA_PAGAMENTO", DateTime.Parse(dhPagamento));
                    command.Parameters.AddWithValue("@ID_CONTAS_FIXAS", pagConta.IdContas);
                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", pagConta.Funcionario.IdFuncionario);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idPagNovaConta = Convert.ToInt32(await command.ExecuteScalarAsync());

                    var result = await GetContaPagaById(idPagNovaConta);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizarConta(ContasFixas contas)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_CONTAS_FIXAS");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    var dhVencimento = contas.DataVencimento.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.AddWithValue("@DATA_VENCIMENTO", DateTime.Parse(dhVencimento));
                    command.Parameters.AddWithValue("@TIPO_CONTA", contas.TipoConta);
                    command.Parameters.AddWithValue("@EMPRESA_CONTA", contas.Empresa);
                    command.Parameters.AddWithValue("@VALOR_CONTA", contas.ValorConta);
                    command.Parameters.AddWithValue("@ID_CONTAS_FIXAS", contas.IdContas);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var result = Convert.ToInt32(await command.ExecuteNonQueryAsync());
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizarPagamentoConta(PagamentoContasFixas pagConta)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_PAGAMENTO_CONTAS_FIXAS");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    var dhPagamento = pagConta.DataPagamento.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.AddWithValue("@DATA_PAGAMENTO", DateTime.Parse(dhPagamento));
                    command.Parameters.AddWithValue("@ID_CONTAS_FIXAS", pagConta.IdContas);
                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", pagConta.Funcionario.IdFuncionario);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var result = Convert.ToInt32(await command.ExecuteNonQueryAsync());
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ExcluirConta(int idConta)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("DELETE FROM CONTAS_FIXAS WHERE ID_CONTAS_FIXAS=@ID_CONTAS_FIXAS");
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ID_CONTAS_FIXAS", idConta);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var result = Convert.ToInt32(await command.ExecuteNonQueryAsync());
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ExcluirPagamentoConta(int idPagConta)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("DELETE FROM PAGAMENTO_CONTAS_FIXAS WHERE ID_PAGAMENTO=@ID_PAGAMENTO");
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ID_PAGAMENTO", idPagConta);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var result = Convert.ToInt32(await command.ExecuteNonQueryAsync());
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ContasFixas>> GetContasDoMes()
        {
            var result = new List<ContasFixas>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_CONTAS_MES_CORRENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var conta = new ContasFixas()
                            {
                                DataVencimento = reader["DATA_VENCIMENTO"] == DBNull.Value ?
                                new DateTime() : Convert.ToDateTime(reader["DATA_VENCIMENTO"]),
                                TipoConta = reader["TIPO_CONTA"].ToString() ?? null,
                                Empresa = reader["EMPRESA_CONTA"].ToString() ?? null,
                                ValorConta = reader["VALOR_CONTA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_CONTA"]),
                                IdContas = reader["ID_CONTAS_FIXAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CONTAS_FIXAS"]),
                            };
                            result.Add(conta);
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

        public async Task<List<ContasFixas>> GetContasDoDia()
        {
            var result = new List<ContasFixas>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_CONTAS_DIA_CORRENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var conta = new ContasFixas()
                            {
                                DataVencimento = reader["DATA_VENCIMENTO"] == DBNull.Value ?
                                new DateTime() : Convert.ToDateTime(reader["DATA_VENCIMENTO"]),
                                TipoConta = reader["TIPO_CONTA"].ToString() ?? null,
                                Empresa = reader["EMPRESA_CONTA"].ToString() ?? null,
                                ValorConta = reader["VALOR_CONTA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_CONTA"]),
                                IdContas = reader["ID_CONTAS_FIXAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CONTAS_FIXAS"]),
                            };
                            result.Add(conta);
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

        public async Task<List<ContasFixas>> GetContasDoAno()
        {
            var result = new List<ContasFixas>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_CONTAS_ANO_CORRENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var conta = new ContasFixas()
                            {
                                DataVencimento = reader["DATA_VENCIMENTO"] == DBNull.Value ?
                                new DateTime() : Convert.ToDateTime(reader["DATA_VENCIMENTO"]),
                                TipoConta = reader["TIPO_CONTA"].ToString() ?? null,
                                Empresa = reader["EMPRESA_CONTA"].ToString() ?? null,
                                ValorConta = reader["VALOR_CONTA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_CONTA"]),
                                IdContas = reader["ID_CONTAS_FIXAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CONTAS_FIXAS"]),
                            };
                            result.Add(conta);
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
