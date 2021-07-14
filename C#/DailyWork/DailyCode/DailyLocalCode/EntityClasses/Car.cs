using System;
using System.Collections;

namespace DailyLocalCode
{
    public class Car:IEnumerable
    {
        private Car[] carList;

        public Car()
        {
            carList = new Car[6]
            {
                new Car("Ford",1992),
                new Car("Fiat",1988),
                new Car("Buick",1932),
                new Car("Ford",1932),
                new Car("Dodge",1999),
                new Car("Honda",1977),
            };
        }

        public Car(string name, int year)
        {
            Name = name;
            Year = year;
        }

        public string Name { get; set; }
        public int Year { get; set; }

        private class MyEnumerator : IEnumerator
        {
            public Car[] carList;
            int position = -1;

            public MyEnumerator(Car[] list)
            {
                carList = list;
            }

            private IEnumerator getEnumerator()
            {
                return (IEnumerator)this;
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return carList[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public bool MoveNext()
            {
                position++;
                return (position < carList.Length);
            }

            public void Reset()
            {
                position = -1;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(carList);
        }

    }

}
