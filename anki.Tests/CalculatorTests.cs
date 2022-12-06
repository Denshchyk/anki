using FluentAssertions;

namespace anki.Tests;

public class CalculatorTests
{
    private Calculator _calculator;
    
    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator();
    }

    [Test]
    public void Sum_TwoPlusTwo_ReturnFour()
    {
        _calculator.Sum(2, 2).Should().Be(4);
    }
    
    [TestCase(5, 5, 10)]
    [TestCase(1, 2, 3)]
    [TestCase(5, 15, 20)]
    public void Sum_TwoNumbers_ReturnCorrectResult(int a, int b, int result)
    {
        _calculator.Sum(a, b).Should().Be(result);
    }
}

public class Calculator
{
    public int Sum(int a, int b) => a + b;
}