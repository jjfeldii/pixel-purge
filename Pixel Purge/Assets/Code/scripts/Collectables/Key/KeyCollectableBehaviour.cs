using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    public void OnCollected(GameObject player)
    {
        player.GetComponent<KeyController>().AddKey();
    }
}
