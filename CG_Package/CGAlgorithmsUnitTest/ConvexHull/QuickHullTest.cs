﻿using System;
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
    public class QuickHullTest : ConvexHullTest
    {
        [TestMethod]
        public void QuickHullTestCase1()
        {
            convexHullTester = new QuickHull();
            Case1();
        }
        [TestMethod]
        public void QuickHullTestCase2()
        {
            convexHullTester = new QuickHull();
            Case2();
        }
        [TestMethod]
        public void QuickHullTestCase3()
        {
            convexHullTester = new QuickHull();
            Case3();
        }
        [TestMethod]
        public void QuickHullTestCase4()
        {
            convexHullTester = new QuickHull();
            Case4();
        }
        [TestMethod]
        public void QuickHullTestCase5()
        {
            convexHullTester = new QuickHull();
            Case5();
        }
        [TestMethod]
        public void QuickHullTestCase6()
        {
            convexHullTester = new QuickHull();
            Case6();
        }
        [TestMethod]
        public void QuickHullTestCase7()
        {
            convexHullTester = new QuickHull();
            Case7();
        }
        [TestMethod]
        public void QuickHullTestCase8()
        {
            convexHullTester = new QuickHull();
            Case8();
        }
        [TestMethod]
        public void QuickHullTestCase9()
        {
            convexHullTester = new QuickHull();
            Case9();
        }
        [TestMethod]
        public void QuickHullTestCase10()
        {
            convexHullTester = new QuickHull();
            Case10();
        }
        [TestMethod]
        public void QuickHullTestCase11()
        {
            convexHullTester = new QuickHull();
            Case11();
        }
        [TestMethod]
        public void QuickHullTestCase12()
        {
            convexHullTester = new QuickHull();
            Case12();
        }
        [TestMethod]
        public void QuickHullTestCase13()
        {
            convexHullTester = new QuickHull();
            Case13();
        }
    }
}
