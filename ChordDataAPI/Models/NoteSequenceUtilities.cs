﻿namespace ChordDataAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class NoteSequenceUtilities
    {
        public static IEnumerable<string> GetNotes(IChordData chordData, string root, NoteSequence results)
        {
            var rootNote = chordData.NoteNames.First(name => name.Contains(root.CorrectCase()));

            var rootNoteIndex = chordData.NoteNames.IndexOf(rootNote);
            var notes = new List<string>();

            foreach (var noteNumber in results.Notes)
            {
                notes.Add(chordData.NoteNames[(rootNoteIndex + noteNumber) % chordData.NoteNames.Count]);
            }

            // notes[0] = root.CorrectCase();
            return notes;
        }

        public static IEnumerable<T> SearchDescriptions<T>(List<T> noteSequences, string searchTerm) where T : NoteSequence
        {
            var searchTermList = searchTerm.Split(' ');
            var searchResults = noteSequences.Where(
                noteSequence => searchTermList.All(
                    termText => noteSequence.Description.Contains(termText, StringComparison.InvariantCultureIgnoreCase))).ToArray();
            var results = new T[searchResults.Length];
            Array.Copy(searchResults, results, searchResults.Length);

            return results;
        }

        private static string CorrectCase(this string noteName)
        {
            var upperNoteName = noteName.Substring(0, 1).ToUpper();
            var accidental = noteName.Length > 1 ? noteName.Substring(1, 1) : string.Empty;
            return upperNoteName + accidental;
        }
    }
}