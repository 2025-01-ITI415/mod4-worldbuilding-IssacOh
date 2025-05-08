using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggCollector : MonoBehaviour
{
    public Text countText;
    public GameObject pickupPromptUI; // ← Assign in Inspector

    private int count = 0;
    private Egg currentNearbyEgg;

    void Start()
    {
        SetCountText();
        pickupPromptUI.SetActive(false); // Hide at start
    }


    void Update()
    {
        if (currentNearbyEgg != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyEgg.Collect();
            count++;
            SetCountText();
            currentNearbyEgg = null;
        }
    }

    void SetCountText()
    {
        countText.text = "Eggs Collected: " + count.ToString() + "/3";
    }

    void OnTriggerEnter(Collider other)
    {
        Egg egg = other.GetComponent<Egg>();
        if (egg != null && !egg.IsCollected)
        {
            egg.Activate(); // Make it glow or something
            currentNearbyEgg = egg;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Egg egg = other.GetComponent<Egg>();
        if (egg != null && egg == currentNearbyEgg)
        {
            egg.Deactivate(); // Hide world-space prompt
            currentNearbyEgg = null;
        }
    }

}
