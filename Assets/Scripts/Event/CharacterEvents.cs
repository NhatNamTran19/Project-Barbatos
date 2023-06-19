using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Event
{
    internal class CharacterEvents
    {
        public static UnityAction<GameObject, float> characterDamaged;

    }
}
