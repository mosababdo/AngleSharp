﻿namespace AngleSharp.DOM
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a generic node attribute.
    /// </summary>
    sealed class Attr : IAttr, IEquatable<IAttr>
    {
        #region Fields

        readonly Element _container;
        readonly String _localName;
        readonly String _prefix;
        readonly String _namespace;
        String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new NodeAttribute with empty value.
        /// </summary>
        /// <param name="container">The parent of the attribute.</param>
        /// <param name="localName">The name of the attribute.</param>
        internal Attr(Element container, String localName)
            : this(container, localName, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="container">The parent of the attribute.</param>
        /// <param name="localName">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        internal Attr(Element container, String localName, String value)
        {
            _container = container;
            _localName = localName;
            _value = value;
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="container">The parent of the attribute.</param>
        /// <param name="prefix">The prefix of the attribute.</param>
        /// <param name="localName">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="namespaceUri">The namespace of the attribute.</param>
        internal Attr(Element container, String prefix, String localName, String value, String namespaceUri)
        {
            _prefix = prefix;
            _localName = localName;
            _container = container;
            _value = value;
            _namespace = namespaceUri;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the namespace prefix of the specified node, or null if no prefix is specified.
        /// </summary>
        public String Prefix
        {
            get { return _prefix; }
        }

        /// <summary>
        /// Gets whether the attribute is an ID attribute.
        /// </summary>
        public Boolean IsId
        {
            get { return _prefix == null && _localName.Equals(AttributeNames.Id, StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>
        /// Gets if this attribute was explicitly given a value in the document.
        /// </summary>
        public Boolean Specified
        {
            get { return !String.IsNullOrEmpty(_value); }
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        public String Name
        {
            get { return _prefix == null ? _localName : String.Concat(_prefix, ":", _localName); }
        }

        /// <summary>
        /// Gets or sets the value of the attribute.
        /// </summary>
        public String Value
        {
            get { return _value; }
            set 
            { 
                var oldValue = _value;
                _value = value; 
                _container.AttributeChanged(_localName, _namespace, oldValue); 
            }
        }

        /// <summary>
        /// Gets the local name of the attribute.
        /// </summary>
        public String LocalName
        {
            get { return _localName; }
        }

        /// <summary>
        /// Gets the namespace URI of the attribute.
        /// </summary>
        public String NamespaceUri
        {
            get { return _namespace; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the given attribute to the current one.
        /// </summary>
        /// <param name="other">The attibute to compare to.</param>
        /// <returns>True if both attributes are equal, otherwise false.</returns>
        public Boolean Equals(IAttr other)
        {
            return other == this || (_value == other.Value && _localName == other.Name);
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns an HTML-code representation of the attribute.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToString()
        {
            var temp = Pool.NewStringBuilder();

            if (String.IsNullOrEmpty(_namespace))
                temp.Append(LocalName);
            else if (_namespace == Namespaces.XmlUri)
                temp.Append(Namespaces.XmlPrefix).Append(Specification.Colon).Append(LocalName);
            else if (_namespace == Namespaces.XLinkUri)
                temp.Append(Namespaces.XLinkPrefix).Append(Specification.Colon).Append(LocalName);
            else if (_namespace == Namespaces.XmlNsUri)
                temp.Append(XmlNamespaceLocalName());
            else
                temp.Append(_localName);

            temp.Append(Specification.Equality).Append(Specification.DoubleQuote);

            for (int i = 0; i < _value.Length; i++)
            {
                switch (_value[i])
                {
                    case Specification.Ampersand: temp.Append("&amp;"); break;
                    case Specification.NoBreakSpace: temp.Append("&nbsp;"); break;
                    case Specification.DoubleQuote: temp.Append("&quot;"); break;
                    default: temp.Append(_value[i]); break;
                }
            }

            return temp.Append(Specification.DoubleQuote).ToPool();
        }

        String XmlNamespaceLocalName()
        {
            if (LocalName != Namespaces.XmlNsPrefix)
                return String.Concat(Namespaces.XmlNsPrefix, ":");

            return LocalName;
        }

        #endregion
    }
}
;