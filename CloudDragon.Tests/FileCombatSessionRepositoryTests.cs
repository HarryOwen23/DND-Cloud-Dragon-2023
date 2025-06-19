using System.IO;
using System.Threading.Tasks;
using Xunit;
using CloudDragon.CloudDragonApi.Functions.Combat;

namespace CloudDragon.Tests
{
public class FileCombatSessionRepositoryTests
{
    [Fact]
    public async Task Save_and_get_round_trip()
    {
        var dir = Path.Combine(Path.GetTempPath(), "combat-tests");
        var repo = new FileCombatSessionRepository(dir);
        var session = new CombatSession { Name = "test-session" };

        await repo.SaveAsync(session);
        var loaded = await repo.GetAsync(session.Id);

        Assert.NotNull(loaded);
        Assert.Equal(session.Id, loaded.Id);
        Directory.Delete(dir, true);
    }
}
}
