using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour {

    public Item item { get; set; }
    private Image img;
    private Text text;

	public void Init()
    {
        this.img = this.transform.GetChild(0).GetComponent<Image>();
        this.text = this.transform.GetChild(1).GetComponent<Text>();
    }

    public void SetUp()
    {    
        if(this.item.amount > 1)
        {
            this.text.text = "x" + this.item.amount.ToString();
            this.text.gameObject.SetActive(true);
        }
        else
        {
            this.text.gameObject.SetActive(false);
        }
        this.img.sprite = this.item.sprite;
    }
}
