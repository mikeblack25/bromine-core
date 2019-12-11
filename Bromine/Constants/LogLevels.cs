namespace Bromine.Constants
{
    /// <summary>
    /// Supported web browser types.
    /// </summary>
    public enum LogLevels
    {
        /// <summary>
        /// Log only Messages.
        /// </summary>
        Message = 0,

        /// <summary>
        /// Log Messages and Errors.
        /// </summary>
        Error,

        /// <summary>
        /// Log Messages, Errors, and Debug messages.
        /// </summary>
        Debug,

        /// <summary>
        /// Log Messages, Errors, Debug messages, and Framework messages.
        /// </summary>
        Framework
    }
}
