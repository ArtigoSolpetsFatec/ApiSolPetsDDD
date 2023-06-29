namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface ISqlComWrapperFac
    {
        ISqlComWrapper CreateCommand(string cmdText);
    }
}
