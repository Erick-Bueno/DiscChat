using Xunit;

[CollectionDefinition("database")]
public class DatabaseCollection: ICollectionFixture<DbFixture>
{
    
}