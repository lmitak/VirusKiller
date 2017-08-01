using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyValueDisplay : MonoBehaviour {

    public Text lblEnemyValue;

    public void ShowEnemyValue(int value, Vector3 position)
    {
        //GameObject container = new GameObject("container", typeof(RectTransform));
        //GameObject containerInstance = Instantiate(container, Camera.main.WorldToScreenPoint(position), Quaternion.identity, this.gameObject.transform);

        //Text lblEnemyValueInstance = Instantiate(lblEnemyValue, new Vector3(), Quaternion.identity, containerInstance.transform);
        //lblEnemyValueInstance.GetComponent<RectTransform>().localPosition = new Vector3();
        //lblEnemyValueInstance.text = value.ToString();
        //Animator animator = lblEnemyValueInstance.GetComponent<Animator>();
        //RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        //float animationClipLength = ac.animationClips[0].length;

        //Destroy(lblEnemyValueInstance, animationClipLength);
        //Destroy(containerInstance, animationClipLength + 0.01f);
        GameObject valueDisplay = ObjectPool.instance.GetPooledObject("enemyValue");
        valueDisplay.transform.parent = this.transform;
        valueDisplay.transform.position = Camera.main.WorldToScreenPoint(position);

        GameObject textGO = valueDisplay.transform.GetChild(0).gameObject;
        Text text = textGO.GetComponent<Text>();
        text.text = value.ToString();

        valueDisplay.SetActive(true);
    }
}
