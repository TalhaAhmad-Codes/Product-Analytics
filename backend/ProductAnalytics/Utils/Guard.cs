namespace ProductAnalytics.Utils
{
    public static class Guard
    {
        public static void AgainstNullOrWhitespace(string value, string property)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException($"{property} can't be empty or whitespace.");
        }

        public static void AgainstInvalidMinimumLength(string value, int length, string property)
        {
            if (value.Length < length)
                throw new DomainException($"{property} must be at least '{length}' characters long.");
        }

        public static void AgainstInvalidMaximumLength(string value, int length, string property)
        {
            if (value.Length > length)
                throw new DomainException($"{property} can't contain more than '{length}' characters.");
        }

        public static void AgainstNegative(decimal value, string property)
        {
            if (value < 0)
                throw new DomainException($"{property} can't be negative.");
        }

        public static void AgainstZeroOrLess(decimal value, string property)
        {
            if (value <= 0)
                throw new DomainException($"{property} can't be zero or negative.");
        }

        public static void AgainstZeroOrLess(int value, string property)
        {
            if (value <= 0)
                throw new DomainException($"{property} can't be zero or negative.");
        }

        public static void AgainstInvalidRange(int rangeStart, int rangeEnd, int value, string property)
        {
            if (value > rangeEnd || value < rangeStart)
                throw new DomainException($"{property} can contain value between '{rangeStart}' and '{rangeEnd}'.");
        }
    }
}
