using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// keyboard layout is not a concern of this class
/// </summary>
public class KeyboardInteraction : MonoBehaviour
{
    public delegate void HoldBegan(KeyboardInteraction interaction, KeyCode keyCode);
    public delegate void Holding(KeyboardInteraction interaction, KeyCode keyCode, float dutation);
    public delegate void HoldEnding(KeyboardInteraction interaction, KeyCode keyCode, float dutation);

    public HoldBegan OnHoldBegan = null;
    public Holding OnHolding = null;
    public HoldEnding OnHoldEnding = null;

    public List<KeyCode> listeningKeys = new List<KeyCode>();
    protected Dictionary<KeyCode, float> holdingKeys = new Dictionary<KeyCode, float>();

    void Update()
    {
        for (int i = 0; i < this.listeningKeys.Count; i++)
        {
            KeyCode key = this.listeningKeys[i];
            
            if (Input.GetKey(key))
            {
                if (this.holdingKeys.ContainsKey(key))
                {
                    float duration = this.holdingKeys[key];
                    duration += Time.deltaTime;
                    this.holdingKeys[key] = duration;
                    this.OnHolding?.Invoke(this, key, this.holdingKeys[key]);
                }
                else
                {
                    this.holdingKeys.Add(key, 0.0f);
                    this.OnHoldBegan?.Invoke(this, key);
                }
            }
            else
            {
                if (this.holdingKeys.ContainsKey(key))
                {
                    float duration = this.holdingKeys[key];
                    this.holdingKeys.Remove(key);
                    this.OnHoldEnding?.Invoke(this, key, duration);
                }
            }
        }
        // TODO: suspended case while holding
    }

    public bool IsHolding(KeyCode key)
    {
        return this.holdingKeys.ContainsKey(key);
    }

    public void AddListeningKey(KeyCode key)
    {
        if (!this.listeningKeys.Contains(key))
        {
            this.listeningKeys.Add(key);
        }
    }

    public float HoldingDuration(KeyCode key)
    {
        if (!this.holdingKeys.ContainsKey(key))
        {
            return -1.0f;
        }
        return this.holdingKeys[key];
    }

    public bool HoldingOver(KeyCode key, float seconds)
    {
        if (!this.holdingKeys.ContainsKey(key))
        {
            return false;
        }
        return this.holdingKeys[key] >= seconds;
    }
}
