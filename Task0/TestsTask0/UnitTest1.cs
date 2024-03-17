using TestTask0;

namespace TestsTask0
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSumCalculation()
        {
            // Arrange
            int number1 = 5;
            int number2 = 7;
            int expectedSum = number1 + number2;

            // Act
            int actualSum = TaskZero.AddNumbers(number1, number2);

            // Assert
            Assert.AreEqual(expectedSum, actualSum);
        }
    }
}