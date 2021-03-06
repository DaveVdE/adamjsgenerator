﻿using System;
using System.Globalization;
using System.Text;

namespace Adam.JSGenerator
{
    /// <summary>
    /// Represents a number, inserted as a literal.
    /// </summary>
    public class NumberExpression : Expression
    {
        private double _value;

        /// <summary>
        /// Initializes a new instance of <see cref="NumberExpression" /> for the specified Value.
        /// </summary>
        /// <param name="value">The Value of the literal.</param>
        public NumberExpression(double value)
        {
            _value = value;
        }

    	/// <summary>
    	/// Appends the script to represent this object to the StringBuilder.
    	/// </summary>
    	/// <param name="builder">The StringBuilder to which the Javascript is appended.</param>
    	/// <param name="options">The options to use when appending JavaScript</param>
    	/// <param name="allowReservedWords"></param>
    	internal protected override void AppendScript(StringBuilder builder, ScriptOptions options, bool allowReservedWords)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.Append(_value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets or sets the value to append as a literal.
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
