using System;

namespace Gwtdo.PtBr
{
    /// <summary>
    /// Make claims about the object, its collaborators, its parameters, and possibly (rarely!!) global state. 
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>    
    public sealed class Afirmacao<T> where T: IFixture
    {
        public T Value { get; }
        public Afirmacao<T> E =>  this;
        
        private Afirmacao(T value) => Value = value;
        public static Afirmacao<T> Criar(T value) => new Afirmacao<T>(value);

        public Afirmacao<T> Validar(Action<T> action)
        {
            action.Invoke(Value);
            return this;
        }
    }
}