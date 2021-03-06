using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public interface INpcInterface
    {
        void OnTakeDamage();
        void OnTakeDamage(float damage);

        void OnRespawn();

    }
}

