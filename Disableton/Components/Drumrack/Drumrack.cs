using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disableton.Components.Drumrack
{
    public class Drumrack
    {
        public Dictionary<int, Sample> Rack
        {
            get => _rack;
            private set => _rack = value;
        }

        private Dictionary<int, Sample> _rack = new Dictionary<int, Sample>();

        public Drumrack() 
        {

        }

        public void Play(int key)
        {
            if (Rack.ContainsKey(key))
            {
                Rack[key].Play();
            }
        }

        public void Add(int key, Sample value)
        {
            Rack[key] = value;
        }

        public bool Remove(int key)
        {
            return Rack.Remove(key);
        }

        public void Clear()
        {
            foreach (var sample in Rack)
            {
                sample.Value?.Dispose();
            }
        }
    }
}
