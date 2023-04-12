using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Abilities
{
    public abstract class Ability
    {
        public abstract void Cast();
        public abstract float GetManaCost();
        public abstract void SetTarget(Vector2 target);
    }
}
