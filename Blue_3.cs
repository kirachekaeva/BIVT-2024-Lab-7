using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _minuts;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_minuts == null) return null;
                    int[] copy = new int[_minuts.Length];
                    Array.Copy(_minuts, copy, copy.Length);
                    return copy;
                }
            }

            public int Total
            {
                get
                {
                    if (_minuts == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _minuts.Length; i++)
                    {
                        sum += _minuts[i];
                    }
                    return sum;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_minuts == null) return false;
                    bool isExpelled = false;
                    for (int i = 0; i < _minuts.Length; i++)
                    {
                        if (_minuts[i] == 10)
                        {
                            isExpelled = true;
                            break;
                        }
                    }
                    return isExpelled;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _minuts = new int[0];
            }


            public virtual void PlayMatch(int time)
            {
                if (_minuts == null) return;
                int[] arr = new int[_minuts.Length + 1];
                for (int i = 0; i < _minuts.Length; i++)
                {
                    arr[i] = _minuts[i];
                }
                arr[arr.Length - 1] = time;
                _minuts = arr;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;

                int n = array.Length;
                bool swapped;

                for (int i = 0; i < n - 1; i++)
                {
                    swapped = false;

                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;

                            swapped = true;
                        }
                    }
                    if (!swapped)
                        break;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} - {Total}");
            }
        }

        public class BasketballPlayer : Participant
        {

            public override bool IsExpelled
            {
                get
                {
                    if (_minuts == null) return false;
                    int count = 0;
                    for (int i = 0; i < _minuts.Length; i++)
                    {
                        if (_minuts[i] >= 5) count++;
                    }
                    for (int i = 0; i < _minuts.Length; i++)
                    {
                        if (this.Total > 2 * _minuts.Length || count > 0.1 * _minuts.Length)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public BasketballPlayer(string name, string surname) : base(name, surname) 
            {
                _minuts = new int[0];
            }

            public override void PlayMatch(int foul)
            {
                if (_minuts == null || foul < 0 || foul > 5) return;
                base.PlayMatch(foul);

            }
        }

        public class HockeyPlayer: Participant
        {
            private int _allminuts;
            private int _count;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _count++;
                _minuts = new int[0];
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_minuts == null) return false;
                    for (int i = 0; i < _minuts.Length; i++)
                    {
                        if (_minuts[i] >= 10) return true;
                    }
                    for (int i = 0; i < _minuts.Length; i++)
                    {
                        if (_count == 0) return false;
                        if (_minuts.Sum() > 0.1 * _allminuts / _count)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public override void PlayMatch(int minuts)
            {
                if (minuts < 0 || minuts > 5) return;
                base.PlayMatch(minuts);
                _allminuts += minuts;

            }
        }
    }
}
