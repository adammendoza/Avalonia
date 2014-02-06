﻿// -----------------------------------------------------------------------
// <copyright file="SelectorTests.cs" company="Steven Kirk">
// Copyright 2014 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Perspex.UnitTests.Styling
{
    using System.Linq;
    using System.Reactive.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Perspex.Controls;
    using Perspex.Styling;

    [TestClass]
    public class SelectorTests_Class
    {
        [TestMethod]
        public void Class_Matches_Control_With_Class()
        {
            var control = new Control1
            {
                Classes = new Classes { "foo" },
            };

            var target = new Selector().Class("foo");

            CollectionAssert.AreEqual(new[] { true }, target.GetActivator(control).Take(1).ToEnumerable().ToArray());
        }

        [TestMethod]
        public void Class_Doesnt_Match_Control_Without_Class()
        {
            var control = new Control1
            {
                Classes = new Classes { "bar" },
            };

            var target = new Selector().Class("foo");

            CollectionAssert.AreEqual(new[] { false }, target.GetActivator(control).Take(1).ToEnumerable().ToArray());
        }

        [TestMethod]
        public void Class_Doesnt_Match_Control_With_TemplatedParent()
        {
            var control = new Control1
            {
                Classes = new Classes { "foo" },
                TemplatedParent = new Mock<ITemplatedControl>().Object,
            };

            var target = new Selector().Class("foo");

            CollectionAssert.AreEqual(new[] { false }, target.GetActivator(control).Take(1).ToEnumerable().ToArray());
        }

        [TestMethod]
        public void Class_Tracks_Additions()
        {
            var control = new Control1();

            var target = new Selector().Class("foo");
            var activator = target.GetActivator(control);

            CollectionAssert.AreEqual(new[] { false }, activator.Take(1).ToEnumerable().ToArray());
            control.Classes.Add("foo");
            CollectionAssert.AreEqual(new[] { true }, activator.Take(1).ToEnumerable().ToArray());
        }

        [TestMethod]
        public void Class_Tracks_Removals()
        {
            var control = new Control1
            {
                Classes = new Classes { "foo" },
            };

            var target = new Selector().Class("foo");
            var activator = target.GetActivator(control);

            CollectionAssert.AreEqual(new[] { true }, activator.Take(1).ToEnumerable().ToArray());
            control.Classes.Remove("foo");
            CollectionAssert.AreEqual(new[] { false }, activator.Take(1).ToEnumerable().ToArray());
        }

        public class Control1 : TestControlBase
        {
        }
    }
}