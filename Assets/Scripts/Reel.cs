using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [SerializeField] private RectTransform[] symbolOnReel;
    [SerializeField] private float exitPosition;
    [SerializeField] private float startPositionY;
    [SerializeField] private float spriteHeight;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private RectTransform mainCanvasRT;
    private float mainCanvasScale;

    private void Start()
    {
        mainCanvasScale = mainCanvasRT.lossyScale.y;
    }
    private void Update()
    {
        foreach (var symbol in symbolOnReel)
        {
            if (symbol.position.y <= exitPosition * mainCanvasScale)
            {
                MoveSymbolUp(symbol);
                ChangeSymbolSprite(symbol);
            }
        }
    }

    public void ResetSymbolsPosition(float reelStartPositionY)
    {
        foreach(var symbol in symbolOnReel)
        {
            var symbolPos = symbol.localPosition;
            var newPos = symbolPos.y + reelStartPositionY;
            symbol.localPosition = new Vector3(symbolPos.x, newPos);
        }
    }

    private void MoveSymbolUp(RectTransform symbol)
    {
        Debug.Log(symbol.position.y);
        var offset = symbol.position.y + startPositionY * mainCanvasScale;
        var newPos = new Vector3(symbol.position.x, offset);
        symbol.position = newPos;
    }
    private void ChangeSymbolSprite(RectTransform symbol)
    {
        var random = Random.Range(0, sprites.Length);
        symbol.GetComponent<Image>().sprite = sprites[random];
    }
}
