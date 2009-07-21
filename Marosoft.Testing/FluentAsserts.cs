using System;
using System.Linq;
using System.Collections;
using NUnit.Framework;

namespace Marosoft.Testing
{
    public static class FluentAsserts
    {
        public static T should_satisfy<T>(this T @object, Predicate<T> condition)
        {
            Assert.IsTrue(condition(@object));
            return @object;
        }

        public static T should_satisfy<T>(this T @object, Predicate<T> condition, string failMessage)
        {
            Assert.IsTrue(condition(@object), failMessage);
            return @object;
        }

        public static void should_be_false(this bool condition)
        {
            Assert.IsFalse(condition);
        }

        public static void should_be_true(this bool condition)
        {
            Assert.IsTrue(condition);
        }

        public static string should_contain(this string actual, string expected)
        {
            Assert.IsTrue(actual.Contains(expected),
                    String.Format("\"{0}\" does not contain \"{1}\".", actual, expected));
            return actual;
        }

        /// <summary>
        /// Same as should_equal
        /// </summary>
        public static T should_be<T>(this T actual, object expected)
        {
            return should_equal(actual, expected);
        }

        public static T should_equal<T>(this T actual, object expected)
        {
            Assert.AreEqual(expected, actual);
            return actual;
        }

        public static T should_not_equal<T>(this T actual, object expected)
        {
            Assert.AreNotEqual(expected, actual);
            return actual;
        }

        public static void should_be_null(this object @object)
        {
            Assert.IsNull(@object);
        }

        public static T should_not_be_null<T>(this T @object)
        {
            Assert.IsNotNull(@object);
            return @object;
        }

        public static T should_be_same_as<T>(this T actual, object expected)
        {
            Assert.AreSame(expected, actual);
            return actual;
        }

        public static T should_not_be_same_as<T>(this T actual, object expected)
        {
            Assert.AreNotSame(expected, actual);
            return actual;
        }

        public static object should_be_of_type(this object actual, Type expected)
        {
            Assert.IsAssignableFrom(expected, actual);
            return actual;
        }

        public static TExpected should_be_of_type<TExpected>(this object actual)
        {
            Assert.IsAssignableFrom(typeof(TExpected), actual);
            return (TExpected)actual;
        }

        public static object should_be_of_exact_type(this object actual, Type expected)
        {
            should_not_be_null(actual);
            Assert.AreEqual(expected, actual.GetType());
            return actual;
        }

        public static TExpected should_be_of_exact_type<TExpected>(this object actual)
        {
            return (TExpected)should_be_of_exact_type(actual, typeof(TExpected));
        }

        public static object Should_not_be_of_type(this object actual, Type expected)
        {
            Assert.IsNotAssignableFrom(expected, actual);
            return actual;
        }

        public static object should_not_be_of_type<TExpected>(this object actual)
        {
            Assert.IsNotAssignableFrom(typeof(TExpected), actual);
            return actual;
        }

        public static IEnumerable should_contain(this IEnumerable collection, object expected)
        {
            Assert.Contains(expected, collection.Cast<object>().ToList());
            return collection;
        }

        public static IEnumerable should_not_contain(this IEnumerable collection, object expected)
        {
            Assert.IsFalse(collection.Cast<object>().Contains(expected),
                    "Collection contains the value.");
            return collection;
        }

        public static void should_be_empty(this string @string)
        {
            Assert.IsEmpty(@string);
        }

        public static string should_not_be_empty(this string @string)
        {
            Assert.IsNotEmpty(@string);
            return @string;
        }

        public static void should_be_empty(this IEnumerable collection)
        {
            Assert.IsEmpty(collection.Cast<object>().ToList());
        }

        public static IEnumerable should_not_be_empty(this IEnumerable collection)
        {
            Assert.IsNotEmpty(collection.Cast<object>().ToList());
            return collection;
        }

        public static void ShouldNotContain(this string actual, string expected)
        {
            Assert.IsFalse(actual.Contains(expected),
                    String.Format("\"{0}\" contains \"{1}\".", actual, expected));
        }

        public static void should_contain_error_message(this Exception exception, string expected)
        {
            should_contain(exception.Message, expected);
        }

        public static Exception should_be_thrown_by(this Type exceptionType, Action actionThatThrows)
        {
            Exception exception = actionThatThrows.GetException();

            Assert.IsNotNull(exception);
            Assert.AreEqual(exceptionType, exception.GetType());

            return exception;
        }

        public static Exception should_be_thrown_by(this Type exceptionType, Action actionThatThrows,
                Predicate<Exception> exceptionCondition)
        {
            Exception exception = actionThatThrows.GetException();

            Assert.IsNotNull(exception);
            Assert.AreEqual(exceptionType, exception.GetType());

            if (!exceptionCondition(exception))
                throw new ArgumentException("Exception throw did not satisfy the condition.");

            return exception;
        }

        public static Exception GetException(this Action actionThatThrows)
        {
            Exception exception = null;

            try
            {
                actionThatThrows();
            }
            catch (Exception e)
            {
                exception = e;
            }

            return exception;
        }

    }
}
