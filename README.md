# MillennialResortManager (MRM, Inc.)


#Product Owner
## Craig Barkley - [a link](https://github.com/tektechnologies)

# Scrum Leaders

#Scrum Teams

# C# Coding Standards and Naming Conventions


| Object Name               | Notation   | Length | Plural | Prefix | Suffix | Abbreviation | Char Mask          | Underscores |
|:--------------------------|:-----------|-------:|:-------|:-------|:-------|:-------------|:-------------------|:------------|
| Class name                | PascalCase |    128 | No     | No     | Yes    | No           | [A-z][0-9]         | No          |
| Constructor name          | PascalCase |    128 | No     | No     | Yes    | No           | [A-z][0-9]         | No          |
| Method name               | PascalCase |    128 | Yes    | No     | No     | No           | [A-z][0-9]         | No          |
| Method arguments          | camelCase  |    128 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Local variables           | camelCase  |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Constants name            | PascalCase |     50 | No     | No     | No     | No           | [A-z][0-9]         | No          |
| Field name                | camelCase  |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | Yes         |
| Properties name           | PascalCase |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Delegate name             | PascalCase |    128 | No     | No     | Yes    | Yes          | [A-z]              | No          |
| Enum type name            | PascalCase |    128 | Yes    | No     | No     | No           | [A-z]              | No          |



***Why: consistent with the Microsoft's .NET Framework and easy to read.***

#### 3. Do not use Hungarian notation or any other type identification in identifiers



#### 4. Do not use Screaming Caps for constants or readonly variables:



#### 5. Use meaningful names for variables. The following example uses seattleCustomers for customers who are located in Seattle:




#### 6. Avoid using Abbreviations. Exceptions: abbreviations commonly used as names, such as Id, Xml, Ftp, Uri.



#### 7. Do use PascalCasing for abbreviations 3 characters or more (2 chars are both uppercase):




#### 8. Do not use Underscores in identifiers. Exception: you can prefix private static variables with an underscore:



#### 9. Do use predefined type names instead of system type names like Int16, Single, UInt64, etc.

 

#### 10. Do use implicit type var for local variable declarations. Exception: primitive types (int, string, double, etc) use predefined names. 

#### 11. Do use noun or noun phrases to name a class. 


#### 12. Do prefix interfaces with the letter I. Interface names are noun (phrases) or adjectives.

#### 13. Do name source files according to their main classes. Exception: file names with partial classes reflect their source or purpose, e.g. designer, generated, etc. 


#### 14. Do organize namespaces with a clearly defined structure: 

#### 15. Do vertically align curly brackets: 

#### 16. Do declare all member variables at the top of a class, with static variables at the very top.

#### 17. Do use singular names for enums. Exception: bit field enums.


#### 18. Do not explicitly specify a type of an enum or values of enums (except bit fields):



#### 19. Do not use an "Enum" suffix in enum type names:


#### 20. Do not use "Flag" or "Flags" suffixes in enum type names:

#### 21. Do use suffix EventArgs at creation of the new classes comprising the information on event:


#### 22. Do name event handlers (delegates used as types of events) with the "EventHandler" suffix, as shown in the following example:


#### 23. Do not create names of parametres in methods (or constructors) which differ only by the register:



#### 24.
#### 25. 


## Offical Reference

1. [MSDN General Naming Conventions](http://msdn.microsoft.com/en-us/library/ms229045(v=vs.110).aspx)
2. [DoFactory C# Coding Standards and Naming Conventions](http://www.dofactory.com/reference/csharp-coding-standards) 
3. [MSDN Naming Guidelines](http://msdn.microsoft.com/en-us/library/xzf533w0%28v=vs.71%29.aspx)
4. [MSDN Framework Design Guidelines](http://msdn.microsoft.com/en-us/library/ms229042.aspx)
