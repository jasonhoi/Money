# Money Value Type W.I.P. #

## Introduction

A Money value type designed for C#, noted that since it is designed to be immutable-alike, you may need to cocnern about performance when doing long calculation on Money objects.

### Features

- Provides a `Money` class encapsulates all information and allow for dynamic display formatting based its own internal Currency setup.
- By design, most fields are ready-only, means each Money object is fixed when instantiated, as in the real world, Money can not change its form or currency type after creation, computation and conversion are allowed but they will always produce a new Money object.
- Provides a separate `Currency` class served as both the decorating class and property for each Money object.
- Currency types definition is defined in a separate respository, which means you can change and add currency types easily.
- Support multiple sub-types in a currency type, for example, under "Bitcoin" Currency, there are "mBTC" (1:1000), "Satoshi"(1:100000000), in USD, "Cent"(1:100) can be added as sub-type, DisplayAsSubType() can setup anMoney object to display in sub-type format and symbol, any sub-type settings will have no effect on a Money's fields, or arithmetic behavior.
- Currency repository only included a few popular fiat currencies and digital currencies (ex.Bitcoin, Litecoin).

### Resources

- Structure based on: https://github.com/dewe/Money
- Currency repository based on: http://www.lindelauf.com/?p=17http://www.codeproject.com/Articles/28244/A-Money-type-for-the-CLR?msg=3679755
- Good reference: http://github.com/RubyMoney/money


## Usage

```

# 10.00 USD
Money myMoney = new Money(10, "USD");
Money myMoney2 = new Money((decimal)20.05, "USD");

# Money display
var myMoney = new Money(0.1m, "BTC")
myMoney.toString()				#=> ฿0.1000
"I have " + myMoney + " in my pocket." 		#=> I have ฿0.1000 in my pocket.
myMoney.DisplayAsSubType("mBTC").toString()	#=> mBTC 100.0000

# Comparisons
var myMoney = new Money(10, "USD");
var myMoney2 = new Money(20.05m, "USD");

myMoney == new Money(10, "USD")			#=> true
myMoney == new Money(10.00001, "USD")	#=> false
myMoney == new Money(100, "EUR")		#=> false
myMoney != new Money(100, "EUR")		#=> true
myMoney > myMoney2				#=> false
myMoney <= myMoney2				#=> true

# Arithmetic
Money myMoney = new Money(10, "USD");
myMoney * new Money(10, "EUR")			#=> throw new ArgumentException("Money Currency Not Equal")
myMoney + myMoney2 == new Money((decimal)30.5, "USD")
myMoney - myMoney2 == new Money(-10.5m, "USD")
myMoney * myMoney2 == new Money(205.0m, "USD")
myMoney / 10       == new Money(1, "USD")
(myMoney - 0.5m) * 5 == new Money(100, "USD")



```
