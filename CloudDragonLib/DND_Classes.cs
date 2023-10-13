using System;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;

public class DNDClasses
{
    [JsonProperty("Class Name")]
    public string className { get; set; }
    public string classDescription { get; set; }
    public string ClassName { get; }

    public DNDClasses(string className)
    {
        ClassName = className;
    }

    public DNDClasses()
    {
    }
}