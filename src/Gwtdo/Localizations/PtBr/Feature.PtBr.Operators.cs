namespace Gwtdo.Localizations.PtBr;

/// <summary>
/// Feature OPERATORS
/// </summary>
/// <typeparam name="T"></typeparam>
// public abstract partial class FeaturePtBr<T> where T : IFeatureContext
// {
//     public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, string other)
//     {
//         var syntagma = new Syntagma<T>(other, null);
//             
//         if (!feature.CENARIO.Paradigms.SyntagmaExists(syntagma))
//             feature.CENARIO.Paradigms.AddSyntagma(syntagma);
//
//         return feature;
//     }
//         
//     public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, And other) => feature;
//     public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, Let other) => feature;
//     public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, Arrange<T> other) => Add(feature, GwtConstants.GIVEN);
//     public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, Act<T> other) => Add(feature, GwtConstants.WHEN);
//     public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, Assert<T> other) => Add(feature, GwtConstants.THEN);
//         
//     private static FeaturePtBr<T> Add(FeaturePtBr<T> feature, string value)
//     {
//         var syntagma = new Syntagma<T>(value, null);
//         feature.CENARIO.Paradigms.AddSyntagma(syntagma);
//         feature.CENARIO.MappedParadigms.AddSyntagma(syntagma);
//         return feature;
//     }         
// }