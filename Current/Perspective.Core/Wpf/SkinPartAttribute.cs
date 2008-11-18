using System;

namespace Perspective.Core
{
    /// <summary>
    /// Represents an attribute that is applied to the class definition to identify the types of the named parts that are used for skinning.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class SkinPartAttribute : Attribute
    {
        private string _name;

        /// <summary>
        /// Gets or sets the pre-defined name of the part.
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        private Type _namedPartType;

        /// <summary>
        /// Gets or sets the type of the named part this attribute is identifying.
        /// </summary>
        public Type NamedPartType
        {
            get
            {
                return this._namedPartType;
            }
            set
            {
                this._namedPartType = value;
            }
        }
    }
}
