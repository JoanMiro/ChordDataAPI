namespace ChordDataAPI.Test
{
    using System;
    using System.Linq;

    using Controllers;

    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Models;

    using Moq;

    [TestClass]
    public class ControllerTests
    {
        private ChordData chordData;
        private Mock<ILogger<ChordDataController>> loggerMock;

        [TestInitialize]
        public void Setup()
        {
            this.chordData = new ChordData();
            this.loggerMock = new Mock<ILogger<ChordDataController>>();
        }

        [TestMethod, TestCategory("Controller")]
        public void WhenChordDataMethodIsCalled_ThenAllDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordDataObject = controller.ChordData();

            // Assert
            Assert.IsNotNull(chordDataObject);
            Assert.AreEqual(53, chordDataObject.Chords.Count);
            Assert.AreEqual(36, chordDataObject.Scales.Count);
            Assert.AreEqual(12, chordDataObject.NoteNames.Count);
        }
        
        // ------
        // Chords
        // ------
        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordsMethodIsCalled_ThenAllChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.Chords();

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as Chord[] ?? chordsObject.ToArray();
            Assert.AreEqual(53, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(Chord));
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordsMethodIsCalledWithSingleSearchTerm_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.Chords("maj");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as Chord[] ?? chordsObject.ToArray();
            Assert.AreEqual(16, chordsArray.Length);
            Assert.AreEqual("Major", chordsArray.First().Description);
            Assert.AreEqual("Major 9th Suspended 4th", chordsArray.Last().Description);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordsMethodIsCalledWithMultipleSearchTerms_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.Chords("maj 7th");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as Chord[] ?? chordsObject.ToArray();
            Assert.AreEqual(8, chordsArray.Length);
            Assert.AreEqual("Major 7th", chordsArray.First().Description);
            Assert.AreEqual("Major 7th Suspended 4th", chordsArray.Last().Description);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordNotesMethodIsCalledWithRootArgument_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.ChordNotes("C");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as ChordInstance[] ?? chordsObject.ToArray();
            Assert.AreEqual(16, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(ChordInstance));
            Assert.AreEqual("C Major", chordsArray.First().DisplayName);
            Assert.AreEqual("C Major 9th Suspended 4th", chordsArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordNotesMethodIsCalledWithRootArgumentContainingFlat_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.ChordNotes("Ab");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as ChordInstance[] ?? chordsObject.ToArray();
            Assert.AreEqual(16, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(ChordInstance));
            Assert.AreEqual("G#/Ab Major", chordsArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Major 9th Suspended 4th", chordsArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordNotesMethodIsCalledWithRootArgumentContainingSharp_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.ChordNotes("G#");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as ChordInstance[] ?? chordsObject.ToArray();
            Assert.AreEqual(16, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(ChordInstance));
            Assert.AreEqual("G#/Ab Major", chordsArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Major 9th Suspended 4th", chordsArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordNotesMethodIsCalledWithRootArgumentAndSingleSearchTerm_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.ChordNotes("C", "Min");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as ChordInstance[] ?? chordsObject.ToArray();
            Assert.AreEqual(21, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(ChordInstance));
            Assert.AreEqual("C Minor", chordsArray.First().DisplayName);
            Assert.AreEqual("C Minor 7th Flattened 9th", chordsArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordNotesMethodIsCalledWithRootArgumentContainingFlatAndSingleSearchTerm_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.ChordNotes("Ab", "Min");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as ChordInstance[] ?? chordsObject.ToArray();
            Assert.AreEqual(21, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(ChordInstance));
            Assert.AreEqual("G#/Ab Minor", chordsArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Minor 7th Flattened 9th", chordsArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordNotesMethodIsCalledWithRootArgumentContainingSharpAndSingleSearchTerm_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.ChordNotes("G#", "Min");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as ChordInstance[] ?? chordsObject.ToArray();
            Assert.AreEqual(21, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(ChordInstance));
            Assert.AreEqual("G#/Ab Minor", chordsArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Minor 7th Flattened 9th", chordsArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordNotesMethodIsCalledWithRootArgumentAndMultipleSearchTerms_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.ChordNotes("C", "Min 9th");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as ChordInstance[] ?? chordsObject.ToArray();
            Assert.AreEqual(5, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(ChordInstance));
            Assert.AreEqual("C Minor 9th", chordsArray.First().DisplayName);
            Assert.AreEqual("C Minor 7th Flattened 9th", chordsArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordNotesMethodIsCalledWithRootArgumentContainingFlatAndMultipleSearchTerms_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.ChordNotes("Ab", "Min 9th");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as ChordInstance[] ?? chordsObject.ToArray();
            Assert.AreEqual(5, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(ChordInstance));
            Assert.AreEqual("G#/Ab Minor 9th", chordsArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Minor 7th Flattened 9th", chordsArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Chords"), TestCategory("Controller")]
        public void WhenChordNotesMethodIsCalledWithRootArgumentContainingSharpAndMultipleSearchTerms_ThenMatchingChordDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var chordsObject = controller.ChordNotes("G#", "Min 9th");

            // Assert
            Assert.IsNotNull(chordsObject);
            var chordsArray = chordsObject as ChordInstance[] ?? chordsObject.ToArray();
            Assert.AreEqual(5, chordsArray.Length);
            Assert.IsInstanceOfType(chordsArray.First(), typeof(ChordInstance));
            Assert.AreEqual("G#/Ab Minor 9th", chordsArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Minor 7th Flattened 9th", chordsArray.Last().DisplayName);

        }

        // ------
        // Scales
        // ------
        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScalesMethodIsCalled_ThenAllScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.Scales();

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as Scale[] ?? scalesObject.ToArray();
            Assert.AreEqual(36, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(Scale));
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScalesMethodIsCalledWithSingleSearchTerm_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.Scales("maj");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as Scale[] ?? scalesObject.ToArray();

            Assert.AreEqual(2, scalesArray.Length);
            Assert.AreEqual("Major", scalesArray.First().Description);
            Assert.AreEqual("Pentatonic Major", scalesArray.Last().Description);
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScalesMethodIsCalledWithMultipleSearchTerms_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.Scales("lyd flat");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as Scale[] ?? scalesObject.ToArray();
            Assert.AreEqual(3, scalesArray.Length);
            Assert.AreEqual("Mixolydian Flattened 9th Sharpened 9th", scalesArray.First().Description);
            Assert.AreEqual("Lydian Flattened 7th", scalesArray.Last().Description);
        }
        
        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScaleNotesMethodIsCalledWithRootArgument_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.ScaleNotes("C");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as ScaleInstance[] ?? scalesObject.ToArray();
            Assert.AreEqual(2, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(ScaleInstance));
            Assert.AreEqual("C Major", scalesArray.First().DisplayName);
            Assert.AreEqual("C Pentatonic Major", scalesArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScaleNotesMethodIsCalledWithRootArgumentContainingFlat_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.ScaleNotes("Ab");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as ScaleInstance[] ?? scalesObject.ToArray();
            Assert.AreEqual(2, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(ScaleInstance));
            Assert.AreEqual("G#/Ab Major", scalesArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Pentatonic Major", scalesArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScaleNotesMethodIsCalledWithRootArgumentContainingSharp_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.ScaleNotes("G#");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as ScaleInstance[] ?? scalesObject.ToArray();
            Assert.AreEqual(2, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(ScaleInstance));
            Assert.AreEqual("G#/Ab Major", scalesArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Pentatonic Major", scalesArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScaleNotesMethodIsCalledWithRootArgumentAndSingleSearchTerm_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.ScaleNotes("C", "Min");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as ScaleInstance[] ?? scalesObject.ToArray();
            Assert.AreEqual(8, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(ScaleInstance));
            Assert.AreEqual("C Minor (Natural)", scalesArray.First().DisplayName);
            Assert.AreEqual("C Diminished Whole", scalesArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScaleNotesMethodIsCalledWithRootArgumentContainingFlatAndSingleSearchTerm_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.ScaleNotes("Ab", "Min");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as ScaleInstance[] ?? scalesObject.ToArray();
            Assert.AreEqual(8, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(ScaleInstance));
            Assert.AreEqual("G#/Ab Minor (Natural)", scalesArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Diminished Whole", scalesArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScaleNotesMethodIsCalledWithRootArgumentContainingSharpAndSingleSearchTerm_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.ScaleNotes("G#", "Min");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as ScaleInstance[] ?? scalesObject.ToArray();
            Assert.AreEqual(8, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(ScaleInstance));
            Assert.AreEqual("G#/Ab Minor (Natural)", scalesArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Diminished Whole", scalesArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScaleNotesMethodIsCalledWithRootArgumentAndMultipleSearchTerms_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.ScaleNotes("C", "lyd flat");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as ScaleInstance[] ?? scalesObject.ToArray();
            Assert.AreEqual(3, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(ScaleInstance));
            Assert.AreEqual("C Mixolydian Flattened 9th Sharpened 9th", scalesArray.First().DisplayName);
            Assert.AreEqual("C Lydian Flattened 7th", scalesArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScaleNotesMethodIsCalledWithRootArgumentContainingFlatAndMultipleSearchTerms_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.ScaleNotes("Ab", "lyd flat");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as ScaleInstance[] ?? scalesObject.ToArray();
            Assert.AreEqual(3, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(ScaleInstance));
            Assert.AreEqual("G#/Ab Mixolydian Flattened 9th Sharpened 9th", scalesArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Lydian Flattened 7th", scalesArray.Last().DisplayName);
        }

        [TestMethod, TestCategory("Scales"), TestCategory("Controller")]
        public void WhenScaleNotesMethodIsCalledWithRootArgumentContainingSharpAndMultipleSearchTerms_ThenMatchingScaleDataIsReturned()
        {
            // Arrange
            var controller = new ChordDataController(this.chordData, this.loggerMock.Object);

            // Act
            var scalesObject = controller.ScaleNotes("G#", "lyd flat");

            // Assert
            Assert.IsNotNull(scalesObject);
            var scalesArray = scalesObject as ScaleInstance[] ?? scalesObject.ToArray();
            Assert.AreEqual(3, scalesArray.Length);
            Assert.IsInstanceOfType(scalesArray.First(), typeof(ScaleInstance));
            Assert.AreEqual("G#/Ab Mixolydian Flattened 9th Sharpened 9th", scalesArray.First().DisplayName);
            Assert.AreEqual("G#/Ab Lydian Flattened 7th", scalesArray.Last().DisplayName);

        }

    }
}
