namespace anki;

class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    { }
}