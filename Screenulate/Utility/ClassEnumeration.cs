using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;

namespace Screenulate.Utility
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ValueAttribute : Attribute
    {
        public int Order { get; }

        public ValueAttribute([CallerLineNumber] int order = 0)
        {
            Order = order;
        }
    }

    public abstract class ClassEnumeration<T> : IEquatable<ClassEnumeration<T>> where T : ClassEnumeration<T>
    {
        private struct DeferredProperties
        {
            public FieldInfo FieldInfo { get; set; }
            public int Index { get; set; }
        }

        private static readonly FieldInfo[] Fields = EnumerateFields();
        private static readonly Lazy<T[]> LazyObjects = new Lazy<T[]>(EnumerateObjects);
        public static T[] Values => LazyObjects.Value;

        private readonly Lazy<DeferredProperties> _properties;
        public string Name => _properties.Value.FieldInfo.Name;
        public int Index => _properties.Value.Index;

        protected ClassEnumeration()
        {
            _properties = new Lazy<DeferredProperties>(ComputeProperties);
        }

        private DeferredProperties ComputeProperties()
        {
            int i = 0;
            FieldInfo matchingField = null;
            foreach (var field in Fields)
            {
                if (ReferenceEquals(field.GetValue(null), this))
                {
                    matchingField = field;
                    break;
                }
                ++i;
            }

            if (matchingField == null)
                throw new NotSupportedException(
                    $"Instance of {typeof(T).Name} must be declared as public static with ValueAttribute");

            return new DeferredProperties()
            {
                FieldInfo = matchingField,
                Index = i
            };
        }

        private static T[] EnumerateObjects()
        {
            return Fields.Select(f => f.GetValue(null)).Cast<T>().ToArray();
        }

        private static FieldInfo[] EnumerateFields()
        {
            var valueFields = typeof(T).GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public |
                                                  BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                .Where(f => f.GetCustomAttributes(typeof(ValueAttribute), false).Length > 0);
            var fieldInfos = valueFields as FieldInfo[] ?? valueFields.ToArray();
            if (!fieldInfos.All(f => f.IsPublic && f.IsStatic))
                throw new NotSupportedException(
                    $"All ValueAttribute fields of {typeof(T).Name} must be public static");
            return fieldInfos
                .OrderBy(f => ((ValueAttribute)
                    f.GetCustomAttributes(typeof(ValueAttribute), false).Single()).Order).ToArray();
        }

        public bool Equals(ClassEnumeration<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ClassEnumeration<T>) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString() => Name;
    }
}