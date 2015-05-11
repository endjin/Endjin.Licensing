Endjin.Licensing
================

The framework is inspired by [Rhino.Licensing](http://www.hibernatingrhinos.com/oss/rhino-licensing), an excellent & elegant framework based on creating a [cryptographic signature for an XML document](http://en.wikipedia.org/wiki/XML_Signature) which allows you to check to see if the message has been tampered with. The framework is layered on top of this principal, but allows you easily to specify your own license validation rules. 

License generation and license validation have been seperated into different packages. 

- Endjin.Licensing contains validation & extensibility points
- Endjin.Licensing.Infrastructure contains license generation and extensibility for storing and retrieving license data

The solution contains an example that has both server and client applications, which demonstrate the separation of concerns and how you can implement custom license validation rules. A Specflow based executable specification project also exists and is a good starting point for getting to know the features & behaviour of the framework.

The framework is available for both .NET 4.0 & .NET 4.5 via NuGet:

```
Install-Package Endjin.Licensing
```
and

```
Install-Package Endjin.Licensing.Infrastructure
```

A 5 post blog series has been written which provides a more detailed tour of the framework:

- [Part 1: Why build another licensing system?](https://blogs.endjin.com/2015/03/endjin-licensing-part-1-why-build-another-licensing-system/)
- [Part 2: Defining the desired behaviour](https://blogs.endjin.com/2015/03/endjin-licensing-part-2-defining-the-desired-behaviour/)
- [Part 3: How to create and validate a license](https://blogs.endjin.com/2015/03/endjin-licensing-part-3-how-to-create-and-validate-a-license/)
- [Part 4: How to implement custom validation logic](https://blogs.endjin.com/2015/03/endjin-licensing-part-4-how-to-implement-custom-validation-logic/)
- [Part 5: Real world usage patterns](https://blogs.endjin.com/2015/03/endjin-licensing-part-5-real-world-usage-patterns/)

The last point to highlight is that the whole point of Endjin.Licensing (and many other licensing frameworks) is to act as a mechanism for users to do the right thing. Anyone with access to your binaries can reverse engineer your code (think how simple that is with tools like [DotPeek](https://www.jetbrains.com/decompiler/) or [.NET Reflector](http://www.red-gate.com/products/dotnet-development/reflector/)) and patch them to circumvent your licensing. You can try more sophisticated techniques such as obfuscation via tools like [Dotfuscator](https://www.preemptive.com/products/dotfuscator/overview) or [SmartAssembly](http://www.red-gate.com/products/dotnet-development/smartassembly/) or use a packing tool like Themida  which can inject unmanaged code into your assembly to prevent tools like DotPeek and .NET Reflector from decompiling them. If you’re looking to use Endjin.Licensing, then I would advise you read about these techniques. There are also [several interesting](http://stackoverflow.com/questions/2478230/how-can-i-protect-my-net-assemblies-from-decompilation) StackOverflow articles containing [differing perspectives](http://stackoverflow.com/questions/506282/protect-net-code-from-reverse-engineering) about the merits of code protection mechanisms – well worth your time.

@[HowardvRooijen](http://twitter.com/howardvrooijen) | @[endjin](http://twitter.com/endjin) 
