using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
  protected Text text;
  protected System.DateTime createTime;

  private void Awake() {
    this.text = this.gameObject.GetComponent<Text>();

    if (text == null) {
      Debug.LogError("DamageText does not have a text component attached.");
    }
  }

  public void SetDamage(int damage, bool heal) {
    this.text.color = Color.red;
    if (heal) {
      this.text.color = Color.green;
    }

    this.text.text = damage.ToString();
  }

  public void SetCreateTimeNow() {
    this.createTime = System.DateTime.Now;
  }

  private void Update() {
    if ((System.DateTime.Now - this.createTime).Milliseconds > 500) {
      Destroy(this.gameObject);
    }

    this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0.0f, 0.002f, 0.0f);
  }
}
