using UnityEngine;
using System.Collections;

public class HandSpawner : MonoBehaviour
{
    public GameObject vertHandPrefab;
    public GameObject horizHandPrefab;

    private GameObject currentHand;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left Click = Vertical Hand
        {
            SpawnHand(vertHandPrefab);
        }
        else if (Input.GetMouseButtonDown(1)) // Right Click = Horizontal Hand
        {
            SpawnHand(horizHandPrefab);
        }
    }

    void SpawnHand(GameObject handPrefab)
    {
        // If a hand already exists, animate its disappearance and destroy it
        if (currentHand != null)
        {
            StartCoroutine(DestroyWithAnimation(currentHand));
        }

        // Get mouse position in world space
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPos.z = 0f;

        // Instantiate the new hand prefab
        currentHand = Instantiate(handPrefab, spawnPos, Quaternion.identity);

        // Start auto-destroy after 5 seconds
        StartCoroutine(AutoDestroyAfterTime(currentHand, 5f));
    }

    IEnumerator AutoDestroyAfterTime(GameObject hand, float time)
    {
        yield return new WaitForSeconds(time);
        if (hand != null)
        {
            StartCoroutine(DestroyWithAnimation(hand));
        }
    }

    IEnumerator DestroyWithAnimation(GameObject hand)
    {
        Animator anim = hand.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Disappear");
            yield return new WaitForSeconds(0.25f); // match the Disappear animation length
        }

        if (hand != null)
        {
            Destroy(hand);
        }
    }
}
