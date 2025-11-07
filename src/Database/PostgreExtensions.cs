namespace Livestock.Auth.Database;

public static class PostgreExtensions
{
    public const string UUIDGenerator = "uuid-ossp";
    public const string UUIDAlgorithm = "uuid_generate_v4()";
    public const string PostGis = "postgis";
    public const string PgCrypto = "pgcrypto";
    public const string PgAudit = "pgaudit";
    public const string PgTerm = "pg_term";
    public const string FuzzyStrMatch = "fuzzystrmatch";
}