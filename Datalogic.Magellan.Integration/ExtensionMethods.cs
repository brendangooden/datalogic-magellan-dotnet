using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Magellan.Integration
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Check if value is in between the start and end Int value
        /// </summary>
        /// <param name="n"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool BetweenInclusive(this int n, int start, int end) => n >= start && n <= end;

        /// <summary>
        /// Check if value is in between the start and end Int value
        /// </summary>
        /// <param name="n"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool BetweenInclusive(this decimal n, decimal start, decimal end) => n >= start && n <= end;

        /// <summary>
        /// Check if value is not in between the start and end Int value. Allows value to be one of the start/end values.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool NotBetweenInclusive(this int n, int start, int end) => n <= start || n >= end;
        /// <summary>
        /// Check if value is not in between the start and end Int value. Does not allow value to be one of the start/end values.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool NotBetweenExclusive(this int n, int start, int end) => n < start || n > end;

        /// <summary>
        /// Check if <paramref name="input"/> is in the collection <paramref name="sequence"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool In(this int input, params int[] sequence)
        {
            return sequence.Any(x => x == input);
        }
    }
}
