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
            string actual = ((Key<string>)key).Value;
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ImplicitIntConversionTests()
        {
            var expected = 100;
            Key key = 100;
            int actual = ((Key<int>)key).Value; ;
            actual.Should().Be(expected);
        }

        [Fact]
        public void ImplicitGenericTypeConversionTests()
        {
            var expected = new Test { Testing = true };
            Key<Test> key = expected;
            key.Value.Testing = false;
            Test actual = key;
            actual.Should().BeEquivalentTo(expected);
            actual.Testing.Should().Be(expected.Testing);
        }

        private class Test
        {
            public bool Testing { get; set; } = true;
        }
    }
}
