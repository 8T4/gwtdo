using System;

namespace Gwtdo.PtBr
{
    /// <summary>
    /// Set up the object to be tested. We may need to surround the object with collaborators.
    /// For testing purposes, those collaborators might be test objects (mocks, fakes, etc.) or the real thing.
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Configuracao<T> where T : IFixture
    {
        public T Value { get; }
        public Configuracao<T> E =>  this;
        
        private Configuracao(T value) => Value = value;
        public static Configuracao<T> Criar(T value) => new Configuracao<T>(value);
        
        public Configuracao<T> Iniciar(Action<T> action)
        {
            action.Invoke(Value);
            return this;
        }        
    }
}