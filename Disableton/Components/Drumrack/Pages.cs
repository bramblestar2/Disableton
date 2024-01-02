using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disableton.Components.Drumrack
{
    public class Pages
    {
        public List<Drumrack> Drumracks
        {
            get => _drumracks;
            private set => _drumracks = value;
        }

        private List<Drumrack> _drumracks = new List<Drumrack>();

        public Pages()
        {

        }

        public void RemovePage(int page)
        {
            if (PageInRange(page))
                Drumracks.RemoveAt(page);
        }

        public void AddPage()
        {
            Drumracks.Add(new Drumrack());
        }

        public void AddSample(int page, int key, Sample sample)
        {
            if (PageInRange(page))
            {
                Drumracks[page].Add(key, sample);
            }
        }

        public void Clear()
        {
            foreach (var drumrack in Drumracks)
                drumrack.Clear();

            Drumracks.Clear();
        }

        private bool PageInRange(int page)
        {
            if (page > 0 &&
                page < Drumracks.Count)
            {
                return true;
            }
            return false;
        }
    }
}
