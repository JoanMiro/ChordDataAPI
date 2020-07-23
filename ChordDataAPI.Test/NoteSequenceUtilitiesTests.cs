namespace ChordDataAPI.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Models;

    [TestClass]
    public class NoteSequenceUtilitiesTests
    {
        private ChordData chordData;

        [TestInitialize]
        public void Setup()
        {
            this.chordData = new ChordData();
        }

        [TestMethod]
        public void WhenGetNotesIsCalledForCMajorTriad_ThenCEGNoteNamesIsReturned()
        {
            // Arrange
            var sourceChord = this.chordData.Chords[0];
            var expectedNotes = new[] { "C", "E", "G" };

            // Act
            var notes = NoteSequenceUtilities.GetNotes(this.chordData, "c", sourceChord);

            // Assert
            Assert.IsNotNull(notes);

            var notesArray = notes as string[] ?? notes.ToArray();

            Assert.AreEqual(3, notesArray.Length);

            for (var noteIndex = 0; noteIndex < 3; noteIndex++)
            {
                Assert.AreEqual(expectedNotes[noteIndex], notesArray[noteIndex]);
            }
        }

        [TestMethod]
        public void WhenGetNotesIsCalledForBbASharpMajorTriad_ThenBbDFNoteNamesIsReturned()
        {
            // Arrange
            var sourceChord = this.chordData.Chords[0];
            var expectedNotes = new[] { "Bb/A#", "D", "F" };

            // Act
            var notes = NoteSequenceUtilities.GetNotes(this.chordData, "Bb", sourceChord);

            // Assert
            Assert.IsNotNull(notes);

            var notesArray = notes as string[] ?? notes.ToArray();

            Assert.AreEqual(3, notesArray.Length);

            for (var noteIndex = 0; noteIndex < 3; noteIndex++)
            {
                Assert.AreEqual(expectedNotes[noteIndex], notesArray[noteIndex]);
            }
        }

        [TestMethod]
        public void WhenGetNotesIsCalledForBbASharpMinorTriad_ThenBbDbFNoteNamesIsReturned()
        {
            // Arrange
            var sourceChord = this.chordData.Chords[1];
            var expectedNotes = new[] { "Bb/A#", "C#/Db", "F" };

            // Act
            var notes = NoteSequenceUtilities.GetNotes(this.chordData, "Bb", sourceChord);

            // Assert
            Assert.IsNotNull(notes);

            var notesArray = notes as string[] ?? notes.ToArray();

            Assert.AreEqual(3, notesArray.Length);

            for (var noteIndex = 0; noteIndex < 3; noteIndex++)
            {
                Assert.AreEqual(expectedNotes[noteIndex], notesArray[noteIndex]);
            }
        }

        [TestMethod]
        public void WhenGetNotesIsCalledForBbASharpMajorSixthTetrad_ThenBbDbFNoteNamesIsReturned()
        {
            // Arrange
            var sourceChord = this.chordData.Chords[6];
            var expectedNotes = new[] { "Bb/A#", "D", "F", "G" };

            // Act
            var notes = NoteSequenceUtilities.GetNotes(this.chordData, "Bb", sourceChord);

            // Assert
            Assert.IsNotNull(notes);

            var notesArray = notes as string[] ?? notes.ToArray();

            Assert.AreEqual(4, notesArray.Length);

            for (var noteIndex = 0; noteIndex < 4; noteIndex++)
            {
                Assert.AreEqual(expectedNotes[noteIndex], notesArray[noteIndex]);
            }
        }

        [TestMethod]
        public void WhenGetNotesIsCalledForBbASharpMinorSixthTetrad_ThenBbDbFNoteNamesIsReturned()
        {
            // Arrange
            var sourceChord = this.chordData.Chords[7];
            var expectedNotes = new[] { "Bb/A#", "C#/Db", "F", "G" };

            // Act
            var notes = NoteSequenceUtilities.GetNotes(this.chordData, "Bb", sourceChord);

            // Assert
            Assert.IsNotNull(notes);

            var notesArray = notes as string[] ?? notes.ToArray();

            Assert.AreEqual(4, notesArray.Length);

            for (var noteIndex = 0; noteIndex < 4; noteIndex++)
            {
                Assert.AreEqual(expectedNotes[noteIndex], notesArray[noteIndex]);
            }
        }

        [TestMethod]
        public void WhenSearchDescriptionIsCalledForChordsWithSingleTerm_ThenAllMatchingChordsAreReturned()
        {
            // Arrange
            var searchTerm = "maj";

            // Act
            var results = NoteSequenceUtilities.SearchDescriptions(this.chordData.Chords, searchTerm);

            // Assert
            Assert.IsNotNull(results);
            var resultsArray = results as Chord[] ?? results.ToArray();
            Assert.AreEqual(16, resultsArray.Length);
            Assert.IsTrue(resultsArray.All(result => result.Description.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)));
        }

        [TestMethod]
        public void WhenSearchDescriptionIsCalledForChordsWithMultipleTerms_ThenAllMatchingChordsAreReturned()
        {
            // Arrange
            var searchTerm = "maj sus";
            var searchTerms = searchTerm.Split();

            // Act
            var results = NoteSequenceUtilities.SearchDescriptions(this.chordData.Chords, searchTerm);

            // Assert
            Assert.IsNotNull(results);
            var resultsArray = results as Chord[] ?? results.ToArray();
            Assert.AreEqual(2, resultsArray.Length);
            foreach (var term in searchTerms)
            {
                Assert.IsTrue(resultsArray.All(result => result.Description.Contains(term, StringComparison.InvariantCultureIgnoreCase)));
            }
        }
    }
}
