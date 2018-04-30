namespace Canducci.SqlRaw.Providers
{
    public abstract class Provider
    {
        public abstract string OpenTag();
        public abstract string CloseTag();

        public string CreateTag(string value)
        {
            return OpenTag() + value + CloseTag();
        }
    }
}
