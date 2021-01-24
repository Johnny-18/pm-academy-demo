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
        private readonly Mock<INotesStorage> _storageMoq;
        private readonly Mock<INoteEvents> _eventMoq;
        private readonly Note _testNote;
        private readonly int _testUserId;
        
        public NotesServiceTests()
        {
            _storageMoq = new Mock<INotesStorage>();
            _eventMoq = new Mock<INoteEvents>();
            _testNote = new Note()
            {
                Id = Guid.NewGuid(),
                Header = "header",
                Body = "body"
            };
            _testUserId = 1;
        }
        
        [Fact]
        public void AddNote_MustThrow_ArgumentNullException_If_NoteIsNull()
        {
            var service = new NotesService(_storageMoq.Object, _eventMoq.Object);

            Assert.Throws<ArgumentNullException>(() => service.AddNote(null, _testUserId));
         }

        [Fact]
        public void AddNote_If_Arguments_IsCorrect_NoteEvent_MustCall_NotifyAdded()
        {
            var service = new NotesService(_storageMoq.Object, _eventMoq.Object);
            
            service.AddNote(_testNote, _testUserId);
            
            _eventMoq.Verify(x => x.NotifyAdded(_testNote, _testUserId), Times.Once);
        }

        [Fact]
        public void AddNote_If_NotesStorage_ThrowException_NoteEvent_DontCall_NotifyAdded()
        {
            _storageMoq.Setup(x => x.AddNote(_testNote, _testUserId)).Throws<ArgumentException>();

            var service = new NotesService(_storageMoq.Object, _eventMoq.Object);
            try
            {
                service.AddNote(_testNote, _testUserId);
            }
            catch
            {
                _eventMoq.Verify(x => x.NotifyAdded(_testNote, _testUserId), Times.Never);
            }
        }

        [Fact]
        public void DeleteNote_If_NotesStorage_DeletedNote_NoteEvent_Call_NotifyDeletedOnce()
        {
            _storageMoq.Setup(x => x.DeleteNote(_testNote.Id)).Returns(true);

            var service = new NotesService(_storageMoq.Object, _eventMoq.Object);
            
            service.DeleteNote(_testNote.Id, _testUserId);
            
            _eventMoq.Verify(x => x.NotifyDeleted(_testNote.Id, _testUserId), Times.Once);
        }

        [Fact]
        public void DeleteNote_If_NotesStorage_DontDeletedNote_NoteEvent_Dont_Call_NotifyDeleted()
        {
            _storageMoq.Setup(x => x.DeleteNote(_testNote.Id)).Returns(false);

            var service = new NotesService(_storageMoq.Object, _eventMoq.Object);
            
            service.DeleteNote(_testNote.Id, _testUserId);
            
            _eventMoq.Verify(x => x.NotifyDeleted(_testNote.Id, _testUserId), Times.Never);
        }
    }
}