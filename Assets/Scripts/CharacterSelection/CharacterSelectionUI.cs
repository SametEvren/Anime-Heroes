using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Character character;
    public TextMeshProUGUI name, damage, health;
    public Image artwork;
    public Vector3 originalPosition;
    public int id;

    public enum Selected
    {
        playerOne,
        playerTwo,
        playerThree,
        playerFour
    };

    public Selected mySelected;

    public void SelectOne(int i)
    {
        if(i == 1)
            mySelected = Selected.playerOne;
        if(i == 2)
            mySelected = Selected.playerTwo;
        if(i == 3)
            mySelected = Selected.playerThree;
        if(i == 4)
            mySelected = Selected.playerFour;
    }
    
    
    private void Start()
    {
        StartCoroutine(GetPosition());
        IEnumerator GetPosition()
        {
            yield return new WaitForSeconds(0.1f);
            originalPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        CharacterManager.instance.draggingCharacter = gameObject;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {
        // GetComponent<RectTransform>().anchoredPosition = originalPosition;

        StartCoroutine(Fit());
        IEnumerator Fit()
        {
            yield return new WaitForSeconds(0.01f);
            GetComponent<RectTransform>().anchoredPosition = originalPosition;
            if (mySelected == Selected.playerOne)
            {
                CharacterManager.instance.selectedOne.GetComponent<SelectedCharacterUI>().character =
                    GetComponent<CharacterSelectionUI>().character;
                CharacterManager.instance.selectedOne.GetComponent<SelectedCharacterUI>().artwork.sprite =
                    GetComponent<CharacterSelectionUI>().artwork.sprite;
                CharacterManager.instance.selectedOne.GetComponent<SelectedCharacterUI>().name.text =
                    GetComponent<CharacterSelectionUI>().name.text;
                CharacterManager.instance.selectedOne.GetComponent<SelectedCharacterUI>().damage.text =
                    GetComponent<CharacterSelectionUI>().damage.text;
                CharacterManager.instance.selectedOne.GetComponent<SelectedCharacterUI>().health.text =
                    GetComponent<CharacterSelectionUI>().health.text;
                CharacterManager.instance.selectedOne.GetComponent<SelectedCharacterUI>().id =
                    GetComponent<CharacterSelectionUI>().id;
            }
            if (mySelected == Selected.playerTwo)
            {
                CharacterManager.instance.selectedTwo.GetComponent<SelectedCharacterUI>().character =
                    GetComponent<CharacterSelectionUI>().character;
                CharacterManager.instance.selectedTwo.GetComponent<SelectedCharacterUI>().artwork.sprite =
                    GetComponent<CharacterSelectionUI>().artwork.sprite;
                CharacterManager.instance.selectedTwo.GetComponent<SelectedCharacterUI>().name.text =
                    GetComponent<CharacterSelectionUI>().name.text;
                CharacterManager.instance.selectedTwo.GetComponent<SelectedCharacterUI>().damage.text =
                    GetComponent<CharacterSelectionUI>().damage.text;
                CharacterManager.instance.selectedTwo.GetComponent<SelectedCharacterUI>().health.text =
                    GetComponent<CharacterSelectionUI>().health.text;
                CharacterManager.instance.selectedTwo.GetComponent<SelectedCharacterUI>().id =
                    GetComponent<CharacterSelectionUI>().id;
            }
            if (mySelected == Selected.playerThree)
            {
                CharacterManager.instance.selectedThree.GetComponent<SelectedCharacterUI>().character =
                    GetComponent<CharacterSelectionUI>().character;
                CharacterManager.instance.selectedThree.GetComponent<SelectedCharacterUI>().artwork.sprite =
                    GetComponent<CharacterSelectionUI>().artwork.sprite;
                CharacterManager.instance.selectedThree.GetComponent<SelectedCharacterUI>().name.text =
                    GetComponent<CharacterSelectionUI>().name.text;
                CharacterManager.instance.selectedThree.GetComponent<SelectedCharacterUI>().damage.text =
                    GetComponent<CharacterSelectionUI>().damage.text;
                CharacterManager.instance.selectedThree.GetComponent<SelectedCharacterUI>().health.text =
                    GetComponent<CharacterSelectionUI>().health.text;
                CharacterManager.instance.selectedThree.GetComponent<SelectedCharacterUI>().id =
                    GetComponent<CharacterSelectionUI>().id;
            }
            if (mySelected == Selected.playerFour)
            {
                CharacterManager.instance.selectedFour.GetComponent<SelectedCharacterUI>().character =
                    GetComponent<CharacterSelectionUI>().character;
                CharacterManager.instance.selectedFour.GetComponent<SelectedCharacterUI>().artwork.sprite =
                    GetComponent<CharacterSelectionUI>().artwork.sprite;
                CharacterManager.instance.selectedFour.GetComponent<SelectedCharacterUI>().name.text =
                    GetComponent<CharacterSelectionUI>().name.text;
                CharacterManager.instance.selectedFour.GetComponent<SelectedCharacterUI>().damage.text =
                    GetComponent<CharacterSelectionUI>().damage.text;
                CharacterManager.instance.selectedFour.GetComponent<SelectedCharacterUI>().health.text =
                    GetComponent<CharacterSelectionUI>().health.text;
                CharacterManager.instance.selectedFour.GetComponent<SelectedCharacterUI>().id =
                    GetComponent<CharacterSelectionUI>().id;
            }
            
            
        }
        
    }
}
