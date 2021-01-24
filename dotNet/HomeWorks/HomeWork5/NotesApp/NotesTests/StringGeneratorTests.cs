using System;
using NotesApp.Tools;
using Xunit;

namespace NotesTests
{
    public class StringGeneratorTests
    {
        [Theory]
        [InlineData(0, true)]
        [InlineData(0, false)]
        public void GenerateNumbersString_Return_EmptyString_If_Parameter_Length_Is_0(int length, bool allowLeadingZero)
        {
            var expected = String.Empty;

            Assert.Equal(expected, StringGenerator.GenerateNumbersString(length, allowLeadingZero));
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(-1, true)]
        public void GenerateNumbersString_Throws_ArgumentOutOfRangeException_If_Argument_Invalid(int length, bool allowLeadingZero)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                StringGenerator.GenerateNumbersString(length, allowLeadingZero));
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(2, true)]
        [InlineData(5, true)]
        public void GenerateNumbersString_Generate_String_Without_ZeroInStart_If_AllowLeadingZero_Is_True(int length, bool allowLeadingZero)
        {
            var expected = '0';
            var actual = StringGenerator.GenerateNumbersString(length, allowLeadingZero);
            
            Assert.NotEqual(expected, actual[0]);
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(1, true)]
        [InlineData(200, false)]
        [InlineData(5000, false)]
        [InlineData(50, true)]
        public void GenerateNumbersString_Generate_String_And_StringLength_Equal_To_LengthFromArgument(int length, bool allowLeadingZero)
        {
            var actual = StringGenerator.GenerateNumbersString(length, allowLeadingZero).Length;
            
            Assert.Equal(length, actual);
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(1, true)]
        [InlineData(18, false)]
        public void GenerateNumbersString_ReturnsString_Should_Convert_To_Long(int length, bool allowLeadingZero)
        {
            var generatedString = StringGenerator.GenerateNumbersString(length, allowLeadingZero);

            var expected = true;
            var actual = long.TryParse(generatedString, out var value);
            
            Assert.Equal(expected, actual);
        }
    }
}