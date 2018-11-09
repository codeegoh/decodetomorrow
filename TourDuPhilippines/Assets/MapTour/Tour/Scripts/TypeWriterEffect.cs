using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeWriterEffect : MonoBehaviour {

	public float delay = 0.05f;
	public string fullText;
	private string currentText = "";
    public GameObject claimButton;

    public GameObject otherObject;
    Animator otherAnimator;
    void Awake()
    {
        otherAnimator = otherObject.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () 
    {
        claimButton.SetActive(false);
        StartCoroutine(ShowText());
        Button claimButtonClick = claimButton.GetComponent<Button>();
        claimButtonClick.onClick.AddListener(LoadMapScene);
	}
	
	IEnumerator ShowText()
    {
		for(int i = 0; i < fullText.Length; i++)
        {
			currentText = fullText.Substring(0,i);
			this.GetComponent<Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
        claimButton.SetActive(true);
	}

    void LoadMapScene()
    {
        otherAnimator.SetBool("isDoneGreet", true);
        StartCoroutine(ShowFarewell());

    }

    IEnumerator ShowFarewell()
    {
        yield return new WaitForSeconds(3.5F);
        otherAnimator.SetBool("isDoneGreet", false);
        if(otherAnimator.GetBool("isDoneGreet")==false)
            SceneManager.LoadScene("Map");
    }
}
