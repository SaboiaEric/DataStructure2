using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoGrafos.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    [TestClass]
    public class M1
    {
        [TestMethod]
        public void EulerianTest1()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");

            g.AddEdge("A", "B", 0);
            g.AddEdge("B", "A", 0);
            g.AddEdge("B", "C", 0);
            g.AddEdge("C", "B", 0);

            bool hasEulerianPath = g.HasEulerianPath();
            Assert.IsTrue(hasEulerianPath);

            List<string> r = g.EulerianPath();
            var validSolutions = new List<List<string>>();
            validSolutions.Add(new List<string> { "A", "B", "C" });
            validSolutions.Add(new List<string> { "C", "B", "A" });
            Assert.IsTrue(validSolutions.Any(solution => solution.SequenceEqual(r)));
        }

        [TestMethod]
        public void EulerianTest2()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");

            g.AddEdge("A", "B", 0);
            g.AddEdge("B", "A", 0);
            g.AddEdge("B", "C", 0);
            g.AddEdge("C", "B", 0);
            g.AddEdge("B", "D", 0);
            g.AddEdge("D", "B", 0);

            bool hasEulerianPath = g.HasEulerianPath();
            Assert.IsFalse(hasEulerianPath);

            List<string> r = g.EulerianPath();
            Assert.IsNull(r);
        }

        [TestMethod]
        public void EulerianTest3()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");

            g.AddEdge("A", "B", 0);
            g.AddEdge("B", "A", 0);
            g.AddEdge("B", "C", 0);
            g.AddEdge("C", "B", 0);
            g.AddEdge("B", "D", 0);
            g.AddEdge("D", "B", 0);
            g.AddEdge("A", "D", 0);
            g.AddEdge("D", "A", 0);

            bool hasEulerianPath = g.HasEulerianPath();
            Assert.IsTrue(hasEulerianPath);

            var r = g.EulerianPath();
            var validSolutions = new List<List<string>>();
            validSolutions.Add(new List<string> { "B", "A", "D", "B", "C" });
            validSolutions.Add(new List<string> { "B", "D", "A", "B", "C" });
            validSolutions.Add(new List<string> { "C", "B", "A", "D", "B" });
            validSolutions.Add(new List<string> { "C", "B", "D", "A", "B" });
            Assert.IsTrue(validSolutions.Any(solution => solution.SequenceEqual(r)));
        }

        [TestMethod]
        public void EulerianTest4()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddNode("E");

            g.AddEdge("A", "B", 0);
            g.AddEdge("B", "A", 0);
            g.AddEdge("B", "C", 0);
            g.AddEdge("C", "B", 0);
            g.AddEdge("B", "D", 0);
            g.AddEdge("D", "B", 0);
            g.AddEdge("A", "D", 0);
            g.AddEdge("D", "A", 0);

            var hasEulerianPath = g.HasEulerianPath();
            Assert.IsFalse(hasEulerianPath);

            var r = g.EulerianPath();
            Assert.IsNull(r);
        }

        [TestMethod]
        public void EulerianTest5()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddEdge("A", "B", 0);
            g.AddEdge("B", "A", 0);
            g.AddEdge("A", "C", 0);
            g.AddEdge("C", "A", 0);
            g.AddEdge("B", "C", 0);
            g.AddEdge("C", "B", 0);

            var hasEulerianPath = g.HasEulerianPath();
            Assert.IsTrue(hasEulerianPath);

            var r = g.EulerianPath();
            var validSolutions = new List<List<string>>();
            validSolutions.Add(new List<string> { "A", "B", "C", "A" });
            validSolutions.Add(new List<string> { "A", "C", "B", "A" });
            validSolutions.Add(new List<string> { "B", "A", "C", "B" });
            validSolutions.Add(new List<string> { "B", "C", "A", "B" });
            validSolutions.Add(new List<string> { "C", "A", "B", "C" });
            validSolutions.Add(new List<string> { "C", "B", "A", "C" });
            Assert.IsTrue(validSolutions.Any(solution => solution.SequenceEqual(r)));
        }

        //[TestMethod]
        //public void MinimumDistancesTest()
        //{
        //    Graph g = new Graph();
        //    g.AddNode("A");
        //    g.AddNode("B");
        //    g.AddNode("C");
        //    g.AddNode("D");
        //    g.AddEdge("A", "D", 6);
        //    g.AddEdge("B", "A", 9);
        //    g.AddEdge("B", "C", 3);
        //    g.AddEdge("C", "A", 5);
        //    g.AddEdge("D", "A", 1);
        //    g.AddEdge("D", "B", 4);
        //    g.AddEdge("D", "C", 12);
        //    Graph r = g.MinimumDistances();
        //    Assert.AreEqual(r.Nodes.Count, 4);
        //    double total = 0;
        //    int edgeQty = 0;
        //    r.Nodes.ForEach(n => n.Edges.ForEach(e => total += e.Cost));
        //    r.Nodes.ForEach(n => n.Edges.ForEach(e => edgeQty++));
        //    Assert.AreEqual(total, 97);
        //    Assert.AreEqual(edgeQty, 12);
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "A").Edges.Any(e => e.To.Name == "B" && e.Cost == 10));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "A").Edges.Any(e => e.To.Name == "C" && e.Cost == 13));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "A").Edges.Any(e => e.To.Name == "D" && e.Cost == 6));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "B").Edges.Any(e => e.To.Name == "A" && e.Cost == 8));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "B").Edges.Any(e => e.To.Name == "C" && e.Cost == 3));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "B").Edges.Any(e => e.To.Name == "D" && e.Cost == 14));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "C").Edges.Any(e => e.To.Name == "A" && e.Cost == 5));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "C").Edges.Any(e => e.To.Name == "B" && e.Cost == 15));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "C").Edges.Any(e => e.To.Name == "D" && e.Cost == 11));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "D").Edges.Any(e => e.To.Name == "A" && e.Cost == 1));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "D").Edges.Any(e => e.To.Name == "B" && e.Cost == 4));
        //    Assert.IsTrue(r.Nodes.Find(n => n.Name == "D").Edges.Any(e => e.To.Name == "C" && e.Cost == 7));
        //}
    }
}
