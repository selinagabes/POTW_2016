using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POTW_5
{
    public class Trie
    {
        TrieNode _root;

        public Trie()
        {
            _root = new POTW_5.TrieNode(null, '^');      //^ being the spcieal first character for a Trie
        }

        public void Insert(string s)
        {
            var prefix = Prefix(s);
            var current = prefix;

            foreach (char c in s)
                current = current.AddChild(c);


            current.AddChild('$');        //special char '$' denoting end of string.

        }

        public bool SearchForMaliciousPrefix(string s)
        {
            var prefix = Prefix(s);
            if (prefix.FindChildNode('$') != null)
            {
                return true;
            }
            return false;

        }

        public TrieNode Prefix(string s)
        {
            TrieNode current = _root;
            TrieNode prefix = current;

            foreach (var c in s)
            {
                if (current.ContainsKey(c))
                {
                    current = current.FindChildNode(c);
                    prefix = current; ;
                }
            }
            return prefix;
        }


    }
    public class TrieNode
    {
        public bool Terminator { get; set; }
        public Dictionary<char, TrieNode> Children { get; set; }

        public TrieNode(TrieNode parent, char val)
        {

            this.Children = new Dictionary<char, TrieNode>();
        }
        public bool IsLeaf()
        {
            return NumChildren() == 0;
        }

        public TrieNode FindChildNode(char c)
        {
            if (Children.ContainsKey(c))
            {
                return Children[c];
            }

            return null;        //Not found.
        }
        public List<char> PrefixMatches()
        {
            if (IsLeaf())
            {
                return new List<char>();
            }
            else
            {
                List<char> values = new List<char>();
                foreach (TrieNode t in Children.Values)
                {
                    values.AddRange(t.PrefixMatches());

                }
                if (this.Terminator)
                {
                    values.Add('$');
                }
                return values;
            }
        }

        public TrieNode AddChild(char key)
        {
            if (Children.ContainsKey(key))
                return Children[key];
            else
            {
                TrieNode newChild = new POTW_5.TrieNode(this, key);
                Children.Add(key, newChild);
                return newChild;
            }
        }

        public int NumChildren()
        {
            return Children.Count;
        }

        public bool ContainsKey(char key)
        {
            return Children.ContainsKey(key);
        }
    }

    public class Prefix
    {
        TrieNode _root;
        TrieNode _match;
        string prefix;

        public Prefix(TrieNode root)
        {
            _root = root;
            _match = root;
        }

        public String GetPrefix()
        {
            return prefix;
        }

        public bool NextMatch(char next)
        {
            if (_match.ContainsKey(next))
            {
                _match = _match.FindChildNode(next);
                prefix += next;
                return true;
            }
            return false;
        }

        public List<char> GetPrefixMatches()
        {
            return _match.PrefixMatches();
        }

    }
}
