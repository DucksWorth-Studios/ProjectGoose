This file contains the code structure that we must follow

# Commenting Etiquette

* Place the comment on a separate line, not at the end of a line of code.
* Begin comment text with an uppercase letter.
* End comment text with a period.
* Insert one space between the comment delimiter (//) and the comment text, as shown in the following example.
* Do not create formatted blocks of asterisks around comments.

**We must comment as we go**

**All Merges to master can only be merged when comments are included**
Commenting will be heavily enforced within the project we will use C# triple slash documentation. Will allow an XML documentation sheet to be developed.
Simple Triple Slash Comment

```
///<summary>
/// Author:
/// Description
///
///</summary>
```

More Advanced Version:
```
/// <summary>
/// Author:
/// Description
/// </summary>
/// <param name=""> what is it </param>
```


# Naming Conventions
Upper Camel is used for classes and any file names.
Lower camel for methods variables etc
All uppercase for Consts

Always use “I” as prefix for Interfaces. This is a common practice for declaring interfaces.
Prefix “Is”, “Has” or “Can” for boolean properties like “IsVisible”, “HasChildren”, “CanExecute”. These give proper meaning to the properties.

Never prefix or suffix the class name to its property names. It will unnecessarily increase the property name. If “Firstname” is a property of “Person” class, you can easily identify it from that class directly. No need to write “PersonFirstname” or “FirstnameOfPerson”.

# Coding Practices
### Implicitly Typed Variables:
* Use implicit typing for local variables when the type of the variable is obvious from the right side of the assignment, or when the precise type is not important.
..* Eg: var var1 = "This is clearly a string.";
* Do not rely on the variable name to specify the type of the variable. It might not be correct.
* Avoid the use of var in place of dynamic.



### Static Members
Call static members by using the class name: ClassName.StaticMember. This practice makes code more readable by making static access clear. Do not qualify a static member defined in a base class with the name of a derived class. While that code compiles, the code readability is misleading, and the code may break in the future if you add a static member with the same name to the derived class.

### Always Use Properties instead of Public Variables
	• By using getters & setters, you can restrict the user directly accessing the member variables.

### Use Nullable data type when needed
int? index = null; // nullable data type declaration
		
### String Concatination
Prefer string.Format() or StringBuilder for String Concatenation
