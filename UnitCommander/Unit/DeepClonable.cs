namespace Unit
{
    public abstract class DeepClonable<T> where T: class, new()
    {
        public virtual T Clone()
        {
            var result = this.MemberwiseClone();

            return (T)result;
        }
    }
}