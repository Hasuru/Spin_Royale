using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("Sprite Information")]
    [SerializeField] private Sprite[] sprites;
    [SerializeField] public Image currentSprite;

    [Header("Animation Info")]
    [SerializeField] private float timeInterval;

    private bool slotStopped;
    private int spriteIndex;

    public void Start()
    {
        slotStopped = true;
    }

    public void Update()
    {
        if (slotStopped)
        {
            if (currentSprite.sprite != sprites[spriteIndex]) currentSprite.sprite = sprites[spriteIndex];
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        } 
        else if (!slotStopped)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public int RunSlot()
    {
        spriteIndex = Random.Range(0, sprites.Length);

        StartCoroutine(Spin());

        return spriteIndex;
    }

    private IEnumerator Spin()
    {
        slotStopped = false;
        float animTimer = 0f;

        while (animTimer < timeInterval)
        {
            int value = Random.Range(0, sprites.Length);
            currentSprite.sprite = sprites[value];
            animTimer += Time.deltaTime;

            Debug.Log(animTimer + "/" + timeInterval);

            yield return new WaitForSeconds(0.05f);
        }

        slotStopped = true;
    }
}
