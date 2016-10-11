using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Gabriele, Selina 
 * October 2016
 * POTW 4
 * Use disjoint sets to find friend sets within CS
 * Input: N, N*Friend Sets (2 people), M, M*Friendship checks
 * Output: you either can or can't sit with us
 */
namespace POTW_4
{
    public class Program
    {

        static void Main(string[] args)
        {
            int numOfFriendships = Int32.Parse(Console.ReadLine());
            List<Person> Homies = new List<Person>();                           //For access to the Person objects created
            DisjointSet Squads = new DisjointSet();
            Person p1 = new Person();
            Person p2 = new Person();
            for (int i = 0; i < numOfFriendships; i++)
            {
                string[] friends = Console.ReadLine().Split();
                if (!Homies.Any(x => x.Name.Equals(friends[0].ToLower())))            //If we haven't already seen this homie
                {
                   
                    p1 = new Person(friends[0].ToLower());
                    Homies.Add(p1);
                    Squads.MakeSet(p1);

                }
                else
                    p1 = Homies.Single(x => x.Name.Equals(friends[0].ToLower()));   //grab the homie if we have

                if (!Homies.Any(x => x.Name.Equals(friends[1].ToLower())))
                {

                    p2 = new Person(friends[1].ToLower());
                    Homies.Add(p2);
                    Squads.MakeSet(p2);

                }
                else
                    p2 = Homies.Single(x => x.Name.Equals(friends[1].ToLower()));
                Squads.Union(p1, p2);
            }

            int numOfFriendChecks = Int32.Parse(Console.ReadLine());
            string[] areTheyFriends = new string[numOfFriendChecks];                                    //store the results for clean output
            for (int j = 0; j < numOfFriendChecks; j++)
            {
                string[] rollsWith = Console.ReadLine().Split();
                p1 = Homies.Single(x => x.Name.Equals(rollsWith[0].ToLower()));
                p2 = Homies.Single(x => x.Name.Equals(rollsWith[1].ToLower()));
                if (p1 == null || p2 == null)                                                              //make sure they are referring to people in the forest
                {
                    Console.WriteLine("You may have messed up the spelling, try again.");
                    --j;
                }
                else
                    areTheyFriends[j] = Squads.IsUnioned(p1, p2) ? "yes" : "no";                            
            }
            Console.WriteLine();
            for (int k = 0; k < numOfFriendChecks; k++)
            {
                Console.WriteLine("{0}", areTheyFriends[k]);                                                //print out the stored results
            }
            Console.ReadLine();

        }
    }

    public class DisjointSet
    {
        private Dictionary<Person, Node> Friends = new Dictionary<Person, Node>();

        public void MakeSet(Person friend)
        {
            Friends.Add(friend, new Node(friend, 0));
        }

        public bool IsUnioned(Person x, Person y)
        {
            Person xSet = Find(x);
            Person ySet = Find(y);
            Node xNode = Friends.First(f => f.Key.Equals(xSet)).Value;
            Node yNode = Friends.First(f => f.Key.Equals(ySet)).Value;

            if (xNode.Parent == yNode.Parent)                                        //Same parents mean same squad
                return true;                                                        //they roll with each other

            return false;
        }
        public void Union(Person x, Person y)
        {
            Person xSet = Find(x);
            Person ySet = Find(y);
            if (xSet == null || ySet == null || xSet == ySet)
                return;

            Node xNode = Friends.First(f => f.Key.Equals(xSet)).Value;
            Node yNode = Friends.First(f => f.Key.Equals(ySet)).Value;
            if (xNode.Rank < yNode.Rank)
                xNode.Parent = y;
            else if (xNode.Rank > yNode.Rank)
                yNode.Parent = x;
            else
            {
                yNode.Parent = x;
                xNode.Rank++;
            }
        }

        private Person Find(Person x)
        {
            var node = Friends.First(f => f.Key.Equals(x)).Value;

            if (node == null) return null;

            if (!node.Parent.Equals(x))
                node.Parent = Find(node.Parent);

            return node.Parent;
        }
    }
    //Class to store the names of people with checking for duplicates
    public class Person
    {
      
        public string Name;
        public Person() { }
        public Person(string name)
        {
            Name = name;
        }

    }
    //Nodes used for the disjoint set
    public class Node
    {
        public int Rank;
        public Person Parent;
        public Node(Person parent, int rank)
        {
            Parent = parent;
            Rank = rank;
        }
    }
}
