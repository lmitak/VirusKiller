using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyValue : MonoBehaviour {

    public Text lblEnemyValue;

	// Use this for initialization
	void Start () {
        //Text score = Instantiate(lblScore, transform.parent);
        //Animator animator = score.GetComponent<Animator>();
        //RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        //float animationClipLength = ac.animationClips[0].length;

        //Destroy(score, animationClipLength);
        //lblScore.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position);
        //GameObject container = new GameObject("container", typeof(RectTransform));
        //GameObject instance = Instantiate(container, Camera.main.WorldToScreenPoint(obj.transform.position), Quaternion.identity, this.gameObject.transform);
        //GameObject lblValueInstance = Instantiate(lblEnemyValue.gameObject, Camera.main.WorldToScreenPoint(new Vector3()), Quaternion.identity, instance.transform);
        //Debug.Log(lblValueInstance.transform.position);
        //lblValueInstance.GetComponent<RectTransform>().localPosition = new Vector3();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowEnemyValue(int value, Vector3 position)
    {
        GameObject container = new GameObject("container", typeof(RectTransform));
        GameObject containerInstance = Instantiate(container, Camera.main.WorldToScreenPoint(position), Quaternion.identity, this.gameObject.transform);

        Text lblEnemyValueInstance = Instantiate(lblEnemyValue, new Vector3(), Quaternion.identity, containerInstance.transform);
        lblEnemyValueInstance.GetComponent<RectTransform>().localPosition = new Vector3();
        lblEnemyValueInstance.text = value.ToString();
        Animator animator = lblEnemyValueInstance.GetComponent<Animator>();
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        float animationClipLength = ac.animationClips[0].length;

        Destroy(lblEnemyValueInstance, animationClipLength);
        Destroy(containerInstance, animationClipLength + 0.01f);
    }
}
