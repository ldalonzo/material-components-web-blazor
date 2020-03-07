namespace Test.Leonardo.AspNetCore.Components.Material.Framework
{
    public class ValueSpy<T>
    {
        public ValueSpy(T initialValue = default)
        {
            Value = initialValue;
        }

        public T Value { get; private set; }

        public void SetValue(T value) => Value = value;
    }
}
