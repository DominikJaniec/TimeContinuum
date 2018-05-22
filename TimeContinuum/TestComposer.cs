using System;
using TimeContinuum.TestComposerSharded;

namespace TimeContinuum
{
    public static class TestComposer
    {
        public interface IPreparable
        {
            IPreparable WithInitial(DateTimeOffset instance);
            IPreparable WithInitial(DateTime currentClick);
            IPreparable WithInitial(TimeZoneInfo config);
            IExecutable<TSubject> Prepare<TSubject>(Func<IClockFlow, TSubject> factory);
        }

        public interface IExecutable<TSubject>
        {
            void Execute();
        }

        public static IPreparable WithInitial(DateTimeOffset instance)
            => Preparable.Initial(instance);

        public static IPreparable WithInitial(DateTime currentClick)
            => Preparable.Initial(currentClick);

        public static IPreparable WithInitial(TimeZoneInfo config)
            => Preparable.Initial(config);

        public static IExecutable<TSubject> Prepare<TSubject>(Func<IClockFlow, TSubject> factory)
            => Executable.As(factory, SubstitutableClockFlow.Default);
    }
}
