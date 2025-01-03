using NUnit.Framework;
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]

namespace AdminTool.UITests
{
    internal class AssemblyInfo
    {
    }
}
