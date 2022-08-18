using System;
using System.Collections.Generic;
using System.Linq;
using diamond_kata;
using FsCheck;

namespace diamon_kata.Tests
{
    public class DiamondPropertyTests
    {
        private static char CurrentLetter(string x) => x.First(x => x != ' ');

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property NonEmpty(char c)
        {
            return Diamond.Generate(c).All(s => s != string.Empty).ToProperty();
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property StartsWithA(char c)
        {
            return Diamond.Generate(c).First().Contains('A').ToProperty();
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property EndsWithA(char c)
        {
            return Diamond.Generate(c).Last().Contains('A').ToProperty();
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property AllRowsAreEqualLength(char c)
        {
            var diamond = Diamond.Generate(c);
            var fstRowLength = diamond[0].Length;

            return diamond.All(x => x.Length == fstRowLength).ToProperty();
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property WidthEqualsHeight(char c)
        {
            var diamond = Diamond.Generate(c);
            var fstRowLength = diamond[0].Length;

            return (diamond.Length == fstRowLength).ToProperty();
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property LeadingAndTrailingSpacesAreSymmetrical(char c)
        {
            bool SpacesAreSymmetric(string x)
            {
                var currentLetter = CurrentLetter(x);
                var leadingSpaces = x.IndexOf(currentLetter);
                var trailingSpaces = x.Length - x.LastIndexOf(currentLetter) - 1;
                return leadingSpaces == trailingSpaces;
            }

            return Diamond.Generate(c).All(SpacesAreSymmetric).ToProperty();
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property InsideSpacesAreAlwaysOdd(char c)
        {
            bool SpacesAreOdd(string x)
            {
                var currentLetter = CurrentLetter(x);
                var middleSpaces = x.LastIndexOf(currentLetter) - x.IndexOf(currentLetter) -1;
                return Math.Abs(middleSpaces) % 2 == 1;
            }

            return Diamond.Generate(c).All(SpacesAreOdd).ToProperty();
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property AllRowsContainOneLetterTwiceExceptFirstAndLast(char c)
        {
            bool ContainsOneLetterTwice(string x)
            {
                var currentLetter = CurrentLetter(x);
                var currentLetters = x.Count(x => x == currentLetter);
                var anyOtherLetters = x.Count(x => x != currentLetter && x != ' ');
                return currentLetters == 2 && anyOtherLetters == 0;
            }

            return Diamond.Generate(c).Skip(1).SkipLast(1).All(ContainsOneLetterTwice).ToProperty();
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property MiddleRowContainsMyLetterWithNoSpaces(char c)
        {
            var diamond = Diamond.Generate(c);
            var middleRowIndex = diamond[0].Length / 2;
            var middleRow = diamond[middleRowIndex];

            return middleRow.StartsWith(c).And(middleRow.EndsWith(c));
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property EachRowContainsTheExpectedLetter(char c)
        {
            var expected = new List<char>();
            for (var i = 'A'; i < c; i++) expected.Add(i);
            for (var i = c; i >= 'A'; i--) expected.Add(i);

            var actual = Diamond.Generate(c).Select(CurrentLetter);

            return expected.SequenceEqual(actual).ToProperty();
        }

        [FsCheck.Xunit.Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property DiamondIsSymmetricAroundMiddleRow(char c)
        {
            var diamond = Diamond.Generate(c);
            var middleRowIndex = diamond[0].Length / 2;
            var upToMiddleRows = diamond[..middleRowIndex];
            var fromMiddleRows = diamond[(middleRowIndex+1)..];

            return upToMiddleRows.Reverse().SequenceEqual(fromMiddleRows).ToProperty();
        }
    }
}