using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class PlaceObjectInSlotMiniGame : MiniGame
{

    [SerializeField] private Vector2[] possibleObjectStarts = new Vector2[4];
    [SerializeField] private float onSpotTolerance = 0.5f;
    private float _toleranceCounter = 0;

    public BoxCollider2D slotColldier;
    public GameObject objectToPut;
    private bool _objectInSlot = false;

    private void Start() {
        SetupGame();
    }
    public override void SetupGame()
    {
        slotColldier.gameObject.transform.position = Vector2.zero;
        int spot = Random.Range(0, possibleObjectStarts.Length - 1);
        objectToPut.transform.position = possibleObjectStarts[spot];

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(slotColldier.OverlapPoint(objectToPut.transform.position))
        {
            //In slot
            _objectInSlot = true;
        }
        else
            _objectInSlot = false;

        if(_objectInSlot)
        {
            _toleranceCounter += Time.deltaTime;
        }
        else 
            _toleranceCounter = 0.0f;

        if(_toleranceCounter > onSpotTolerance)
        {
            WinGame();
            print("Game won");
        }
            
            

    }

    private void OnDrawGizmosSelected() {
        for(int i = 0; i < possibleObjectStarts.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(possibleObjectStarts[i],0.5f);
        }

        if(_objectInSlot)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(slotColldier.transform.position,Vector3.one);
        }
    }
}
