﻿using System.Collections.Generic;
using System.Text;

namespace Adam.JSGenerator
{
    /// <summary>
    /// Represents a list of statements.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class Script : IEnumerable<Statement> // Really, just for initializers.
    {
        private readonly List<Statement> _statements = new List<Statement>();

        /// <summary>
        /// Initializes a new instance of <see cref="Script" />.
        /// </summary>
        public Script()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Script" /> for the specified statements.
        /// </summary>
        /// <param name="statements">A sequence of statements.</param>
        public Script(IEnumerable<Statement> statements)
        {
            Add(statements);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Script" /> for the specified statements.
        /// </summary>
        /// <param name="statements">An array of statements.</param>
        public Script(params Statement[] statements)
        {
            Add(statements);
        }

        /// <summary>
        /// Gets or sets the list of statements.
        /// </summary>
        public IList<Statement> Statements
        {
            get
            {
                return _statements;
            }
        }

        /// <summary>
        /// Adds a new statement to the list.
        /// </summary>
        /// <param name="statement">The statement to add.</param>
        public void Add(Statement statement)
        {
            _statements.Add(statement);
        }

        /// <summary>
        /// Adds a number of statements to the list.
        /// </summary>
        /// <param name="statements">An array of statements to add to the list.</param>
        public void Add(params Statement[] statements)
        {
            if (statements != null)
            {
                _statements.AddRange(statements);                
            }
        }

        /// <summary>
        /// Adds a number of statements to the list.
        /// </summary>
        /// <param name="statements">A sequence of statements to add to the list.</param>
        public void Add(IEnumerable<Statement> statements)
        {
            if (statements != null)
            {
                _statements.AddRange(statements);
            }
        }

        /// <summary>
        /// Adds all the statements from another script to the list.
        /// </summary>
        /// <param name="script">A script with statements.</param>
        public void Add(Script script)
        {
            if (script != null)
            {
               Add(script.Statements);
            }
        }

        /// <summary>
        /// Produces all the statements as script.
        /// </summary>
        /// <param name="options">The options to use when generating JavaScript.</param>
        /// <returns>A string representing all the statements in the list as JavaScript.</returns>
        /// <remarks>
        /// All the statements in the list that are null are converted to instances of <see cref="EmptyStatement" />.
        /// </remarks>
        public string ToString(ScriptOptions options)
        {
            StringBuilder builder = new StringBuilder();

            foreach (Statement statement in _statements.WithConvertedNulls())
            {
                statement.AppendScript(builder, options, false);
                statement.AppendRequiredTerminator(builder);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Produces all the statements as script.
        /// </summary>
        /// <returns>A string representing all the statements in the list as JavaScript.</returns>
        /// <remarks>
        /// All the statements in the list that are null are converted to instances of <see cref="EmptyStatement" />.
        /// </remarks>
        public override string ToString()
        {
            return ToString(new ScriptOptions());
        }

        #region IEnumerable<Statement> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection of statements. 
        /// </summary>
        public IEnumerator<Statement> GetEnumerator()
        {
            return Statements.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Statements.GetEnumerator();
        }

        #endregion

    }
}
