using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSelected : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public enum Selected
    {
        playerOne,
        playerTwo,
        playerThree,
        playerFour
    };

    public Selected mySelected;
    
    public bool isEntered;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isEntered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isEntered)
            return;
        else
        {
            isEntered = false;
            
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(isEntered)
            {
                if (CharacterManager.instance.draggingCharacter != null)
                {
                    if (mySelected == Selected.playerOne)
                        CharacterManager.instance.draggingCharacter.GetComponent<CharacterSelectionUI>().SelectOne(1);
                    if (mySelected == Selected.playerTwo)
                        CharacterManager.instance.draggingCharacter.GetComponent<CharacterSelectionUI>().SelectOne(2);
                    if (mySelected == Selected.playerThree)
                        CharacterManager.instance.draggingCharacter.GetComponent<CharacterSelectionUI>().SelectOne(3);
                    if (mySelected == Selected.playerFour)
                        CharacterManager.instance.draggingCharacter.GetComponent<CharacterSelectionUI>().SelectOne(4);
                }
            }
            // else
            // {
            //     if (CharacterManager.instance.draggingCharacter != null)
            //     {
            //         CharacterManager.instance.draggingCharacter.GetComponent<CharacterSelectionUI>().SelectOne(Convert.ToInt16(CharacterManager.instance.selectionCountForMobile));
            //         CharacterManager.instance.selectionCountForMobile += 0.25f;
            //         if (CharacterManager.instance.selectionCountForMobile == 5)
            //             CharacterManager.instance.selectionCountForMobile = 0;
            //     }
            // }
        }
    }
}
