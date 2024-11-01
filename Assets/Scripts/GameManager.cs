using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public  Color select_color;
    public  Color main_color;
    [Header("for Menu")]
    [SerializeField] private GameObject menu;
    [SerializeField] private Button[] buttons_roles;
    [SerializeField] private Button buttons_deoloyment;
    [SerializeField] private Button buttons_folding;
    [SerializeField] private GameObject Panel_Sure_Check;
    [SerializeField] private GameObject Panel_Check_Result;


    [Header("for Testing")]
    [SerializeField] private GameObject actions_select;
    [SerializeField] private Command Action_deoloyment;
    [SerializeField] private Command Action_folding;
    [SerializeField] private GameObject Button_Prefab;
    [SerializeField] private Transform Content;
    [SerializeField] private Button M1_button;
    [SerializeField] private Button M2_button;
    [SerializeField] private Button Outside_button;
    [SerializeField] private Options M1;
    [SerializeField] private Options M2;
    [SerializeField] private Options Outside;
    
    public static GameManager instance;
    private void Awake()=>instance =this;

    public static int Index_Selected_Element;

    private int role;    
    private Option_Test test_rls;
    private enum Option_Test {
        is_deoloyment,
        is_folding,
        none
    }

    public void Start(){
        test_rls = Option_Test.none;
        menu.SetActive(true);
        Panel_Check_Result.SetActive(false);
        Panel_Sure_Check.SetActive(false);
        actions_select.SetActive(false);
    }  

    public void Sure_To_Menu()=>Panel_Sure_Check.SetActive(true);
    public void Cansel_Menu()=>Panel_Sure_Check.SetActive(false);

    public void Back_To_Menu(){
        menu.SetActive(true);
        Panel_Sure_Check.SetActive(false);
        actions_select.SetActive(false);
        actions_select.SetActive(false);
        role = 0;        
        test_rls = Option_Test.none;
        Reset_Color_Button_Menu();
        Debug.Log("END TEST");
    }

    private void Start_Test(){
        if(role == 0) return;
        if(test_rls == Option_Test.none) return;
        Debug.Log("START TEST");
        Debug.Log("role: "+ role.ToString()+ " test: " + test_rls);
        menu.SetActive(false);
        actions_select.SetActive(true);
        Reset_ALL_Elements();
        Placement_Outside();
    }

    private void Pass_Test(){

    }
    
    private void Fall_Test(){

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
    
    public void Placement_M1()=>Placement_Text_List(M1, M1_button);
    public void Placement_M2()=>Placement_Text_List(M2,M2_button);
    public void Placement_Outside()=> Placement_Text_List(Outside, Outside_button);

    public void Placement_Text_List(Options option, Button button_click){ 

        Reset_Color_Button_Navigation();
        Switch_color(button_click, select_color);

        if(test_rls == Option_Test.is_deoloyment)
            Initialization_Button(option.variants_deoloyment);
        else if(test_rls == Option_Test.is_folding){
            if(option.use_list_deoloyment== true)
                Initialization_Button(option.variants_deoloyment);
            else
                Initialization_Button(option.variants_folding);            
        }else
            Debug.Log("ERROR!!!");        
        Debug.Log("Button is placement, for: " + test_rls);
    }

    private void Initialization_Button(List<TestVariantAction> variantActions){
        foreach (Transform child in Content)
            Destroy(child.gameObject);  
        foreach (var opt in variantActions)
        {
           GameObject buttonInstance = Instantiate(Button_Prefab,Content);
           buttonInstance.GetComponent<Button_Body>().Get_Variant(opt);                     
        }     
    }

    private void Reset_ALL_Elements(){
        foreach (TestVariantAction v in M1.variants_deoloyment)
            v.number = 0;
        foreach(TestVariantAction v in M1.variants_folding)
            v.number =0;
        foreach(TestVariantAction v in M2.variants_deoloyment)
            v.number =0;
        foreach(TestVariantAction v in M2.variants_folding)
            v.number = 0;
        foreach(TestVariantAction v in Outside.variants_deoloyment)
            v.number = 0;
        foreach(TestVariantAction v in Outside.variants_folding)
            v.number =0;
        Index_Selected_Element = 0;        
    }
    
    private void Reset_Color_Button_Navigation(){
        Switch_color(M1_button,main_color);
        Switch_color(M2_button,main_color);
        Switch_color(Outside_button,main_color);
    }

    private void Reset_Color_Button_Menu(){
        foreach (var b in buttons_roles)
            Switch_color(b, main_color);        
        Switch_color(buttons_deoloyment, main_color);
        Switch_color(buttons_folding, main_color);     
    }

    private void Switch_color (Button b, Color c)=> b.gameObject.GetComponent<Image>().color = c;
        // ColorBlock b_color = b.colors;
        // b_color.normalColor = c;
        // b_color.highlightedColor = c;
        // b_color.pressedColor = c;
        // b.colors = b_color;
    
    public void Exit()=>Application.Quit();
}
