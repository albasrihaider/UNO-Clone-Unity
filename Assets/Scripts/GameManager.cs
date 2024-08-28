using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject newCard = Instantiate(cardPrefab);
        newCard.GetComponent<Card>().Initialize("Blue", "0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
