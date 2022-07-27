using System.Diagnostics.CodeAnalysis;
using HoloCure.Game.API.Exceptions;

namespace HoloCure.Game.API
{
    /// <summary>
    ///     Represents a string lexer identifier, used to uniquely identify content.
    /// </summary>
    public readonly record struct Identifier(string Namespace, string Content)
    {
        public override string ToString() => $"{Namespace}:{Content}";

        /// <summary>
        ///     Parses a string into an identifier.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed identifier.</returns>
        /// <exception cref="IdentifierParseException">If parsing fails.</exception>
        public static Identifier Parse(string value)
        {
            if (TryParse(value, out Identifier? identifier)) return identifier.Value;

            throw new IdentifierParseException("Failed to parse string into Identifier: " + value);
        }

        /// <summary>
        ///     Attempts to parse a string into an Identifier.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="identifier">The resulting parsed identifier, which will be null if this method returns false.</param>
        /// <returns>True if the string was successfully parsed, false otherwise.</returns>
        public static bool TryParse(string value, [NotNullWhen(true)] out Identifier? identifier)
        {
            identifier = null;
            string[] parts = value.Split(':', 2);

            if (parts.Length != 2) return false;

            identifier = new Identifier(parts[0], parts[1]);
            return true;
        }

        public static implicit operator Identifier(string value) => Parse(value);
        public static implicit operator string(Identifier value) => value.ToString();
    }
}
