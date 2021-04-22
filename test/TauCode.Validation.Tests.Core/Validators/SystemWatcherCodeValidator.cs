using TauCode.Validation.Codes;

namespace TauCode.Validation.Tests.Core.Validators
{
    public class SystemWatcherCodeValidator<T> : LowercaseCodeValidator<T>
    {
        public SystemWatcherCodeValidator()
            : base(
                1,
                DataConstants.SystemWatcher.MaxSystemWatcherCodeLength,
                '-',
                true)
        {
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "'{PropertyName}' must be a valid System Watcher code.";
        }

        public override string Name => "SystemWatcherCodeValidator";
    }
}
