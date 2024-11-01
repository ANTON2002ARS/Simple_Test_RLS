using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Body : MonoBehaviour
{
    [SerializeField] private Text text_button;
    [SerializeField] private Text text_number;
    
    public void Get_Variant(TestVariantAction testVariantAction){
        text_button.text = testVariantAction.ActionName;
        if(testVariantAction.number == 0)
            return;
        text_number.text = testVariantAction.number.ToString();
        Set_Color(GameManager.select_color);        
    }

    private void Set_Color(Color color)=>this.GetComponent<Image>().color = color;
    
}
