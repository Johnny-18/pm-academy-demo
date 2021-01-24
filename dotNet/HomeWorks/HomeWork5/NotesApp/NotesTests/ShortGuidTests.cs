using System;
using NotesApp.Tools;
using Xunit;

namespace NotesTests
{
    public class ShortGuidTests
    {
        private readonly Guid _guid;

        public ShortGuidTests()
        {
            _guid = Guid.NewGuid();
        }
        
        [Fact]
        public void ToShortId_And_FromShortId_Correct_Convert_From_Guid_To_String_To_GuidBack()
        {
            var shortId = _guid.ToShortId();
            var actual = shortId.FromShortId();
            
            Assert.Equal(_guid, actual);
        }

        [Fact]
        public void ToShortId_And_FromShortId_If_ToResult_Of_ToShortId_AddTwoEquals_FromShortId_ShouldReturn_CorrectGuid() // two equal that means '=='
        {
            var shortId = _guid.ToShortId();
            var actual = (shortId + "==").FromShortId();
            
            Assert.Equal(_guid, actual);
        }

        [Fact]
        public void FromShortId_Returns_CorrectGuid_FromGuid_ConvertedToString()
        {
            var actual = _guid.ToString().FromShortId();
            
            Assert.Equal(_guid, actual);
        }

        [Fact]
        public void FromShortId_Throws_FormatException_If_Argument_InvalidShortGuid()
        {
            var invalidGuid = "936DA01F-9ABD-4d9d-80C7-02AF85C822A";
            Assert.Throws<FormatException>(() => invalidGuid.FromShortId());
        }

        [Fact]
        public void FromShortId_Returns_Null_If_Argument_Is_Null()
        {
            string str = null;
            
            Assert.Null(str.FromShortId());
        }
    }
}