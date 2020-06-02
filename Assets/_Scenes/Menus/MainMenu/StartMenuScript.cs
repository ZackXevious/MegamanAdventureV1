using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenuScript : MonoBehaviour {
	public GameObject initialMenu;
	public GameObject Settings;
	public GameObject SaveLoad;
	public GameObject About;

	public EventSystem events;

	public GameObject FirstInMain;
	public GameObject FirstInSettings;
	public GameObject FirstInSaveLoad;
	public GameObject FirstInAbout;
	//public GameObject stageSelect;
	// Use this for initialization
	void Start () {
		RenderSettings.skybox.SetFloat("_Rotation", 0);
		StartMenu ();
		//stageSelect.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
		RenderSettings.skybox.SetFloat("_Rotation", Time.time*10);
	}
	public void StartMenu(){
		initialMenu.SetActive (true);
		Settings.SetActive (false);
		SaveLoad.SetActive (false);
		About.SetActive (false);
		events.SetSelectedGameObject (FirstInMain);
	}
	public void SettingsMenu(){
		initialMenu.SetActive (false);
		Settings.SetActive (true);
		SaveLoad.SetActive (false);
		About.SetActive (false);
		events.SetSelectedGameObject (FirstInSettings);
	
	}
	public void LoadSaveMenu(){
		initialMenu.SetActive (false);
		Settings.SetActive (false);
		SaveLoad.SetActive (true);
		About.SetActive (false);
		events.SetSelectedGameObject (FirstInSaveLoad);

	}
	public void AboutMenu(){
		initialMenu.SetActive (false);
		Settings.SetActive (false);
		SaveLoad.SetActive (false);
		About.SetActive (true);
		events.SetSelectedGameObject (FirstInAbout);

	}
	public void LoadStage(string StageName){
		Debug.Log ("Attempting to load stage"+StageName);
		SceneManager.LoadScene (StageName);
	}
	public void ExitGame(){
		Application.Quit ();
	}
}
