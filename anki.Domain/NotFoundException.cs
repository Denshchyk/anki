namespace anki.Domain;

class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    { }
}