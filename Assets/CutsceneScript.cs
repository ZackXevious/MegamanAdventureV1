using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour {

	//Stagename Stuff
	public Text stageText;
	public string nameOfNextStage;

	//StoryFrameStuff
	public GameObject[] frames;
	public Sprite[] characters;
	public int[] CharacterList;
	public GameObject background;

	public GameObject container;

	public int currFrame;
	public string nextStage;

	// Use this for initialization
	void Start () {
		stageText.text = this.nameOfNextStage;
		if (characters [CharacterList [currFrame]] != null) {
			background.GetComponent<Image> ().sprite = characters [CharacterList [currFrame]];
			background.SetActive (true);
		}else{
			background.SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AdvanceFrame(){
		if (currFrame < frames.Length-1) {
			frames [currFrame].SetActive (false);
			currFrame++;
			frames [currFrame].SetActive (true);
			if(characters[CharacterList[currFrame]]!=null){
				background.GetComponent<Image> ().sprite=characters[CharacterList[currFrame]];
				background.SetActive(true);
			}else{
				background.SetActive(false);
			}

		} else {
			this.StageLoad (this.nextStage);
		}
	}
	public void StageLoad(string stage){
		container.SetActive (false);
		SceneManager.LoadScene (stage);
	}
}
