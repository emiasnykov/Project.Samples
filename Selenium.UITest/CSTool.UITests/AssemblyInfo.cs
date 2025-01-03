using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]

namespace CSTool.UITests
{
    class AssemblyInfo
    {
    }
}
