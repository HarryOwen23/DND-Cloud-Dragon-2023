using System.Collections.Generic;
using Xunit;

namespace CloudDragon.Tests
{
public class PointBuyBuilderTests
{
    [Fact]
    public void GenerateStats_returns_stats_for_valid_input()
    {
        var builder = new CharacterStatsPointBuy();
        var input = new Dictionary<string, int>
        {
            ["STR"] = 8,
            ["DEX"] = 10,
            ["CON"] = 12,
            ["INT"] = 13,
            ["WIS"] = 14,
            ["CHA"] = 15
        };
        var result = builder.GenerateStats(input);
        Assert.Equal(6, result.Count);
        Assert.Equal(8, result["STR"]);
        Assert.Equal(10, result["DEX"]);
        Assert.Equal(12, result["CON"]);
        Assert.Equal(13, result["INT"]);
        Assert.Equal(14, result["WIS"]);
        Assert.Equal(15, result["CHA"]);
    }

    [Fact]
    public void GenerateStats_invalid_total_throws()
    {
        var builder = new CharacterStatsPointBuy();
        var input = new Dictionary<string, int>
        {
            ["STR"] = 15,
            ["DEX"] = 15,
            ["CON"] = 15,
            ["INT"] = 15,
            ["WIS"] = 15,
            ["CHA"] = 15
        };
        Assert.Throws<ArgumentException>(() => builder.GenerateStats(input));
    }
}
}
