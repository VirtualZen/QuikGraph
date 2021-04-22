using System;
using System.Text;
using JetBrains.Annotations;

namespace QuikGraph.Graphviz.Dot
{
    /// <summary>
    /// Graphviz record.
    /// </summary>
#if SUPPORTS_SERIALIZATION
    [Serializable]
#endif
    public class GraphvizRecord
    {
        [NotNull, ItemNotNull]
        private GraphvizRecordCellCollection _cells = new GraphvizRecordCellCollection();

        /// <summary>
        /// Record cells.
        /// </summary>
        [NotNull, ItemNotNull]
        public GraphvizRecordCellCollection Cells
        {
            get => _cells;
            set => _cells = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Converts this record to DOT.
        /// </summary>
        /// <returns>Record as DOT.</returns>
        [Pure]
        [NotNull]
        public string ToDot()
        {
            if (Cells.Count == 0)
                return string.Empty;

            var builder = new StringBuilder();

            bool flag = false;
            foreach (GraphvizRecordCell cell in Cells)
            {
                if (flag)
                {
                    builder.Append($" | {cell.ToDot()}");
                    continue;
                }
                builder.Append(cell.ToDot());
                flag = true;
            }

            return builder.ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToDot();
        }
    }
}