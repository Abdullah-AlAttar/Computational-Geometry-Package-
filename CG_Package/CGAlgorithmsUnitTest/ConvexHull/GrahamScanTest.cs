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
    public class GrahamScanTest : ConvexHullTest
    {
        [TestMethod]
        public void GrahamScanTestCase1()
        {
            convexHullTester = new GrahamScan();
            Case1();
        }
        [TestMethod]
        public void GrahamScanTestCase2()
        {
            convexHullTester = new GrahamScan();
            Case2();
        }
        [TestMethod]
        public void GrahamScanTestCase3()
        {
            convexHullTester = new GrahamScan();
            Case3();
        }
        [TestMethod]
        public void GrahamScanTestCase4()
        {
            convexHullTester = new GrahamScan();
            Case4();
        }
        [TestMethod]
        public void GrahamScanTestCase5()
        {
            convexHullTester = new GrahamScan();
            Case5();
        }
        [TestMethod]
        public void GrahamScanTestCase6()
        {
            convexHullTester = new GrahamScan();
            Case6();
        }
        [TestMethod]
        public void GrahamScanTestCase7()
        {
            convexHullTester = new GrahamScan();
            Case7();
        }
        [TestMethod]
        public void GrahamScanTestCase8()
        {
            convexHullTester = new GrahamScan();
            Case8();
        }
        [TestMethod]
        public void GrahamScanTestCase9()
        {
            convexHullTester = new GrahamScan();
            Case9();
        }
        [TestMethod]
        public void GrahamScanTestCase10()
        {
            convexHullTester = new GrahamScan();
            Case10();
        }
        [TestMethod]
        public void GrahamScanTestCase11()
        {
            convexHullTester = new GrahamScan();
            Case11();
        }
        [TestMethod]
        public void GrahamScanTestCase12()
        {
            convexHullTester = new GrahamScan();
            Case12();
        }
        [TestMethod]
        public void GrahamScanTestCase13()
        {
            convexHullTester = new GrahamScan();
            Case13();
        }
    }
}
