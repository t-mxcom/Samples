using System;

namespace ConsoleHelper
{
    public static class ColorConsole
    {
        private static readonly ConsoleColor defaultForegroundColor = Console.ForegroundColor;
        private static readonly ConsoleColor defaultBackgroundColor = Console.BackgroundColor;

        public static ConsoleColor StrongForegroundColor { get; set; } = ConsoleColor.White;
        public static ConsoleColor StrongBackgroundColor { get; set; } = ConsoleColor.Black;

        public static ConsoleColor HighlightForegroundColor { get; set; } = ConsoleColor.Yellow;
        public static ConsoleColor HighlightBackgroundColor { get; set; } = ConsoleColor.Black;

        /// <summary>
        /// Prints the passed in <paramref name="text"/> in a strong way.
        /// </summary>
        /// <param name="text">[in] Text to be printed in a strong way.</param>
        /// <param name="intentionLevel">[in, optional] The number of levels that the output is being intended by tabs. (Default: 0)</param>
        public static void WriteStrongLine(string text, int intentionLevel = 0)
        {
            Console.ForegroundColor = StrongForegroundColor;
            Console.BackgroundColor = StrongBackgroundColor;
            Console.WriteLine($"{string.Empty.PadRight(intentionLevel, '\t')}{text}");
            Console.ForegroundColor = defaultForegroundColor;
            Console.BackgroundColor = defaultBackgroundColor;
        }

        /// <summary>
        /// Prints the passed in <paramref name="text"/> in the specified color.
        /// </summary>
        /// <param name="text">[in] Text to be printed in the specified color.</param>
        /// <param name="foregroundColor">[in] Foreground color to print the text with.</param>
        /// <param name="backgroundColor">[in, optional] Background color to print the text with. (Default: Black)</param>
        /// <param name="intentionLevel">[in, optional] The number of levels that the output is being intended by tabs. (Default: 0)</param>
        public static void WriteColoredLine(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black, int intentionLevel = 0)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine($"{string.Empty.PadRight(intentionLevel, '\t')}{text}");
            Console.ForegroundColor = defaultForegroundColor;
            Console.BackgroundColor = defaultBackgroundColor;
        }

        /// <summary>
        /// Prints the passed in <paramref name="label"/> in highlight color followed by <paramref name="text"/>.
        /// </summary>
        /// <param name="label">[in] The label to be printed in highlight color.</param>
        /// <param name="text">[in] The text to be printed in default color.</param>
        /// <param name="intentionLevel">[in, optional] The number of levels that the output is being intended by tabs. (Default: 1)</param>
        public static void WriteHighlightedLabelTextLine(string label, string text, int intentionLevel = 1)
        {
            Console.ForegroundColor = HighlightForegroundColor;
            Console.BackgroundColor = HighlightBackgroundColor;
            Console.Write($"{string.Empty.PadRight(intentionLevel, '\t')}{label}: ");
            Console.ForegroundColor = defaultForegroundColor;
            Console.BackgroundColor = defaultBackgroundColor;

            Console.WriteLine(text);
        }
    }
}
