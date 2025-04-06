using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public abstract class WaterJump 
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Participant[] newarr = new Participant[_participants.Length + 1];
                for(int i = 0; i < _participants.Length; i++)
                {
                    newarr[i] = _participants[i];
                }
                newarr[_participants.Length] = participant;
                _participants = newarr;
            }

            public void Add(Participant[] participants)
            {
                if (_participants == null || participants == null || participants.Length == 0) return;
                foreach(Participant participant in participants)
                {
                    Add(participant);
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (this.Participants.Length <= 3 || this.Participants == null) return default(double[]);
                    double[] prizes = new double[3];
                    prizes[0] = 0.5 * this.Bank;
                    prizes[1] = 0.3 * this.Bank;
                    prizes[2] = 0.2 * this.Bank;
                    return prizes;

                }
            }

        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }


            public override double[] Prize
            {
                get
                {
                    if (this.Participants.Length < 3 || this.Participants == null) return default(double[]);
                    double[] prizes = new double[Math.Min(this.Participants.Length, 10)];

                    prizes[0] = Bank * 0.40;
                    prizes[1] = Bank * 0.25;
                    prizes[2] = Bank * 0.15; 

                    int halfCount = this.Participants.Length / 2;
                    int topCount = Math.Min(Math.Max(halfCount, 3), 10); 

                  
                    double nPercent = 20.0 / topCount;

                    for (int i = 3; i < topCount; i++)
                    {
                        prizes[i] = Bank * nPercent / 100;
                    }

                    return prizes;
                }
            }
        }
        public struct Participant
        {

            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _lastadd;


            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return null; 
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < copy.GetLength(0); i++)
                    {
                        for (int j = 0; j < copy.GetLength(1); j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return 0;
                    int sum = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            sum += _marks[i, j];
                        }
                    }
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _lastadd = -1;
            }

            public void Jump(int[] result)
            {
                if (_marks == null || result == null) return;
                if (_lastadd >= _marks.GetLength(0)) return;
                for (int i = 0; i < 5; i++)
                {
                    _marks[_lastadd + 1, i] = result[i];

                }
                _lastadd++;
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
                        if (array[j].TotalScore < array[j + 1].TotalScore)
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
                Console.WriteLine($"{_name} {_surname} - {TotalScore} ");
            }

        }
    }
}
