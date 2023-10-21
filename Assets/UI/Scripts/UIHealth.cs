using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealth : MonoBehaviour, IHealthObserver
{
    public GameObject UIHealthHeartPrefab;
    public Health HealthScriptToTrack;

    private List<UIHealthHeart> UIHearts = new();

    private void Awake()
    {
        HealthScriptToTrack.HealthObserver = this;
    }
    public void OnHealthUpdated(int newHealth)
    {
        foreach (var heart in UIHearts)
        {
            heart.SetState(newHealth > 0);
            newHealth--;
        }
    }

    public void OnMaxHealthUpdated(int newMaxHealth, int currentHealth)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        UIHearts.Clear();
        for (int i = 0; i < newMaxHealth; i++)
        {
            var heart = Instantiate(UIHealthHeartPrefab);
            heart.transform.SetParent(transform);
            UIHearts.Add(heart.GetComponent<UIHealthHeart>());
        }
    }
}
