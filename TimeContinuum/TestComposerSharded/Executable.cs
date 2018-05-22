using System;
using static TimeContinuum.TestComposer;

namespace TimeContinuum.TestComposerSharded
{
    internal static class Executable
    {
        public static IExecutable<TSubject> As<TSubject>(Func<IClockFlow, TSubject> factory, IClockFlow sourceFlow)
            => new Executable<TSubject>(factory, sourceFlow);
    }

    internal struct Executable<TSubject> : IExecutable<TSubject>
    {
        private readonly Func<IClockFlow, TSubject> _factory;
        private readonly IClockFlow _sourceFlow;

        public Executable(
            Func<IClockFlow, TSubject> factory,
            IClockFlow sourceFlow)
        {
            _factory = factory;
            _sourceFlow = sourceFlow;
        }

        void IExecutable<TSubject>.Execute()
        {
            var subject = _factory(_sourceFlow);
        }
    }
}
