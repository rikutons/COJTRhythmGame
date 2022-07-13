using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpritePresenter : MonoBehaviour
{
    [SerializeField]
    private MasterData.NoteImages noteImages;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public void Init(string code)
    {
        foreach(MasterData.NoteImage noteImage in noteImages.noteImages)
        {
            if(code == noteImage.code)
            {
                spriteRenderer.sprite = noteImage.sprite;
                if(code.EndsWith("rest")){
                    spriteRenderer.color += new Color(0, 0, 0, -0.7f);
                }
                break;
            }
        }
    }
}
