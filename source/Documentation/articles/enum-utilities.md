# Enum utilities

## Decorating enumerations

The `CodeAttribute` allows you to decorate an enum field with a string `Code` in the same way you can do it with the `Description` attribute present in the *System.ComponentModel* namespace. This way, you have several choices when decorating enumerations and relate them with different things in your business logic.

```cs
using Celerik.NetCore.Util;
...

public enum SouthParkCharacter
{
	[Code("EC")]
	[Description("Eric Cartman")]
	Cartman = 1,

	[Code("KM")]
	[Description("Kenny McCormick")]
	Kenny = 2,

	[Code("KB")]
	[Description("Kyle Broflovski")]
	Kyle = 3,

	[Code("SM")]
	[Description("Stan Marsh")]
	Stan = 4
}
```

Once defined the Code of each item, you can use `GetCode()` extension method to retrieve the Code of an enum item. You can also use `GetDescription()` to pick the Description value as well:

```cs
var code = SouthParkCharacter.Cartman.GetCode();        // "EC"
var desc = SouthParkCharacter.Cartman.GetDescription(); // "Eric Cartman"
```

You can also retrieve Code and Description from the integer value:

```cs
var code = EnumUtility.GetCode<SouthParkCharacterType>(1);        // "EC"
var desc = EnumUtility.GetDescription<SouthParkCharacterType>(1); // "Eric Cartman"
```

Or you can use `GetAttribute()` extension method to get any other attribute present in your enumeration:

```cs
var cartman = SouthParkCharacter.Cartman;
var attribute = cartman.GetAttribute<CodeAttribute>();

Assert.AreEqual("EC", attribute.Code);
```

## Getting the min and max value of an enum
## Converting an enum to a list

...


