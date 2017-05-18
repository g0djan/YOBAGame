using System;
using System.IO;

namespace YOBAGame.Exceptions
{
    public class MapLoadingException : Exception
    {
        public MapLoadingException(Exception e) : base("", e)
        {
        }
    }
}