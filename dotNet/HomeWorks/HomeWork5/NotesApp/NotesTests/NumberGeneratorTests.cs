using System;
using NotesApp.Tools;
using Xunit;

namespace NotesTests
{
    public class NumberGeneratorTests
    {
        [Theory]
        [InlineData(19)]
        [InlineData(0)]
        [InlineData(202)]
        [InlineData(-202)]
        public void GeneratePositiveLong_Throws_ArgumentOutOfRangeException_If_Parameter_Invalid(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberGenerator.GeneratePositiveLong(length));
        }

        [Theory]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(8)]
        public void GeneratePositiveLong_Returns_Number_With_LengthFromArgument(int length)
        {
            var expected = length;
            var actual = NumberGenerator.GeneratePositiveLong(length).ToString().Length;
            
            Assert.Equal(expected, actual);
        }
    }
}