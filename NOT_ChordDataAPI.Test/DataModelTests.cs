using NUnit.Framework;

namespace ChordDataAPI.Test
{
    using Models;

    public class DataModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenConstructed_SchemaDataIsLoaded()
        {
            // Arrange

            var fred = new string("Fred");

            // Act
            var chordData = new ChordData();

            // Assert
            Assert.IsNotNull(chordData.Chords);
            Assert.IsNotNull(chordData.Scales);
            Assert.AreEqual(154,chordData.Chords);
            Assert.AreEqual(77,chordData.Scales);
        }
    }
}