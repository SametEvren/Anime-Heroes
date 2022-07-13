using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    #region Singleton
    public static CharacterManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    
    public List<Character> Characters = new List<Character>();
    public GameObject characterPrefab;
    public GameObject panel;
    public GameObject selectedOne, selectedTwo, selectedThree, selectedFour;
    public GameObject draggingCharacter;
    public float selectionCountForMobile;
    private void Start()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            GameObject character = Instantiate(characterPrefab,panel.transform);
            character.GetComponent<CharacterSelectionUI>().character = Characters[i];
            FillUI(character,i);
        }
    }

    public void FillUI(GameObject character, int i)
    {
        character.GetComponent<CharacterSelectionUI>().name.text = Characters[i].name;
        character.GetComponent<CharacterSelectionUI>().artwork.sprite = Characters[i].artwork;
        character.GetComponent<CharacterSelectionUI>().damage.text = Characters[i].damage.ToString();
        character.GetComponent<CharacterSelectionUI>().health.text = Characters[i].health.ToString();
        character.GetComponent<CharacterSelectionUI>().id = Characters[i].id;
    }

    public void Play()
    {
        PlayerPrefs.SetInt("FirstPlayer",selectedOne.GetComponent<SelectedCharacterUI>().id);
        PlayerPrefs.SetInt("SecondPlayer",selectedTwo.GetComponent<SelectedCharacterUI>().id);
        PlayerPrefs.SetInt("ThirdPlayer",selectedThree.GetComponent<SelectedCharacterUI>().id);
        PlayerPrefs.SetInt("ForthPlayer",selectedFour.GetComponent<SelectedCharacterUI>().id);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }




    public void ChangeMusic()
    {
        MusicManager.Instance.ChangeSong(UnityEngine.Random.Range(0,MusicManager.Instance.songs.Length));
    }

    public void StopMusic()
    {
        if (MusicManager.Instance.volume == 1)
        {
            MusicManager.Instance.volume = 0;
            return;
        }

        if (MusicManager.Instance.volume == 0)
        {
            MusicManager.Instance.volume = 1;
            return;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
