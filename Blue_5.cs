using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;

            public string Name
            {
                get
                {
                    return _name;
                }
            }
            public string Surname
            {
                get
                {
                    return _surname;
                }
            }
            public int Place
            {
                get
                {
                    return _place;
                }
            }

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place)
            {
                if (_place != 0) return;
                _place = place;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} - {Place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _sportsmenCount;

            public string Name => _name;
            public int SportsmenCount => _sportsmenCount;
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return null;
                    return _sportsmen;
                }
            }

            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 0;
                    int scores = 0;
                    foreach (var sportsman in _sportsmen)
                    {
                        switch (sportsman.Place)
                        {
                            case 1:
                                scores += 5;
                                break;
                            case 2:
                                scores += 4;
                                break;
                            case 3:
                                scores += 3;
                                break;
                            case 4:
                                scores += 2;
                                break;
                            case 5:
                                scores += 1;
                                break;
                            default:
                                break;
                        }
                    }
                    return scores;
                }

            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 0;
                    int top = 18;
                    foreach (var sportsman in _sportsmen)
                    {
                        int place = sportsman.Place == 0 ? 18 : sportsman.Place;
                        if (place < top)
                        {
                            top = place;
                        }
                    }
                    return top;
                }
            }


            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _sportsmenCount = 0;

            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team manChampion = null;
                Team womanChampion = null;

                foreach (var team in teams)
                {
                    if (team is ManTeam)
                    {
                        if (manChampion == null || team.GetTeamStrength() > manChampion.GetTeamStrength())
                            manChampion = team;
                    }
                    else if (team is WomanTeam)
                    {
                        if (womanChampion == null || team.GetTeamStrength() > womanChampion.GetTeamStrength())
                            womanChampion = team;
                    }
                }

                Team overallChampion = null;
                double maxStrength = double.MinValue;
                foreach (var team in teams)
                {
                    double strength = team.GetTeamStrength();
                    if (strength > maxStrength)
                    {
                        maxStrength = strength;
                        overallChampion = team;
                    }
                }

                return overallChampion;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || _sportsmen.Length == 0) return;
                if (_sportsmenCount < 6)
                {
                    _sportsmen[_sportsmenCount] = sportsman;
                    _sportsmenCount++;
                }
            }

            public void Add(params Sportsman[] sportsmen)
            {
                if (sportsmen == null || sportsmen.Length == 0) return;
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }


            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
                int n = teams.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                            {
                                Team temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                            }
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Команда: {_name}");
                Console.WriteLine($"Суммарный балл: {SummaryScore}");
                Console.WriteLine($"Наивысшее место: {TopPlace}");
                Console.WriteLine("Спортсмены:");

                if (_sportsmen != null && _sportsmen.Length > 0)
                {
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        _sportsmen[i].Print();
                    }
                }
            }

        }

        public class ManTeam: Team
        {
            public ManTeam(string name): base (name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || SportsmenCount == 0) return 0;

                double sum = 0;
                int count = 0;
                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null && sportsman.Place != 0)
                    {
                        sum += sportsman.Place;
                        count++;
                    }
                }

                return count == 0 ? 0 : 100 / (sum / count);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || SportsmenCount == 0) return 0;

                double sum = 0;
                double product = 1;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null && sportsman.Place != 0)
                    {
                        sum += sportsman.Place;
                        product *= sportsman.Place;
                        count++;
                    }
                }

                return count == 0 ? 0 : 100 * sum * count / product;
            }
        }
    }
}
