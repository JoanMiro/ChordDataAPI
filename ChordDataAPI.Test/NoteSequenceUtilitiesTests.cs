﻿namespace ChordDataAPI.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Models;

    using System;
    using System.Linq;

    [TestClass]
    public class NoteSequenceUtilitiesTests
    {
        private ChordData chordData;

        [TestInitialize]
        public void Setup()
        {
            this.chordData = new ChordData();
        }

        [TestMethod, TestCategory("NoteSequenceUtilities")]
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

        [TestMethod, TestCategory("NoteSequenceUtilities")]
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

        [TestMethod, TestCategory("NoteSequenceUtilities")]
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

        [TestMethod, TestCategory("NoteSequenceUtilities")]
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

        [TestMethod, TestCategory("NoteSequenceUtilities")]
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

        [TestMethod, TestCategory("NoteSequenceUtilities")]
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

        [TestMethod, TestCategory("NoteSequenceUtilities")]
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

        [TestMethod, TestCategory("NoteSequenceUtilities")]
        public void WhenSearchDescriptionIsCalledForChordsWithVariantSpelling_ThenAllMatchingChordsAreReturned()
        {
            // Arrange
            var searchTerm = "eenth";
            var expectedDescriptions = new[]
            {
                "13th",
                "Minor 13th",
                "Major 13th",
                "Minor + Major 13th",
                "7th+13th",
                "Minor 7th+13th",
                "Major 7th+13th",
                "Minor + Major 7th+13th",
                "9th Flattened 13th"
            };

            // Act
            var results = NoteSequenceUtilities.SearchDescriptions(this.chordData.Chords, searchTerm);

            // Assert
            Assert.IsNotNull(results);
            var resultsArray = results as Chord[] ?? results.ToArray();
            Assert.AreEqual(9, resultsArray.Length);
            Assert.AreEqual(0, expectedDescriptions.Except(resultsArray.Select(r => r.Description)).Count());
        }

        [TestMethod, TestCategory("NoteSequenceUtilities")]
        public void WhenSearchDescriptionIsCalledForChordsWithVariantSpellingMultipleMatchingHits_ThenAllMatchingChordsAreReturned()
        {
            // Arrange
            var searchTerm = "even";
            var expectedDescriptions = new[]
            {
                "7th",
                "Major 7th",
                "Minor 7th",
                "Diminished 7th",
                "7th Flattened 5th",
                "7th Sharpened 5th",
                "7th Flattened 9th",
                "7th Sharpened 9th",
                "Major 7th+9th",
                "11th",
                "Augmented 11th",
                "Minor + Major 7th",
                "Minor 11th",
                "Major 11th",
                "Minor + Major 11th",
                "7th+11th",
                "Minor 7th+11th",
                "Major 7th+11th",
                "Minor + Major 7th+11th",
                "7th+13th",
                "Minor 7th+13th",
                "Major 7th+13th",
                "Minor + Major 7th+13th",
                "7th Sharpened 5th Flattened 9th",
                "Minor 7th Flattened 5th",
                "Minor 7th Sharpened 5th",
                "Minor 7th Flattened 9th",
                "7th Suspended 4th",
                "Major 7th Suspended 4th"
            };

            // Act
            var results = NoteSequenceUtilities.SearchDescriptions(this.chordData.Chords, searchTerm);

            // Assert
            Assert.IsNotNull(results);
            var resultsArray = results as Chord[] ?? results.ToArray();
            Assert.AreEqual(29, resultsArray.Length);
            Assert.AreEqual(0, expectedDescriptions.Except(resultsArray.Select(r => r.Description)).Count());
        }

        
        [TestMethod, TestCategory("NoteSequenceUtilities")]
        public void WhenSearchDescriptionIsCalledForChordsWithMultipleTermsWithVariantSpelling_ThenAllMatchingChordsAreReturned()
        {
            // Arrange
            var searchTerm = "eenth maj";
            var expectedDescriptions = new[]
            {
                "Major 13th",
                "Minor + Major 13th",
                "Major 7th+13th",
                "Minor + Major 7th+13th"
            };

            // Act
            var results = NoteSequenceUtilities.SearchDescriptions(this.chordData.Chords, searchTerm);

            // Assert
            Assert.IsNotNull(results);
            var resultsArray = results as Chord[] ?? results.ToArray();
            Assert.AreEqual(4, resultsArray.Length);
            Assert.AreEqual(0, expectedDescriptions.Except(resultsArray.Select(r => r.Description)).Count());
        }

        [TestMethod, TestCategory("NoteSequenceUtilities")]
        public void WhenSearchDescriptionIsCalledForChordsWithMultipleTermsAndVariantSpelling_ThenAllMatchingChordsAreReturned()
        {
            // Arrange
            var searchTerm = "eenth maj min";
            var expectedDescriptions = new[]
            {
                "Minor + Major 13th",
                "Minor + Major 7th+13th"
            };

            // Act
            var results = NoteSequenceUtilities.SearchDescriptions(this.chordData.Chords, searchTerm);

            // Assert
            Assert.IsNotNull(results);
            var resultsArray = results as Chord[] ?? results.ToArray();
            Assert.AreEqual(2, resultsArray.Length);
            Assert.AreEqual(0, expectedDescriptions.Except(resultsArray.Select(r => r.Description)).Count());
        }
    }
}
