using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;
        private readonly ILoginRepository loginRepository;
        private readonly ICargoRepository cargoRepository;

        public FuncionarioRepository(IDbConFactory dbConnectionFactory,
            ISqlComWrapperFac commandWrapperFactory, ILoginRepository loginRepository,
            ICargoRepository cargoRepository)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
            this.loginRepository = loginRepository;
            this.cargoRepository = cargoRepository;
        }

        public async Task<Funcionario> GetInfoFuncionario(int idFuncionario)
        {
            var result = new Funcionario();
            result.Login = new Login();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_FUNCIONARIO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", idFuncionario);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdFuncionario = Convert.ToInt32(reader["ID_FUNCIONARIO"]);
                        result.NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null;
                        result.Cpf = reader["CPF_FUNCIONARIO"].ToString() ?? null;
                        result.Rg = reader["RG_FUNCIONARIO"].ToString() ?? null;
                        result.QtdeDependentes = reader["QTDE_DEPENDENTES"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QTDE_DEPENDENTES"]);
                        result.DhNascimento = reader["DATA_NASCIMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_NASCIMENTO"]);
                        result.DhInicio = reader["DATA_INICIO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_INICIO"]);
                        result.DhUltimaAtualizacaoFunc = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                        result.DhInativo = reader["DATA_HORA_INATIVIDADE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_INATIVIDADE"]);
                        result.Login.IdLogin = Convert.ToInt32(reader["ID_LOGIN"]);
                        result.Login.Email = reader["EMAIL_LOGIN"].ToString() ?? null;
                        result.Login.IsAdmin = reader["IS_ADMIN"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_ADMIN"]);
                        result.Login.Senha = reader["SENHA_LOGIN"].ToString() ?? null;
                        result.Login.DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                        result.IdCargo = Convert.ToInt32(reader["ID_CARGO"]);
                        result.NomeCargo = reader["NOME_CARGO"].ToString() ?? null;
                        result.Salario = reader["SALARIO_CARGO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["SALARIO_CARGO"]);
                        result.DhUltimaAtualizacaoFunc = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Funcionario> GetFuncionarioByName(string nomeFuncionario)
        {
            var result = new Funcionario();
            result.Login = new Login();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    var query = @"SELECT FUNC.*, LG.* FROM FUNCIONARIO FUNC WITH(NOLOCK)
                                  INNER JOIN LOGIN_USER LG WITH(NOLOCK) ON LG.ID_LOGIN=FUNC.ID_LOGIN
                                  WHERE NOME_FUNCIONARIO LIKE @NOME_FUNCIONARIO";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@NOME_FUNCIONARIO", nomeFuncionario);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdFuncionario = reader["ID_FUNCIONARIO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FUNCIONARIO"]);
                        result.NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null;
                        result.Cpf = reader["CPF_FUNCIONARIO"].ToString() ?? null;
                        result.Rg = reader["RG_FUNCIONARIO"].ToString() ?? null;
                        result.QtdeDependentes = reader["QTDE_DEPENDENTES"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QTDE_DEPENDENTES"]);
                        result.DhNascimento = reader["DATA_NASCIMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_NASCIMENTO"]);
                        result.DhInicio = reader["DATA_INICIO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_INICIO"]);
                        result.DhUltimaAtualizacaoFunc = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                        result.DhInativo = reader["DATA_HORA_INATIVIDADE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_INATIVIDADE"]);
                        result.Login.IdLogin = reader["ID_LOGIN"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_LOGIN"]);
                        result.Login.Email = reader["EMAIL_LOGIN"].ToString() ?? null;
                        result.Login.IsAdmin = reader["IS_ADMIN"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_ADMIN"]);
                        result.Login.Senha = reader["SENHA_LOGIN"].ToString() ?? null;
                        result.Login.DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                        result.IdCargo = reader["ID_CARGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CARGO"]);
                        result.NomeCargo = reader["NOME_CARGO"].ToString() ?? null;
                        result.Salario = reader["SALARIO_CARGO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["SALARIO_CARGO"]);
                        result.DhUltimaAtualizacaoFunc = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Funcionario> GetFuncionarioByIdLogin(int idLogin)
        {
            var result = new Funcionario();
            result.Login = new Login();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_FUNCIONARIO_BY_ID_LOGIN");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_LOGIN", idLogin);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdFuncionario = Convert.ToInt32(reader["ID_FUNCIONARIO"]);
                        result.NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null;
                        result.Cpf = reader["CPF_FUNCIONARIO"].ToString() ?? null;
                        result.Rg = reader["RG_FUNCIONARIO"].ToString() ?? null;
                        result.QtdeDependentes = reader["QTDE_DEPENDENTES"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QTDE_DEPENDENTES"]);
                        result.DhNascimento = reader["DATA_NASCIMENTO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_NASCIMENTO"]);
                        result.DhInicio = reader["DATA_INICIO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_INICIO"]);
                        result.DhUltimaAtualizacaoFunc = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                        result.DhInativo = reader["DATA_HORA_INATIVIDADE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_INATIVIDADE"]);
                        result.Login.IdLogin = Convert.ToInt32(reader["ID_LOGIN"]);
                        result.Login.Email = reader["EMAIL_LOGIN"].ToString() ?? null;
                        result.Login.IsAdmin = reader["IS_ADMIN"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_ADMIN"]);
                        result.Login.Senha = reader["SENHA_LOGIN"].ToString() ?? null;
                        result.Login.DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                        result.IdCargo = Convert.ToInt32(reader["ID_CARGO"]);
                        result.NomeCargo = reader["NOME_CARGO"].ToString() ?? null;
                        result.Salario = reader["SALARIO_CARGO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["SALARIO_CARGO"]);
                        result.DhUltimaAtualizacaoFunc = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Funcionario> CadastrarFuncionario(Funcionario funcionario)
        {
            try
            {
                var cargo = await cargoRepository.GetCargoById(funcionario.IdCargo);
                funcionario.Login.IsAdmin = cargo.NomeCargo.ToUpper().Contains("GERENTE");
                var login = await loginRepository.CriarLogin(funcionario.Login);

                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_FUNCIONARIO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@NOME_FUNCIONARIO", funcionario.NomeCompleto);
                    command.Parameters.AddWithValue("@CPF_FUNCIONARIO", funcionario.Cpf);
                    command.Parameters.AddWithValue("@RG_FUNCIONARIO", funcionario.Rg);
                    command.Parameters.AddWithValue("@DATA_INICIO", funcionario.DhInicio);
                    command.Parameters.AddWithValue("@DATA_NASCIMENTO", funcionario.DhNascimento);
                    command.Parameters.AddWithValue("@QTDE_DEPENDENTES", funcionario.QtdeDependentes);
                    command.Parameters.AddWithValue("@ID_CARGO", funcionario.IdCargo);
                    command.Parameters.AddWithValue("@ID_LOGIN", login.IdLogin);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovoFuncionario = Convert.ToInt32(await command.ExecuteScalarAsync());
                    var result = await GetInfoFuncionario(idNovoFuncionario);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizarFuncionario(Funcionario funcionario)
        {
            try
            {
                if (funcionario.Login != null && funcionario.Login.IdLogin > 0)
                    await loginRepository.AtualizarLogin(funcionario.Login);
                if (funcionario.Login != null && funcionario.Login.IdLogin == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Para atualizar informações de login, obrigatório informar o id login");

                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_FUNCIONARIO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", funcionario.IdFuncionario);
                    command.Parameters.AddWithValue("@NOME_FUNCIONARIO", funcionario.NomeCompleto);
                    command.Parameters.AddWithValue("@CPF_FUNCIONARIO", funcionario.Cpf);
                    command.Parameters.AddWithValue("@RG_FUNCIONARIO", funcionario.Rg);
                    command.Parameters.AddWithValue("@DATA_INICIO", funcionario.DhInicio);
                    command.Parameters.AddWithValue("@DATA_NASCIMENTO", funcionario.DhNascimento);
                    command.Parameters.AddWithValue("@QTDE_DEPENDENTES", funcionario.QtdeDependentes);
                    command.Parameters.AddWithValue("@ID_CARGO", funcionario.IdCargo);
                    command.Parameters.AddWithValue("@ID_LOGIN", funcionario.Login?.IdLogin);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovoFuncionario = Convert.ToInt32(await command.ExecuteScalarAsync());
                    var result = await GetInfoFuncionario(idNovoFuncionario);

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

        public async Task<int> ExcluirFuncionario(int idFuncionario)
        {

            try
            {
                var funcionario = await GetInfoFuncionario(idFuncionario);
                if (funcionario?.IdFuncionario > 0 && funcionario.Login?.IdLogin > 0)
                {
                    var idLoginFunc = funcionario.Login.IdLogin;

                    using (var connection = DbConFactory.CreateConnection())
                    {
                        using var command = ComWrapperFac.CreateCommand("sp_EXCLUIR_INFO_FUNCIONARIO");
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID_FUNCIONARIO", idFuncionario);
                        command.Parameters.AddWithValue("@ID_LOGIN", idLoginFunc);
                        command.Connection = connection;
                        string commandString = command.SqlCommandToString();
                        connection.Open();

                        var result = Convert.ToInt32(await command.ExecuteNonQueryAsync());
                        return result;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
