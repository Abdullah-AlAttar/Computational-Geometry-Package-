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
    public class DivideAndConquerTest : ConvexHullTest
    {
        [TestMethod]
        public void DivideAndConquerTestCase1()
        {
            convexHullTester = new DivideAndConquer();
            Case1();
        }
        [TestMethod]
        public void DivideAndConquerTestCase2()
        {
            convexHullTester = new DivideAndConquer();
            Case2();
        }
        [TestMethod]
        public void DivideAndConquerTestCase3()
        {
            convexHullTester = new DivideAndConquer();
            Case3();
        }
        [TestMethod]
        public void DivideAndConquerTestCase4()
        {
            convexHullTester = new DivideAndConquer();
            Case4();
        }
        [TestMethod]
        public void DivideAndConquerTestCase5()
        {
            convexHullTester = new DivideAndConquer();
            Case5();
        }
        [TestMethod]
        public void DivideAndConquerTestCase6()
        {
            convexHullTester = new DivideAndConquer();
            Case6();
        }
        [TestMethod]
        public void DivideAndConquerTestCase7()
        {
            convexHullTester = new DivideAndConquer();
            Case7();
        }
        [TestMethod]
        public void DivideAndConquerTestCase8()
        {
            convexHullTester = new DivideAndConquer();
            Case8();
        }
        [TestMethod]
        public void DivideAndConquerTestCase9()
        {
            convexHullTester = new DivideAndConquer();
            Case9();
        }
        [TestMethod]
        public void DivideAndConquerTestCase10()
        {
            convexHullTester = new DivideAndConquer();
            Case10();
        }
        [TestMethod]
        public void DivideAndConquerTestCase11()
        {
            convexHullTester = new DivideAndConquer();
            Case11();
        }
        [TestMethod]
        public void DivideAndConquerTestCase12()
        {
            convexHullTester = new DivideAndConquer();
            Case12();
        }
        [TestMethod]
        public void DivideAndConquerTestCase13()
        {
            convexHullTester = new DivideAndConquer();
            Case13();
        }
    }
}
