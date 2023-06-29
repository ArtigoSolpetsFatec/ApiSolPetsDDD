using System;

namespace APISolPets.Domain.Extensions
{
    public static class EnviromentsConfigurations
    {
        public static string BdSolPets => Environment.GetEnvironmentVariable("BDSOLPETS");
    }
}
