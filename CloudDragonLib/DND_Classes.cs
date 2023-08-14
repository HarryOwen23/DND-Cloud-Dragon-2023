using System;
using System.Xml.Linq;

public class DNDClasses
{
    public string className { get; set; }
    public string classDescription { get; set; }
    public string ClassName { get; }

    public DNDClasses(string className)
    {
        ClassName = className;
    }
}