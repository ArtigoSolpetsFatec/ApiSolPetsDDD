using ApiSolPetsDDD.Domain.Interfaces;

namespace ApiSolPetsDDD.Domain.Factory
{
    public class SqlComWrapperFac : ISqlComWrapperFac
    {
        public ISqlComWrapper CreateCommand(string cmdText)
        {
            return new SqlComWrapper(cmdText);
        }
    }
}
