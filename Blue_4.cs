using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;


            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, copy.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        sum += _scores[i];
                    }
                    return sum;
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                int[] arr = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                {
                    arr[i] = _scores[i];
                }
                arr[arr.Length - 1] = result;
                _scores = arr;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} - {TotalScore}");
            }

        }

        public class WomanTeam: Team
        {
            public WomanTeam(string name) : base(name) { }
        }

        public class ManTeam: Team
        {
            public ManTeam(string name) : base(name) { }
        }
        public class Group
        {
            private string _name;
            private ManTeam[] _manteams;
            private WomanTeam[] _womanteams;
            private int _manteamsCount;
            private int _womanteamsCount;

            public string Name => _name;
            public ManTeam[] ManTeams => _manteams;
            public WomanTeam[] WomanTeams => _womanteams;
            public Group(string name)
            {
                _name = name;
                _manteams = new ManTeam[12];
                _womanteams = new WomanTeam[12];
                _manteamsCount = 0;
                _womanteamsCount = 0;
            }

            public void Add(Team team)
            {
                if(team is ManTeam manteam && _manteamsCount < _manteams.Length)
                {
                    _manteams[_manteamsCount++] = manteam;
                }
                else if(team is WomanTeam womanteam && _womanteamsCount < _womanteams.Length)
                {
                    _womanteams[_womanteamsCount++] = womanteam;
                }
            }

            public void Add(Team[] team)
            {
                if (team.Length == 0 || team == null) return;
                for (int i = 0; i < team.Length; i++)
                {
                    Add(team[i]);
                }
            }

            public void SortPart(Team[] _teams)
            {
                if (_teams == null || _teams.Length == 0) return;
                for (int i = 0; i < _teams.Length - 1; i++)
                {
                    for (int j = 0; j < _teams.Length - 1 - i; j++)
                    {
                        if (_teams[j].TotalScore < _teams[j + 1].TotalScore)
                        {
                            Team temp = _teams[j];
                            _teams[j] = _teams[j + 1];
                            _teams[j + 1] = temp;
                        }
                    }
                }
            }

            public void Sort()
            {
                SortPart(_womanteams);
                SortPart(_manteams);
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group finals = new Group("Финалисты");
                finals.Add(MergePart(group1._manteams, group2._manteams, size));
                finals.Add(MergePart(group1._womanteams, group2._womanteams, size));
                return finals;
            }

            public static Team[] MergePart(Team[] team1, Team[] team2, int size)
            {
                Team[] mergedteam = new Team[size];
                int halfSize = size / 2;
                int i = 0;
                int j = 0;
                int k = 0;

                while (i < halfSize && j < halfSize)
                {
                    if (team1[i].TotalScore >= team2[j].TotalScore)
                    {
                        mergedteam[k++] = team2[i];
                        i++;
                    }
                    else
                    {
                        mergedteam[k++] = team1[j];
                        j++;
                    }
                }
                while (i < halfSize)
                {
                    mergedteam[k++] = team2[i];
                    i++;
                }
                while (j < halfSize)
                {
                    mergedteam[k++] = team1[j];
                    j++;
                }
                return mergedteam;
            }

            public void Print(Team[] _teams)
            {
                Console.WriteLine($"{Name} {_teams}");
            }


        }
    }
}
