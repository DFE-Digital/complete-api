﻿using System.Text.RegularExpressions;

namespace Dfe.Complete.Extensions
{
	public static class StringExtensions
	{
		public static string SplitPascalCase<T>(this T source)
		{
			return source == null ? string.Empty : Regex.Replace(source.ToString(), "[A-Z]", " $0", RegexOptions.None, TimeSpan.FromSeconds(1)).Trim();
		}

		/// <summary>
		///     Converts a string to sentence case.
		/// </summary>
		/// <param name="input">The string to convert.</param>
		/// <returns>A string</returns>
		public static string SentenceCase(this string input)
		{
			if (input.Length < 2)
				return input.ToUpper();

			string sentence = input.ToLower();
			return char.ToUpper(sentence[0]) + sentence[1..];
		}

        public static int ToInt(this string value)
        {
            var succeeded = int.TryParse(value, out var result);

            return succeeded ? result : 0;
        }
    }
}
