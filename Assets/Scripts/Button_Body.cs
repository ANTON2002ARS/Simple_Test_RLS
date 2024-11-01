using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Body : MonoBehaviour
{
    [SerializeField] private int Index;
    [SerializeField] private Text text_button;
    [SerializeField] private Text text_number;
    private TestVariantAction VariantAction;

    public void Start()=> this.GetComponent<Button>().onClick.AddListener(button_click);    
    
    public void Get_Variant(TestVariantAction testVariantAction){
        VariantAction = testVariantAction;
        text_button.text = testVariantAction.ActionName;
        Index = testVariantAction.number;
        text_number.text = Index.ToString();
        if(testVariantAction.number == 0)
            return;
        text_number.text = testVariantAction.number.ToString();
        Set_Color(GameManager.instance.select_color);        
    }

    public void button_click(){        
        if(Index == GameManager.Index_Selected_Element && GameManager.Index_Selected_Element !=0){
            GameManager.Index_Selected_Element--;
            Index = 0;
            Set_Color(GameManager.instance.main_color);
            Debug.Log("Index: " + Index + "  Index_Selected_Element: " + GameManager.Index_Selected_Element);
        }
        else{
            GameManager.Index_Selected_Element++;
            Index = GameManager.Index_Selected_Element;
            GameManager.instance.Verify_Passing_Test(Index,VariantAction);
            Set_Color(GameManager.instance.select_color);
            Debug.Log("Index: " + Index + "  Index_Selected_Element: " + GameManager.Index_Selected_Element);
        }

        VariantAction.number = Index;
        text_number.text = Index.ToString();
        Debug.Log("Choice: " + VariantAction.ActionName + "  Index: " + Index);
    }

    private void Set_Color(Color color)=>this.GetComponent<Image>().color = color;
    
}
