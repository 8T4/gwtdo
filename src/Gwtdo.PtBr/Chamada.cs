using System;

namespace Gwtdo.PtBr
{
    /// <summary>
    /// Act on the object (through some mutator). You may need to give it parameters (again, possibly test objects).
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Chamada<T> where T: IFixture
    {
        public T Value { get; }
        public Chamada<T> E =>  this;
        
        private Chamada(T value) => Value = value;
        public static Chamada<T> Criar(T value) => new Chamada<T>(value);        
        
        public Chamada<T> Excecute(Action<T> action)
        {
            action.Invoke(Value);
            return this;
        }            
    }
}