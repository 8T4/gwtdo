namespace Gwtdo.Steps;

/// <summary>
/// The And class is a C# code that defines a sealed class with a private constructor and a public static factory
/// method named Create. The class uses the singleton pattern to ensure only one instance of the class is created.
/// </summary>
public sealed class And
{
    /// <summary>
    /// The private constructor ensures that no instance of the And class can be created outside of the class itself.
    /// </summary>
    private static And? _and;
    /// <summary>
    /// The Create method is a public static factory method that returns an instance of the And class. If an instance
    /// of the class has not been created yet, it creates a new one, stores it in a private static nullable field _and,
    /// and returns it. If an instance of the class already exists, it returns the existing instance.
    /// </summary>
    /// <returns></returns>
    public static And Create() => _and ??= new And();
}