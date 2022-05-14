using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;
    [SerializeField] private RectTransform[] symbolOnReel;
    [SerializeField] private float exitPosition;
    [SerializeField] private float startPositionY;
    [SerializeField] private float spriteHeight;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] int reelId;

    [SerializeField] private RectTransform mainCanvasRT;
    private int currentSymbolIndex = 0;
    private int currentFinalSet = 0;
    private float mainCanvasScale;
    private ReelState reelState = ReelState.Stop;

    public int ReelId => reelId;
    internal ReelState ReelState { get => reelState; set => reelState = value; } 

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
        currentSymbolIndex = 0;
        if (currentFinalSet < gameConfig.FinalScreens.Length - 1)
        {
            currentFinalSet++;
        } else
        {
            currentFinalSet = 0;
        }
        foreach(var symbol in symbolOnReel)
        {
            var symbolPos = symbol.localPosition;
            var newPos = symbolPos.y + reelStartPositionY;
            symbol.localPosition = new Vector3(symbolPos.x, newPos);
        }
    }

    private Sprite GetRandomSymbol()
    {
        var random = Random.Range(0, gameConfig.Symbols.Length);
        var sprite = gameConfig.Symbols[random].SymbolImage;
        return sprite;
    }

    private Sprite GetFinalScreenSymbol()
    {
        var finalScreenSymbolIndex = currentSymbolIndex + (reelId - 1) * gameConfig.VisibleSymbolsOnReel;
        var currentFinalScreen = gameConfig.FinalScreens[currentFinalSet].FinalScreen;
        if (finalScreenSymbolIndex >= currentFinalScreen.Length)
        {
            finalScreenSymbolIndex = 0;
        }
        var newSymbol = gameConfig.Symbols[currentFinalScreen[finalScreenSymbolIndex]];
        return newSymbol.SymbolImage;
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
        if (ReelState == ReelState.Stopping || ReelState == ReelState.ForceStop)
        {
            symbol.GetComponent<Image>().sprite = GetFinalScreenSymbol();
            currentSymbolIndex++;
        } else
        {
            symbol.GetComponent<Image>().sprite = GetRandomSymbol();
        }
    }
}
