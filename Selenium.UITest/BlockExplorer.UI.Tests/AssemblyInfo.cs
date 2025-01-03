using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]

namespace BlockExplorer.UI.Tests
{
    class AssemblyInfo
    {
    }
}
