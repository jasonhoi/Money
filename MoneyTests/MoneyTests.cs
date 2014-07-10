using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MoneyTests
{
    [TestMethod]
    public void CurrencyIsEqual()
    {
        Currency cur1 = new Currency("BTC");
        Currency cur2 = new Currency("BTC");
        Assert.AreEqual<Currency>(cur1, cur2);
    }

    [TestMethod]
    public void CurrencyIsNotEqual()
    {
        Currency cur1 = new Currency("HKD");
        Currency cur2 = new Currency("BTC");
        Assert.AreNotEqual<Currency>(cur1, cur2);
    }

    [TestMethod]
    public void MoneyCurrencyIsNotEqual()
    {
        Money m1 = new Money(1m, "HKD");
        Money m2 = new Money(1m, "MOP");
        Assert.AreNotEqual<Money>(m1, m2);
    }

    [TestMethod]
    public void MoneyIsNotEqualAmount()
    {
        Money m1 = new Money(1m, "HKD");
        Money m2 = new Money(2m, "HKD");
        Assert.AreNotEqual<Money>(m1, m2);
    }

    [TestMethod]
    public void MoneyIsEqual()
    {
        Money m1 = new Money(1.00000001m, "BTC");
        Money m2 = new Money(1.00000001m, "BTC");
        Assert.AreEqual<Money>(m1, m2);
    }

    [TestMethod]
    public void MoneyAddMoney()
    {
        Money m1 = new Money(1.00000001m, "BTC");
        Money m2 = new Money(10.000000019m, "BTC");

        Money result = m1 + m2;
        Assert.AreEqual<Money>(result, new Money(11.000000029m, "BTC"));
    }

    [TestMethod]
    public void MoneyAddDecimal()
    {
        Money m1 = new Money(1.00000001m, "BTC");

        Money result = m1 + 10.000000019m;
        if (result.Amount != 11.000000029m)
            Assert.Fail();
    }

    [TestMethod]
    public void MoneySubtractMoney()
    {
        Money m1 = new Money(1.00000001m, "MOP");
        Money m2 = new Money(10.000000019m, "MOP");

        Money result = m2 - m1;
        Assert.AreEqual<Money>(result, new Money(9.000000009m, "MOP"));
    }

    [TestMethod]
    public void MoneySubtractDecimal()
    {
        Money m1 = new Money(1.00000001m, "BTC");

        Money result = m1 - 10.000000019m;
        if (result.Amount != -9.000000009m)
            Assert.Fail();
    }

    [TestMethod]
    public void MoneyMultiplyDecimal()
    {
        Money m1 = new Money(1.02m, "MOP");

        Money result = m1 * 2.5m;
        Assert.AreEqual<Money>(result, new Money(2.55m, "MOP"));
    }

    [TestMethod]
    public void MoneyMultiplyInt()
    {
        Money m1 = new Money(1.000000014m, "BTC");

        Money result = m1 * 2;
        if (result.Amount != 2.000000028m)
            Assert.Fail();
    }

    [TestMethod]
    public void MoneyMultiplySmallDecimal()
    {
        Money m1 = new Money(1.000000005m, "BTC");

        Money result = m1 * (5m/1000m);
        Assert.AreEqual<Money>(result, new Money(0.005000000025m, "BTC"));
    }

    [TestMethod]
    public void MoneyDividedByInt()
    {
        Money m1 = new Money(2.5m, "BTC");

        Money result = m1 / 2;
        Assert.AreEqual<Money>(result, new Money(1.25m, "BTC"));
    }
}