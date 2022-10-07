using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Camera main1;
    [SerializeField] private Camera main2;


    private float time;

    private static TurnManager instance;

    private static int activePlayerIndex;
    [SerializeField] public float turnDuration;
    public float currentTurnTime;
    
    // Start is called before the first frame update
   private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            activePlayerIndex = 1;
        }
   
    }

    public bool IsItPlayerTurn(int index)
    {
        return index == activePlayerIndex;
    }

    public static TurnManager GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        currentTurnTime += Time.deltaTime; //1 unit per second

        if (currentTurnTime >= turnDuration)
        {
            if (activePlayerIndex == 1)
            {
                main1.depth = 0;
                main2.depth = 1;
            }
            if (activePlayerIndex == 2)
            {

                main1.depth = 1;
                main2.depth = 0;
            }
            ChangeTurn();
            currentTurnTime = 0;
        }
    }

    public static void ChangeTurn()
    {
        if (activePlayerIndex == 1)
        {

            activePlayerIndex = 2;

            Debug.Log("Changed player" + activePlayerIndex);
        }

        else if (activePlayerIndex == 2)
        {
            activePlayerIndex = 1;

            Debug.Log("Changed player" + activePlayerIndex);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
    if (activePlayerIndex == 1){
        //kamera player 1...
    } else if {
        //kamera palyer 2...
    }
    time += Time.deltaTime;
    }
}*/
}
