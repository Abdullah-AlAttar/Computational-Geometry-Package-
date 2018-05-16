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
    public class ExtremePointsTest : ConvexHullTest
    {
        [TestMethod]
        public void ExtremePointsTestCase1()
        {
            convexHullTester = new ExtremePoints();
            Case1();
        }
        [TestMethod]
        public void ExtremePointsTestCase2()
        {
            convexHullTester = new ExtremePoints();
            Case2();
        }
        [TestMethod]
        public void ExtremePointsTestCase3()
        {
            convexHullTester = new ExtremePoints();
            Case3();
        }
        [TestMethod]
        public void ExtremePointsTestCase4()
        {
            convexHullTester = new ExtremePoints();
            Case4();
        }
        [TestMethod]
        public void ExtremePointsTestCase5()
        {
            convexHullTester = new ExtremePoints();
            Case5();
        }
        [TestMethod]
        public void ExtremePointsTestCase6()
        {
            convexHullTester = new ExtremePoints();
            Case6();
        }
        [TestMethod]
        public void ExtremePointsTestCase7()
        {
            convexHullTester = new ExtremePoints();
            Case7();
        }
        [TestMethod]
        public void ExtremePointsTestCase8()
        {
            convexHullTester = new ExtremePoints();
            Case8();
        }
        [TestMethod]
        public void ExtremePointsTestCase9()
        {
            convexHullTester = new ExtremePoints();
            Case9();
        }
        [TestMethod]
        public void ExtremePointsTestCase10()
        {
            convexHullTester = new ExtremePoints();
            Case10();
        }
        [TestMethod]
        public void ExtremePointsTestCase11()
        {
            convexHullTester = new ExtremePoints();
            Case11();
        }
        [TestMethod]
        public void ExtremePointsTestCase12()
        {
            convexHullTester = new ExtremePoints();
            Case12();
        }
        [TestMethod]
        public void ExtremePointsTestCase13()
        {
            convexHullTester = new ExtremePoints();
            Case13();
        }
    }
}
