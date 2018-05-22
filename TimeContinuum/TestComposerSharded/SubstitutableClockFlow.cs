using System;

namespace TimeContinuum.TestComposerSharded
{
    public sealed class SubstitutableClockFlow : IClockFlow
    {
        private readonly Func<DateTimeOffset> _flowInstance;
        private readonly Func<DateTime> _flowCurrentClick;
        private readonly Func<TimeZoneInfo> _flowConfig;

        public static SubstitutableClockFlow Default { get; }
            = new SubstitutableClockFlow();

        public DateTimeOffset Instance
            => _flowInstance?.Invoke() ?? Source.Default.Instance;

        public DateTime CurrentClick
            => _flowCurrentClick?.Invoke() ?? Source.Default.CurrentClick;

        public TimeZoneInfo Config
            => _flowConfig?.Invoke() ?? Source.Default.Config;

        public SubstitutableClockFlow(
            SubstitutableClockFlow baseFlow,
            Func<DateTimeOffset> flowInstance = null,
            Func<DateTime> flowCurrentClick = null,
            Func<TimeZoneInfo> flowConfig = null)
        {
            _flowInstance = flowInstance ?? baseFlow._flowInstance;
            _flowCurrentClick = flowCurrentClick ?? baseFlow._flowCurrentClick;
            _flowConfig = flowConfig ?? baseFlow._flowConfig;
        }

        private SubstitutableClockFlow()
        {
            _flowInstance = null;
            _flowCurrentClick = null;
            _flowConfig = null;
        }
    }
}
