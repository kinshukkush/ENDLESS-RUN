using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Particle Effect Manager - Handles visual effects for modern UI feedback
/// </summary>
public class ParticleEffectManager : MonoBehaviour
{
    public static ParticleEffectManager instance { get; private set; }

    [Header("Particle Prefabs")]
    public GameObject coinCollectEffect;
    public GameObject powerupEffect;
    public GameObject obstacleHitEffect;
    public GameObject levelUpEffect;
    public GameObject comboEffect;

    [Header("Pool Settings")]
    public int poolSize = 20;

    private Dictionary<string, Queue<GameObject>> effectPools = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, GameObject> effectPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initialize effect pools
    /// </summary>
    private void InitializePools()
    {
        RegisterEffect("CoinCollect", coinCollectEffect);
        RegisterEffect("Powerup", powerupEffect);
        RegisterEffect("ObstacleHit", obstacleHitEffect);
        RegisterEffect("LevelUp", levelUpEffect);
        RegisterEffect("Combo", comboEffect);
    }

    /// <summary>
    /// Register an effect and create its pool
    /// </summary>
    private void RegisterEffect(string effectName, GameObject prefab)
    {
        if (prefab == null) return;

        effectPrefabs[effectName] = prefab;
        effectPools[effectName] = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            effectPools[effectName].Enqueue(obj);
        }
    }

    /// <summary>
    /// Play effect at position
    /// </summary>
    public void PlayEffect(string effectName, Vector3 position, Quaternion rotation = default)
    {
        if (!effectPools.ContainsKey(effectName) || effectPools[effectName].Count == 0)
        {
            // Pool is empty, create new instance
            if (effectPrefabs.ContainsKey(effectName) && effectPrefabs[effectName] != null)
            {
                GameObject newObj = Instantiate(effectPrefabs[effectName], transform);
                effectPools[effectName].Enqueue(newObj);
            }
            else
            {
                Debug.LogWarning($"Effect {effectName} not found!");
                return;
            }
        }

        GameObject effect = effectPools[effectName].Dequeue();
        effect.transform.position = position;
        effect.transform.rotation = rotation == default ? Quaternion.identity : rotation;
        effect.SetActive(true);

        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            StartCoroutine(ReturnToPoolAfterPlay(effectName, effect, ps.main.duration));
        }
        else
        {
            // If no particle system, return after 2 seconds
            StartCoroutine(ReturnToPoolAfterPlay(effectName, effect, 2f));
        }
    }

    /// <summary>
    /// Return effect to pool after playing
    /// </summary>
    private System.Collections.IEnumerator ReturnToPoolAfterPlay(string effectName, GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (effect != null)
        {
            effect.SetActive(false);
            if (effectPools.ContainsKey(effectName))
            {
                effectPools[effectName].Enqueue(effect);
            }
        }
    }

    /// <summary>
    /// Play coin collect effect
    /// </summary>
    public void PlayCoinCollectEffect(Vector3 position)
    {
        PlayEffect("CoinCollect", position);
    }

    /// <summary>
    /// Play powerup effect
    /// </summary>
    public void PlayPowerupEffect(Vector3 position)
    {
        PlayEffect("Powerup", position);
    }

    /// <summary>
    /// Play obstacle hit effect
    /// </summary>
    public void PlayObstacleHitEffect(Vector3 position)
    {
        PlayEffect("ObstacleHit", position);
    }

    /// <summary>
    /// Play level up effect
    /// </summary>
    public void PlayLevelUpEffect(Vector3 position)
    {
        PlayEffect("LevelUp", position);
    }

    /// <summary>
    /// Play combo effect
    /// </summary>
    public void PlayComboEffect(Vector3 position)
    {
        PlayEffect("Combo", position);
    }

    /// <summary>
    /// Clear all active effects
    /// </summary>
    public void ClearAllEffects()
    {
        foreach (var pool in effectPools.Values)
        {
            foreach (var effect in pool)
            {
                if (effect != null && effect.activeSelf)
                {
                    effect.SetActive(false);
                }
            }
        }
    }
}
