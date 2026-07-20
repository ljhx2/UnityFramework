using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private ResourceManagerAddressable _resourceManager;

    public void Init(ResourceManagerAddressable resourceManager)
    {
        _resourceManager = resourceManager;
    }
  
}
