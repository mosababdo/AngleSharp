﻿namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Extensions for the list of attributes.
    /// </summary>
    [DebuggerStepThrough]
    static class AttrExtensions
    {
        /// <summary>
        /// Compares another attribute container to the current container.
        /// </summary>
        /// <param name="sourceAttributes">The original attribute list.</param>
        /// <param name="targetAttributes">
        /// The list of attributes to compare to.
        /// </param>
        /// <returns>True if both objects are equal, otherwise false.</returns>
        public static Boolean AreEqual(this INamedNodeMap sourceAttributes, INamedNodeMap targetAttributes)
        {
            if (sourceAttributes.Length != targetAttributes.Length)
                return false;

            foreach (var elA in sourceAttributes)
            {
                var found = false;

                foreach (var elB in targetAttributes)
                {
                    if (found = (elA.Name == elB.Name && elA.NamespaceUri == elB.NamespaceUri && elA.Value == elB.Value))
                        break;
                }

                if (!found)
                    return false;
            }

            return true;
        }
    }
}
