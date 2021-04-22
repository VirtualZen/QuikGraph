using System;
using NUnit.Framework;
using QuikGraph.Predicates;

namespace QuikGraph.Tests.Predicates
{
    /// <summary>
    /// Tests for <see cref="FilteredIncidenceGraph{TVertex,TEdge,TGraph}"/>.
    /// </summary>
    [TestFixture]
    internal class FilteredIncidenceGraphTests : FilteredGraphTestsBase
    {
        [Test]
        public void Construction()
        {
            VertexPredicate<int> vertexPredicate = vertex => true;
            EdgePredicate<int, Edge<int>> edgePredicate = edge => true;

            var graph = new AdjacencyGraph<int, Edge<int>>();
            var filteredGraph = new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                graph,
                vertexPredicate,
                edgePredicate);
            AssertGraphProperties(filteredGraph, graph);

            graph = new AdjacencyGraph<int, Edge<int>>(false);
            filteredGraph = new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                graph,
                vertexPredicate,
                edgePredicate);
            AssertGraphProperties(filteredGraph, graph, false);

            #region Local function

            void AssertGraphProperties<TVertex, TEdge, TGraph>(
                FilteredIncidenceGraph<TVertex, TEdge, TGraph> g,
                TGraph expectedGraph,
                bool parallelEdges = true)
                where TEdge : IEdge<TVertex>
                where TGraph : IIncidenceGraph<TVertex, TEdge>
            {
                Assert.AreSame(expectedGraph, g.BaseGraph);
                Assert.IsTrue(g.IsDirected);
                Assert.AreEqual(parallelEdges, g.AllowParallelEdges);
                Assert.AreSame(vertexPredicate, g.VertexPredicate);
                Assert.AreSame(edgePredicate, g.EdgePredicate);
            }

            #endregion
        }

        [Test]
        public void Construction_Throws()
        {
            // ReSharper disable ObjectCreationAsStatement
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(
                () => new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                    new AdjacencyGraph<int, Edge<int>>(),
                    vertex => true,
                    null));

            Assert.Throws<ArgumentNullException>(
                () => new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                    new AdjacencyGraph<int, Edge<int>>(),
                    null,
                    edge => true));

            Assert.Throws<ArgumentNullException>(
                () => new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                    null,
                    vertex => true,
                    edge => true));

            Assert.Throws<ArgumentNullException>(
                () => new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                    new AdjacencyGraph<int, Edge<int>>(),
                    null,
                    null));

            Assert.Throws<ArgumentNullException>(
                () => new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                    null,
                    vertex => true,
                    null));

            Assert.Throws<ArgumentNullException>(
                () => new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                    null,
                    null,
                    edge => true));

            Assert.Throws<ArgumentNullException>(
                () => new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                    null,
                    null,
                    null));
            // ReSharper restore AssignNullToNotNullAttribute
            // ReSharper restore ObjectCreationAsStatement
        }

        #region Contains Vertex

        [Test]
        public void ContainsVertex()
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();
            ContainsVertex_Test(
                graph,
                (vertexPredicate, edgePredicate) =>
                    new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                        graph,
                        vertexPredicate,
                        edgePredicate));
        }

        [Test]
        public void ContainsVertex_Throws()
        {
            var filteredGraph = new FilteredIncidenceGraph<TestVertex, Edge<TestVertex>, AdjacencyGraph<TestVertex, Edge<TestVertex>>>(
                new AdjacencyGraph<TestVertex, Edge<TestVertex>>(),
                vertex => true,
                edge => true);
            ContainsVertex_Throws_Test(filteredGraph);
        }

        #endregion

        #region Contains Edge

        [Test]
        public void ContainsEdge()
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();
            ContainsEdge_SourceTarget_Test(
                graph,
                (vertexPredicate, edgePredicate) =>
                    new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                        graph,
                        vertexPredicate,
                        edgePredicate));
        }

        [Test]
        public void ContainsEdge_Throws()
        {
            var filteredGraph = new FilteredIncidenceGraph<TestVertex, Edge<TestVertex>, AdjacencyGraph<TestVertex, Edge<TestVertex>>>(
                new AdjacencyGraph<TestVertex, Edge<TestVertex>>(),
                vertex => true,
                edge => true);
            ContainsEdge_SourceTarget_Throws_Test(filteredGraph);
        }

        #endregion

        #region Out Edges

        [Test]
        public void OutEdge()
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();
            OutEdge_Test(
                graph,
                (vertexPredicate, edgePredicate) =>
                    new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                        graph,
                        vertexPredicate,
                        edgePredicate));
        }

        [Test]
        public void OutEdge_Throws()
        {
            var graph1 = new AdjacencyGraph<int, Edge<int>>();
            OutEdge_Throws_Test(
                graph1,
                (vertexPredicate, edgePredicate) =>
                    new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                        graph1,
                        vertexPredicate,
                        edgePredicate));

            var graph2 = new AdjacencyGraph<TestVertex, Edge<TestVertex>>();
            var filteredGraph2 = new FilteredIncidenceGraph<TestVertex, Edge<TestVertex>, AdjacencyGraph<TestVertex, Edge<TestVertex>>>(
                graph2,
                vertex => true,
                edge => true);
            OutEdge_NullThrows_Test(filteredGraph2);
        }

        [Test]
        public void OutEdges()
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();
            OutEdges_Test(
                graph,
                (vertexPredicate, edgePredicate) =>
                    new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                        graph,
                        vertexPredicate,
                        edgePredicate));
        }

        [Test]
        public void OutEdges_Throws()
        {
            var graph1 = new AdjacencyGraph<EquatableTestVertex, Edge<EquatableTestVertex>>();
            var filteredGraph1 = new FilteredIncidenceGraph
                <
                    EquatableTestVertex,
                    Edge<EquatableTestVertex>,
                    AdjacencyGraph<EquatableTestVertex, Edge<EquatableTestVertex>>
                >(
                graph1,
                vertex => true,
                edge => true);
            OutEdges_NullThrows_Test(filteredGraph1);
            OutEdges_Throws_Test(filteredGraph1);

            var graph2 = new AdjacencyGraph<int, Edge<int>>();
            var filteredGraph2 = new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                graph2,
                vertex => vertex < 4,
                edge => true);

            graph2.AddVertexRange(new[] { 1, 2, 3, 4, 5 });
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<VertexNotFoundException>(() => filteredGraph2.OutEdges(4));
            Assert.Throws<VertexNotFoundException>(() => filteredGraph2.OutEdges(5));
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }

        #endregion

        #region Try Get Edges

        [Test]
        public void TryGetEdge()
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();
            TryGetEdge_Test(
                graph,
                (vertexPredicate, edgePredicate) =>
                    new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                        graph,
                        vertexPredicate,
                        edgePredicate));
        }

        [Test]
        public void TryGetEdge_Throws()
        {
            var filteredGraph = new FilteredIncidenceGraph<TestVertex, Edge<TestVertex>, AdjacencyGraph<TestVertex, Edge<TestVertex>>>(
                new AdjacencyGraph<TestVertex, Edge<TestVertex>>(),
                vertex => true,
                edge => true);
            TryGetEdge_Throws_Test(filteredGraph);
        }

        [Test]
        public void TryGetEdges()
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();
            TryGetEdges_Test(
                graph,
                (vertexPredicate, edgePredicate) =>
                    new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                        graph,
                        vertexPredicate,
                        edgePredicate));
        }

        [Test]
        public void TryGetEdges_Throws()
        {
            var filteredGraph = new FilteredIncidenceGraph<TestVertex, Edge<TestVertex>, AdjacencyGraph<TestVertex, Edge<TestVertex>>>(
                new AdjacencyGraph<TestVertex, Edge<TestVertex>>(),
                vertex => true,
                edge => true);
            TryGetEdges_Throws_Test(filteredGraph);
        }

        [Test]
        public void TryGetOutEdges()
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();
            TryGetOutEdges_Test(
                graph,
                (vertexPredicate, edgePredicate) =>
                    new FilteredIncidenceGraph<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>(
                        graph,
                        vertexPredicate,
                        edgePredicate));
        }

        [Test]
        public void TryGetOutEdges_Throws()
        {
            TryGetOutEdges_Throws_Test(
                new FilteredIncidenceGraph<TestVertex, Edge<TestVertex>, AdjacencyGraph<TestVertex, Edge<TestVertex>>>(
                    new AdjacencyGraph<TestVertex, Edge<TestVertex>>(),
                    vertex => true,
                    edge => true));
        }

        #endregion
    }
}