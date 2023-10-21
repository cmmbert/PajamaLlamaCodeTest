using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthObserver
{
    public void OnHealthUpdated(int newHealth);
    public void OnMaxHealthUpdated(int newMaxHealth, int currentHealth);
}
