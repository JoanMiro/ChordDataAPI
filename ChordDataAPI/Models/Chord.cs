﻿namespace ChordDataAPI.Models
{
    using System.Collections.Generic;

    public class Chord : NoteSequence
    {
        protected Chord()
        {
        }

        public static Chord CreateNew()
        {
            return new Chord();
        }

        public static Chord Create(string description, List<int> notes)
        {
            return new Chord
            {
                Notes = notes,
                Description = description
            };
        }
    }
}