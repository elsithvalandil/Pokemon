namespace Pokemon.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"Pokemon \"{name}\" ({key})  Not Found")
        {
        }
    }
}
