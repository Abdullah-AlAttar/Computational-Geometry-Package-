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
    public class JarvisMarchTest : ConvexHullTest
    {
        [TestMethod]
        public void JarvisMarchTestCase1()
        {
            convexHullTester = new JarvisMarch();
            Case1();
        }
        [TestMethod]
        public void JarvisMarchTestCase2()
        {
            convexHullTester = new JarvisMarch();
            Case2();
        }
        [TestMethod]
        public void JarvisMarchTestCase3()
        {
            convexHullTester = new JarvisMarch();
            Case3();
        }
        [TestMethod]
        public void JarvisMarchTestCase4()
        {
            convexHullTester = new JarvisMarch();
            Case4();
        }
        [TestMethod]
        public void JarvisMarchTestCase5()
        {
            convexHullTester = new JarvisMarch();
            Case5();
        }
        [TestMethod]
        public void JarvisMarchTestCase6()
        {
            convexHullTester = new JarvisMarch();
            Case6();
        }
        [TestMethod]
        public void JarvisMarchTestCase7()
        {
            convexHullTester = new JarvisMarch();
            Case7();
        }
        [TestMethod]
        public void JarvisMarchTestCase8()
        {
            convexHullTester = new JarvisMarch();
            Case8();
        }
        [TestMethod]
        public void JarvisMarchTestCase9()
        {
            convexHullTester = new JarvisMarch();
            Case9();
        }
        [TestMethod]
        public void JarvisMarchTestCase10()
        {
            convexHullTester = new JarvisMarch();
            Case10();
        }
        [TestMethod]
        public void JarvisMarchTestCase11()
        {
            convexHullTester = new JarvisMarch();
            Case11();
        }
        [TestMethod]
        public void JarvisMarchTestCase12()
        {
            convexHullTester = new JarvisMarch();
            Case12();
        }
        [TestMethod]
        public void JarvisMarchTestCase13()
        {
            convexHullTester = new JarvisMarch();
            Case13();
        }
    }
}
