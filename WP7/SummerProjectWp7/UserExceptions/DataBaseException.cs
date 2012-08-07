namespace SummerProjectWp7.UserExceptions
{
    using System;

    public class DataBaseException : Exception
    {
        /// <summary>
        /// This Exception is thrown if an error occurs while saving or reading from the database.
        /// </summary>
        public DataBaseException()
        { }

        /// <summary>
        /// This Exception is thrown if an error occurs while saving or reading from the database.
        /// </summary>
        /// <param name="message">The error message which is thrown</param>
        public DataBaseException(string message) : base(message) { }

        /// <summary>
        /// This Exception is thrown if an error occurs while saving or reading from the database.
        /// </summary>
        /// <param name="message">The error message which is thrown</param>
        /// <param name="inner">The base class</param>
        public DataBaseException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
