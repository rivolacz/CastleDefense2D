using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abilities
{
    public abstract class Ability
    {
        public float ManaCost;

        public abstract void Cast();
    }
}
