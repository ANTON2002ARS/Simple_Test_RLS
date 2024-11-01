using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Color select_color;
    public static Color main_color;
    [Header("for Menu")]
    [SerializeField] private GameObject menu;
    [SerializeField] private Button[] buttons_roles;
    [SerializeField] private Button buttons_deoloyment;
    [SerializeField] private Button buttons_folding;


    [Header("for Testing")]
    [SerializeField] private GameObject actions_select;
    [SerializeField] private Command Action_deoloyment;
    [SerializeField] private Command Action_folding;
    [SerializeField] private GameObject Button_Prefab;
    [SerializeField] private Transform Content;
    
    public static GameManager instance;
    private void Awake()=>instance =this;

    private int role;    
    private Option_Test test_rls;
    private enum Option_Test {
        is_deoloyment,
        is_folding,
        none
    }

    public void Start(){
        test_rls = Option_Test.none;
    }  

    public void Back_To_Menu(){
        menu.SetActive(true);
        actions_select.SetActive(false);
        role = 0;
        test_rls = Option_Test.none;
        Reset_Color_Button();
        Debug.Log("END TEST");
    }

    private void Start_Test(){
        if(role == 0) return;
        if(test_rls == Option_Test.none) return;
        Debug.Log("START TEST");
        Debug.Log("role: "+ role.ToString()+ " test: " + test_rls);
        Back_To_Menu();

        menu.SetActive(false);
        actions_select.SetActive(true);
    }
    
    public void Set_Role(int r){        
        role =r;
        foreach (var b in buttons_roles)
            Switch_color(b, main_color);     
        Switch_color(buttons_roles[r-1], select_color);        
        Start_Test();
    }

    public void Start_Deloyment(){
        test_rls = Option_Test.is_deoloyment;
        Switch_color(buttons_deoloyment,select_color);
        Switch_color(buttons_folding, main_color);
        Start_Test();
    }

    public void Start_Folding(){
        test_rls = Option_Test.is_folding;
        Switch_color(buttons_deoloyment, main_color);
        Switch_color(buttons_folding, select_color);         
        Start_Test();
    }
    
    public void Placement_Button(Options option_list){
        // while (Content.childCount > 0)
        //     Destroy(Content.GetChild(0).gameObject);
        
        foreach (var opt in option_list.variants_deoloyment)
        {
           GameObject buttonInstance = Instantiate(Button_Prefab,Content);
           Text text_button = buttonInstance.GetComponentInChildren<Text>();
           text_button.text = opt.ActionName;
                      
        }

    }

    private void Reset_Color_Button(){
        foreach (var b in buttons_roles)
            Switch_color(b, main_color);        
        Switch_color(buttons_deoloyment, main_color);
        Switch_color(buttons_folding, main_color);     
    }

    private void Switch_color (Button b, Color c)
    {
        b.gameObject.GetComponent<Image>().color = c;
        // ColorBlock b_color = b.colors;
        // b_color.normalColor = c;
        // b_color.highlightedColor = c;
        // b_color.pressedColor = c;
        // b.colors = b_color;
    }

    public void Exit()=>Application.Quit();
}
