using HoloCure.Registry.Exceptions;

namespace HoloCure.Registry
{
    /// <summary>
    ///     Represents a string lexer identifier, used to uniquely identify content.
    /// </summary>
    public readonly record struct Identifier(string Namespace, string Content)
    {
        public override string ToString() {
            return $"{Namespace}:{Content}";
        }

        /// <summary>
        ///     Parses a string into an identifier.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed identifier.</returns>
        /// <exception cref="IdentifierParseException">Thrown if parsing fails.</exception>
        public static Identifier Parse(string value) {
            if (TryParse(value, out Identifier identifier)) return identifier;
            throw new IdentifierParseException($"Failed to parse \"{value}\" to an Identifier.");
        }

        /// <summary>
        ///     Safely parses a string into an identifier.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="identifier">The parsed identifier.</param>
        /// <returns>Whether parsing was successful.</returns>
        public static bool TryParse(string value, out Identifier identifier) {
            string[] parts = value.Split(':', 2);

            identifier = parts.Length == 2 ? new Identifier(parts[0], parts[1]) : new Identifier();
            return parts.Length == 2;
        }

        public static implicit operator Identifier(string value) => Parse(value);
        public static implicit operator string(Identifier value) => value.ToString();
    }
}