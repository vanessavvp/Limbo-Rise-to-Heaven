using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public GameObject _new_game_no_option, _load_game_yes_option;

    public void NewGamePanel() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_new_game_no_option);
    }

    public void LoadGamePanel() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_load_game_yes_option);
    }



}
