using System;
using static TimeContinuum.TestComposer;

namespace TimeContinuum.TestComposerSharded
{
    internal struct Preparable : IPreparable
    {
        public static IPreparable Default { get; }
            = new Preparable();

        public static IPreparable Initial(DateTimeOffset instance)
            => Default.WithInitial(instance);

        public static IPreparable Initial(DateTime currentClick)
            => Default.WithInitial(currentClick);

        public static IPreparable Initial(TimeZoneInfo config)
            => Default.WithInitial(config);

        IPreparable IPreparable.WithInitial(DateTimeOffset instance)
            => new Preparable(
                new SubstitutableClockFlow(
                    InitialClockFlow,
                    flowInstance: () => instance));

        IPreparable IPreparable.WithInitial(DateTime currentClick)
            => new Preparable(
                new SubstitutableClockFlow(
                    InitialClockFlow,
                    flowCurrentClick: () => currentClick));

        IPreparable IPreparable.WithInitial(TimeZoneInfo config)
            => new Preparable(
                new SubstitutableClockFlow(
                    InitialClockFlow,
                    flowConfig: () => config));

        IExecutable<TSubject> IPreparable.Prepare<TSubject>(Func<IClockFlow, TSubject> factory)
            => Executable.As(factory, InitialClockFlow);

        private readonly SubstitutableClockFlow _clockFlow;

        private Preparable(SubstitutableClockFlow clockFlow)
            => _clockFlow = clockFlow;

        private SubstitutableClockFlow InitialClockFlow
            => _clockFlow ?? SubstitutableClockFlow.Default;
    }
}
