﻿using System;
using JetBrains.Annotations;
using QuikGraph.Graphviz.Dot;

namespace QuikGraph.Graphviz
{
    /// <summary>
    /// Arguments of an event related to the formatting of a clustered graph.
    /// </summary>
#if SUPPORTS_SERIALIZATION
    [Serializable]
#endif
    public class FormatClusterEventArgs<TVertex, TEdge> : EventArgs
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeEventArgs{TVertex, TEdge}"/> class.
        /// </summary>
        /// <param name="clusteredGraph">Graph to format.</param>
        /// <param name="graphFormat">Graph format.</param>
        public FormatClusterEventArgs([NotNull] IVertexAndEdgeListGraph<TVertex, TEdge> clusteredGraph, [NotNull] GraphvizGraph graphFormat)
        {
            Cluster = clusteredGraph ?? throw new ArgumentNullException(nameof(clusteredGraph));
            GraphFormat = graphFormat ?? throw new ArgumentNullException(nameof(graphFormat));
        }

        /// <summary>
        /// Graph to format.
        /// </summary>
        [NotNull]
        public IVertexAndEdgeListGraph<TVertex, TEdge> Cluster { get; }

        /// <summary>
        /// Graph format.
        /// </summary>
        [NotNull]
        public GraphvizGraph GraphFormat { get; }
    }
}