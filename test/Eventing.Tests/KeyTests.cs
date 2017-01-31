using FluentAssertions;
using Xunit;

namespace Eventing.Tests
{
    public class KeyTests
    {
        [Fact]
        public void ImplicitStringConversionTests()
        {
            var expected = "expected";
            Key key = "expected";
            string actual = key;
            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void ImplicitIntConversionTests()
        {
            var expected = 100;
            Key key = 100;
            int actual = key;
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
