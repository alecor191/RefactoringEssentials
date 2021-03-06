using NUnit.Framework;
using RefactoringEssentials.CSharp.Diagnostics;

namespace RefactoringEssentials.Tests.CSharp.Diagnostics
{
    [TestFixture]
    public class ReplaceWithSingleCallToLastOrDefaultTests : CSharpDiagnosticTestBase
    {
        [Test]
        public void TestSimpleCase()
        {
            Analyze<ReplaceWithSingleCallToLastOrDefaultAnalyzer>(@"using System.Linq;
public class CSharpDemo {
    public void Bla () {
        int[] arr;
        var bla = $arr.Where(x => x < 4).LastOrDefault()$;
    }
}", @"using System.Linq;
public class CSharpDemo {
    public void Bla () {
        int[] arr;
        var bla = arr.LastOrDefault(x => x < 4);
    }
}");
        }

        [Test]
        public void TestDisable()
        {
            Analyze<ReplaceWithSingleCallToLastOrDefaultAnalyzer>(@"using System.Linq;
public class CSharpDemo {
	public void Bla () {
		int[] arr;
#pragma warning disable " + CSharpDiagnosticIDs.ReplaceWithSingleCallToLastOrDefaultAnalyzerID + @"
		var bla = arr.Where (x => x < 4).LastOrDefault ();
	}
}");
        }
    }
}
