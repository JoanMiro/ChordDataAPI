namespace ChordDataAPI.Test
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class DataModelTests
    {
        private ChordData chordData;

        [TestInitialize]
        public void Setup()
        {
            this.chordData = new ChordData();
        }

        [TestMethod, TestCategory("Models")]
        public void WhenChordDataIsConstructed_ThenSchemaDataIsLoaded()
        {
            // Arrange
            // Act

            // Assert
            Assert.IsNotNull(this.chordData.Chords);
            Assert.IsNotNull(this.chordData.Scales);
            Assert.IsNotNull(this.chordData.NoteNames);
            Assert.AreEqual(53, this.chordData.Chords.Count);
            Assert.AreEqual(36, this.chordData.Scales.Count);
            Assert.AreEqual(12, this.chordData.NoteNames.Count);
        }

        [TestMethod, TestCategory("Models")]
        public void WhenChordInstanceIsCreated_ThenNotesCountIsCorrect()
        {
            // Arrange
            var sourceChord = this.chordData.Chords[0];
            var noteNames = NoteSequenceUtilities.GetNotes(this.chordData, "bb", sourceChord);

            // Act
            var chordInstance = ChordInstance.Create(sourceChord.Description, sourceChord.Notes, noteNames.ToList());

            // Assert
            Assert.AreEqual(sourceChord.Notes.Count, chordInstance.Notes.Count);
        }

        [TestMethod, TestCategory("Models")]
        public void WhenChordInstanceIsCreated_ThenNoteNamesCountIsCorrect()
        {
            // Arrange
            var sourceChord = this.chordData.Chords[0];
            var noteNames = NoteSequenceUtilities.GetNotes(this.chordData, "bb", sourceChord);

            // Act
            var chordInstance = ChordInstance.Create(sourceChord.Description, sourceChord.Notes, noteNames.ToList());

            // Assert
            Assert.AreEqual(sourceChord.Notes.Count, chordInstance.NoteNames.Count);
        }

        [TestMethod, TestCategory("Models")]
        public void WhenChordInstanceIsCreated_ThenRootNameIsCorrect()
        {
            // Arrange
            var sourceChord = this.chordData.Chords[0];
            var noteNames = NoteSequenceUtilities.GetNotes(this.chordData, "bb", sourceChord);

            // Act
            var chordInstance = ChordInstance.Create(sourceChord.Description, sourceChord.Notes, noteNames.ToList());

            // Assert
            Assert.AreEqual("Bb/A#", chordInstance.RootName);
        }

        [TestMethod, TestCategory("Models")]
        public void WhenChordInstanceIsCreated_ThenDisplayNameIsCorrect()
        {
            // Arrange
            var sourceChord = this.chordData.Chords[0];
            var noteNames = NoteSequenceUtilities.GetNotes(this.chordData, "bb", sourceChord);

            // Act
            var chordInstance = ChordInstance.Create(sourceChord.Description, sourceChord.Notes, noteNames.ToList());

            // Assert
            Assert.AreEqual("Bb/A# Major", chordInstance.DisplayName);
        }

        [TestMethod, TestCategory("Models")]
        public void WhenScaleInstanceIsCreated_ThenNotesCountIsCorrect()
        {
            // Arrange
            var sourceScale = this.chordData.Scales[0];
            var noteNames = NoteSequenceUtilities.GetNotes(this.chordData, "bb", sourceScale);

            // Act
            var scaleInstance = ScaleInstance.Create(sourceScale.Description, sourceScale.Notes, noteNames.ToList());

            // Assert
            Assert.AreEqual(sourceScale.Notes.Count, scaleInstance.Notes.Count);
        }

        [TestMethod, TestCategory("Models")]
        public void WhenScaleInstanceIsCreated_ThenNoteNamesCountIsCorrect()
        {
            // Arrange
            var sourceScale = this.chordData.Scales[0];
            var noteNames = NoteSequenceUtilities.GetNotes(this.chordData, "bb", sourceScale);

            // Act
            var scaleInstance = ScaleInstance.Create(sourceScale.Description, sourceScale.Notes, noteNames.ToList());

            // Assert
            Assert.AreEqual(sourceScale.Notes.Count, scaleInstance.NoteNames.Count);
        }

        [TestMethod, TestCategory("Models")]
        public void WhenScaleInstanceIsCreated_ThenRootNameIsCorrect()
        {
            // Arrange
            var sourceScale = this.chordData.Scales[0];
            var noteNames = NoteSequenceUtilities.GetNotes(this.chordData, "bb", sourceScale);

            // Act
            var scaleInstance = ScaleInstance.Create(sourceScale.Description, sourceScale.Notes, noteNames.ToList());

            // Assert
            Assert.AreEqual("Bb/A#", scaleInstance.RootName);
        }

        [TestMethod, TestCategory("Models")]
        public void WhenScaleInstanceIsCreated_ThenDisplayNameIsCorrect()
        {
            // Arrange
            var sourceScale = this.chordData.Scales[0];
            var noteNames = NoteSequenceUtilities.GetNotes(this.chordData, "bb", sourceScale);

            // Act
            var scaleInstance = ScaleInstance.Create(sourceScale.Description, sourceScale.Notes, noteNames.ToList());

            // Assert
            Assert.AreEqual("Bb/A# Major", scaleInstance.DisplayName);
        }
    }
}