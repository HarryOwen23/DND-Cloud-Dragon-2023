using Xunit;

public class StandardArrayTests
{
    [Fact]
    public void Generate_returns_standard_array_values()
    {
        var stats = CharacterStatsStandardArray.Generate();
        Assert.Equal(6, stats.Count);
        Assert.Equal(15, stats["STR"]);
        Assert.Equal(14, stats["DEX"]);
        Assert.Equal(13, stats["CON"]);
        Assert.Equal(12, stats["INT"]);
        Assert.Equal(10, stats["WIS"]);
        Assert.Equal(8, stats["CHA"]);
    }
}
