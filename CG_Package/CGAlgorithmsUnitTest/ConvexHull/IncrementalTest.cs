using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CGAlgorithms.Algorithms.ConvexHull;
using CGAlgorithms;
using CGUtilities;
using System.Collections.Generic;

namespace CGAlgorithmsUnitTest
{
    /// <summary>
    /// Unit Test for Convex Hull
    /// </summary>
    [TestClass]
    public class IncrementalTest : ConvexHullTest
    {
        [TestMethod]
        public void IncrementalTestCase1()
        {
            convexHullTester = new Incremental();
            Case1();
        }
        [TestMethod]
        public void IncrementalTestCase2()
        {
            convexHullTester = new Incremental();
            Case2();
        }
        [TestMethod]
        public void IncrementalTestCase3()
        {
            convexHullTester = new Incremental();
            Case3();
        }
        [TestMethod]
        public void IncrementalTestCase4()
        {
            convexHullTester = new Incremental();
            Case4();
        }
        [TestMethod]
        public void IncrementalTestCase5()
        {
            convexHullTester = new Incremental();
            Case5();
        }
        [TestMethod]
        public void IncrementalTestCase6()
        {
            convexHullTester = new Incremental();
            Case6();
        }
        [TestMethod]
        public void IncrementalTestCase7()
        {
            convexHullTester = new Incremental();
            Case7();
        }
        [TestMethod]
        public void IncrementalTestCase8()
        {
            convexHullTester = new Incremental();
            Case8();
        }
        [TestMethod]
        public void IncrementalTestCase9()
        {
            convexHullTester = new Incremental();
            Case9();
        }
        [TestMethod]
        public void IncrementalTestCase10()
        {
            convexHullTester = new Incremental();
            Case10();
        }
        [TestMethod]
        public void IncrementalTestCase11()
        {
            convexHullTester = new Incremental();
            Case11();
        }
        [TestMethod]
        public void IncrementalTestCase12()
        {
            convexHullTester = new Incremental();
            Case12();
        }
        [TestMethod]
        public void IncrementalTestCase13()
        {
            convexHullTester = new Incremental();
            Case13();
        }
    }
}
