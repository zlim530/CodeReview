﻿using System;

namespace MyNamespace
{
    public interface IMyInterface
    {
        public int Calculate(float value, float exp);
    }

    public enum MyEnum
    { 
        Yes,
        No,
    }

    public class MyClass
    {
        public int Foo = 0;

        public bool NoCounting = false;
        private class Results
        {
            public int NumNegativeResults = 0;
            public int NumPositiveResults = 0;
        }
        private Results _results;

        public static int NumTimesCalled = 0;
        private const int _bar = 100;

        private int[] _someTable = { 2, 3, 4, };

        public MyClass()
        {
            _results = new Results
            {
                NumNegativeResults = 1,
                NumPositiveResults = 1,
            };     
        }

        public int CalculateValue(int mulNumber)
        {
            var resultValue = Foo * mulNumber;
            NumTimesCalled++;
            Foo += _bar;

            if (!NoCounting)
            {
                if (resultValue < 0)
                {
                    _results.NumNegativeResults++;
                }
                else if (resultValue > 0)
                {
                    _results.NumPositiveResults++;
                }
            }

            return resultValue;
        }

        public void ExpressionBodies()
        {
            Func<int, int> increment = x => x + 1;

            Func<int, int, long> difference1 = (x, y) =>
            {
                long diff = (long)x - y;
                return diff >= 0 ? diff : -diff;
            };

            void DoNothing() { }

            void AVeryLongFunctionNameThatCausesLinerWrappingProblems(int longArgumentName,
                                                                    int p1, int p2){ }

            void AnotherLongFunctionNameThatCausesLineWrappingProblems(
                int longArgumentName, int longArgumentName2, int longArgumentName3){ }

        }

    }
}