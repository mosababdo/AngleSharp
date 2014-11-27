﻿namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-origins
    /// </summary>
    sealed class CSSBackgroundOriginProperty : CSSProperty, ICssBackgroundOriginProperty
    {
        #region Fields

        readonly List<BoxModel> _origins;

        #endregion

        #region ctor

        internal CSSBackgroundOriginProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BackgroundOrigin, rule)
        {
            _origins = new List<BoxModel>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration with the desired origin settings.
        /// </summary>
        public IEnumerable<BoxModel> Origins
        {
            get { return _origins; }
        }

        #endregion

        #region Methods

        public void SetOrigins(IEnumerable<BoxModel> origins)
        {
            _origins.Clear();
            _origins.AddRange(origins);
        }

        internal override void Reset()
        {
            _origins.Clear();
            _origins.Add(BoxModel.PaddingBox);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.TakeList(this.WithBoxModel()).TryConvert(value, SetOrigins);
        }

        #endregion
    }
}
