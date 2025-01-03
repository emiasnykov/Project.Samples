using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(1)]

namespace Dashboard.UITests
{

    class AssemblyInfo
    {
    }
}