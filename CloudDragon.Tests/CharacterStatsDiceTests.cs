using Xunit;

public class CharacterStatsDiceTests
{
    [Fact]
    public void Generate_returns_scores_between_3_and_18()
    {
        var stats = CharacterStatsDice.Generate();
        Assert.InRange(stats.Strength, 3, 18);
        Assert.InRange(stats.Dexterity, 3, 18);
        Assert.InRange(stats.Constitution, 3, 18);
        Assert.InRange(stats.Intelligence, 3, 18);
        Assert.InRange(stats.Wisdom, 3, 18);
        Assert.InRange(stats.Charisma, 3, 18);
    }
}
