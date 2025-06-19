using System;

namespace CloudDragon.Models.ModelContext
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelContextAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class ModelFieldAttribute : Attribute
    {
        public string Description { get; }

        public ModelFieldAttribute(string description)
        {
            Description = description;
        }
    }
}
