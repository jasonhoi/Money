using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public sealed class Money : IEquatable<Money>, IComparable, IComparable<Money>
{
    public decimal Amount { get; private set; }
    public readonly Currency Currency;

    public Money(decimal amount, Currency currency)
    {
        if (currency == null)
            throw new ArgumentNullException("Currency is not defined in Money object");

        Amount = amount;
        Currency = currency;
    }

    public Money(decimal amount, string isoCode)
    {
        Amount = (decimal)amount;
        Currency = new Currency(isoCode.ToUpper());

        if (Currency == null)
            throw new ArgumentNullException("Currency is not defined in Money object");
    }

    #region Equatable and Operator ==, !=

    public bool Equals(Money other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return (Amount == other.Amount && Currency.Equals(other.Currency));
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(Money)) return false;
        return Equals((Money)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Amount.GetHashCode() * 397) ^ Currency.GetHashCode();
        }
    }

    public static bool operator ==(Money left, Money right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Money left, Money right)
    {
        return !Equals(left, right);
    }

    #endregion

    #region Comparable and Operator >, <, >=, <=

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        if (!(obj is Money)) throw new ArgumentException("Object is not Money object");

        return CompareTo((Money)obj);
    }

    public int CompareTo(Money other)
    {
        if (other == null) return 1;
        if (this < other) return -1;
        if (this > other) return 1;
        return 0;
    }

    public static bool operator >(Money left, Money right)
    {
        AssertSameCurrency(left, right);
        return left.Amount > right.Amount;
    }

    public static bool operator >=(Money left, Money right)
    {
        AssertSameCurrency(left, right);
        return left.Amount >= right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
        AssertSameCurrency(left, right);
        return left.Amount < right.Amount;
    }

    public static bool operator <=(Money left, Money right)
    {
        AssertSameCurrency(left, right);
        return left.Amount <= right.Amount;
    }

    #endregion

    #region Operator +, -
    public static Money operator +(Money left, Money right)
    {
        AssertSameCurrency(left, right);
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator +(Money left, decimal right)
    {
        return new Money(left.Amount + right, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        AssertSameCurrency(left, right);
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator -(Money left, decimal right)
    {
        return new Money(left.Amount - right, left.Currency);
    }

    #endregion

    #region Operator *, /

    public static Money operator *(Money left, Money right)
    {
        AssertSameCurrency(left, right);
        return new Money(left.Amount * right.Amount, left.Currency);
    }

    public static Money operator *(Money left, decimal right)
    {
        return new Money(left.Amount * right, left.Currency);
    }

    public static Money operator /(Money left, Money right)
    {
        AssertSameCurrency(left, right);
        return new Money(left.Amount / right.Amount, left.Currency);
    }

    public static Money operator /(Money left, decimal right)
    {
        return new Money(left.Amount / right, left.Currency);
    }

    #endregion

    public static void AssertSameCurrency(Money first, Money second)
    {
        if (first == null || second == null)
            throw new ArgumentNullException("One or both Money Is Null");
        if (first.Currency != second.Currency)
            throw new ArgumentException("Money Currency Not Equal");
    }

    /// <summary>
    /// Use the decorated interal Currency object to display the string
    /// </summary>
    /// 
    /// <returns>string</returns>
    public override string ToString()
    {
        return Currency.ToString(this);
    }
}