using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] public Image currentSprite;

    public int RunSlot()
    {
        int value = Random.Range(0, sprites.Length);

        currentSprite.sprite = sprites[value];

        return value;
    }
}
