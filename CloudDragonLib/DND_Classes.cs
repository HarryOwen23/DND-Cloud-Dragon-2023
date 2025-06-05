using System;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;

public class DNDClasses
{
    [JsonProperty("Class Name")]
    public string ClassName { get; set; }

    [JsonProperty("Class Description")]
    public string ClassDescription { get; set; }

    public DNDClasses(string className)
    {
        ClassName = className;
    }

    public DNDClasses()
    {
    }
}