using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Blue_1
    {
        public class Response
        {

            private string _name;
            protected int _votes;


            public string Name { get { return _name; } }
            public int Votes => _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }


            public virtual int CountVotes(Response[] response)
            {
                int count = 0;
                foreach (var item in response)
                {
                    if (item.Name == this.Name)
                    {
                        count++;
                    }
                }

                _votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name} - {Votes}");
            }
        }

        public class HumanResponse: Response
        {
            public string _surname;

            public string Surname => _surname;

            public HumanResponse(string surname, string name): base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                int count = 0;
                foreach (var response in responses)
                {
                    if (response is HumanResponse humanResponse && humanResponse.Name == Name && humanResponse.Surname == Surname)
                    {
                        count++;
                    }
                }

                _votes = count;
                return count;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {Surname} - {Votes}");
            }
        }
    }
}
