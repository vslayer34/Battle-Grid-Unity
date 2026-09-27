using System;
using UnityEngine;
using UnityEngine.VFX;

using Random = UnityEngine.Random;

[Serializable]
public class MuzzleEffectsHandler: MonoBehaviour
{
    [SerializeField]
    private VisualEffect _visualEffects;

    [SerializeField, Range(0.0f, 1.0f)]
    private float _spawnRate;



    // Game Loop Methods---------------------------------------------------------------------------

    public void FireVisualEffects()
    {
        float chance = Random.Range(0.0f, _spawnRate);

        if (chance <= _spawnRate)
        {
            _visualEffects.Play();
        }
    }
}