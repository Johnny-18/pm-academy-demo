using System;
using Moq;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;
using Xunit;

namespace NotesTests
{
    public class NotesServiceTests
    {
        [Fact]
        public void AddNote_MustThrow_ArgumentNullException_If_NoteIsNull()
        {
            var service = new NotesService(null, null);
            Note note = null;

            Assert.Throws<ArgumentNullException>(() => service.AddNote(note, 1));
         }

        [Fact]
        public void AddNote_()
        {
            var storageMoq = new Mock<INotesStorage>();
            
        }
    }
}