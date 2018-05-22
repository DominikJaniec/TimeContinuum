using System;
using TimeContinuum;
using TimeContinuum.TestComposerSharded;
using Xunit;

namespace TimeContinuumTests
{
    public class TestComposerTests
    {
        [Fact]
        public void WhenExecuted_ShouldCall_PrepareFactory()
        {
            const string methodName = nameof(TestComposer.IExecutable<object>.Execute);

            var called = false;
            object SampleFactory(IClockFlow _)
            {
                called = true;
                return new object();
            }

            var preparedExecutable = TestComposer
                .Prepare(SampleFactory);

            Assert.False(called, $"Expecting to not call given Factory before '{methodName}'.");

            preparedExecutable
                .Execute();

            Assert.True(called, $"Expecting to call given Factory after '{methodName}'.");
        }

        [Fact]
        public void WhenExecuted_ShouldYield_DefaultSubstitutableClockFlow()
        {
            var passedFlow = default(IClockFlow);

            TestComposer
                .Prepare(flow => passedFlow = flow)
                .Execute();

            Assert.NotNull(passedFlow);
            Assert.Same(passedFlow, SubstitutableClockFlow.Default);
        }

        [Fact]
        public void WhenExecuted_ShouldYield_PreparedInitialClockFlow()
        {
            // TODO: Make this values more unique:
            var instance = DateTimeOffset.Now;
            var currentClick = DateTime.UtcNow;
            var config = TimeZoneInfo.Local;

            var preparable = TestComposer
                .WithInitial(instance)
                .WithInitial(currentClick)
                .WithInitial(config);

            var passedFlow = default(IClockFlow);

            preparable
                .Prepare(flow => passedFlow = flow)
                .Execute();

            Assert.NotNull(passedFlow);
            Assert.Equal(instance, passedFlow.Instance);
            Assert.Equal(currentClick, passedFlow.CurrentClick);
            Assert.Equal(config, passedFlow.Config);
        }
    }
}
