using System;

public class ImageTooLargeException : Exception
{
    public ImageTooLargeException(string message) : base(message) { }
}