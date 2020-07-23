namespace ChordDataAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;

    using ChordDataAPI.Models;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Models;

    [ApiController]
    [Route("[controller]")]
    public class ChordDataController : ControllerBase
    {
        private readonly IChordData chordData;
        private readonly ILogger<ChordDataController> logger;

        public ChordDataController(IChordData chordData, ILogger<ChordDataController> logger)
        {
            this.chordData = chordData;
            this.logger = logger;
        }


        [HttpGet]
        public ChordData ChordData()
        {
            return (ChordData)this.chordData;
        }


        [HttpGet("Chords")]
        public IEnumerable<Chord> Chords()
        {
            return this.chordData.Chords;
        }

        [HttpGet("Chords/{searchTerm}")]
        public IEnumerable<Chord> Chords(string searchTerm)
        {
            return this.Chords().Where(chord => chord.Description.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
        }

        [HttpGet("ChordNotes/{root}/{searchTerm}")]
        public IEnumerable<Chord> ChordNotes(string root, string searchTerm)
        {
            var chordResults = NoteSequenceUtilities.SearchDescriptions(this.chordData.Chords, searchTerm);
            var chordArray = chordResults as Chord[] ?? chordResults.ToArray();
            var chordInstances = new List<ChordInstance>(chordArray.Length);

            foreach (var chord in chordArray)
            {
                var chordInstance = ChordInstance.Create(
                    chord.Description,
                    chord.Notes,
                    (List<string>)NoteSequenceUtilities.GetNotes(this.chordData, root, chord));

                chordInstances.Add(chordInstance);
            }

            return chordInstances;
        }

        [HttpGet("Scales")]
        public IEnumerable<Scale> Scales()
        {
            return this.chordData.Scales;
        }

        [HttpGet("Scales/{searchTerm}")]
        public IEnumerable<Scale> Scales(string searchTerm)
        {
            return this.chordData.Scales.Where(scale => scale.Description.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
        }

        [HttpGet("ScaleNotes/{root}/{searchTerm}")]
        public IEnumerable<Scale> ScaleNotes(string root, string searchTerm)
        {
            var scaleResults = NoteSequenceUtilities.SearchDescriptions(this.chordData.Scales, searchTerm);
            var scaleArray = scaleResults as Scale[] ?? scaleResults.ToArray();
            var scaleInstances = new List<ScaleInstance>(scaleArray.Length);

            foreach (var scale in scaleArray)
            {
                var scaleInstance = ScaleInstance.Create(
                    scale.Description,
                    scale.Notes,
                    (List<string>)NoteSequenceUtilities.GetNotes(this.chordData, root, scale));

                scaleInstances.Add(scaleInstance);
            }

            return scaleInstances;
        }
    }
}