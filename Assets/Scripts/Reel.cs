using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    [SerializeField] private RectTransform[] symbolOnReel;
    private float exitPos = 195;

    private void Update()
    {
        foreach (var symbol in symbolOnReel)
        {
            if (symbol.position.y <= exitPos)
            {
                Debug.Log(symbol.position.y);
                var offset = symbol.position.y + 220 * 4;
                var newPos = new Vector3(symbol.position.x, offset);
                symbol.position = newPos;
            }
        }
    }
}
