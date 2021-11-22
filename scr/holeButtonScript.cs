using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Color_Change;
using System.Threading;

public class holeButtonScript : MonoBehaviour
{
    public int row;
    public int column;
    private static int[,] list;
    private static int[,] pair_list;
    private static int count;
    private static int pair_count;
    private Color color;
    private Color result_col;
    private Image my_image;
    private Image res_image;
    private Text text;
    private bool result_flag;
    private static int total_resist;
    private static GameObject error_ob;
    private static List<infoComponent> totalComponent;
    private static int saveRow;
    private static int saveCol;

    private void Start()
    {
        count = 0;
        pair_count = 1;
        list = new int[7, 10];
        pair_list = new int[7, 10];
        total_resist = 0;
        totalComponent = new List<infoComponent>();
        
        error_ob = GameObject.FindWithTag("popup");

        saveRow = 0;
        saveCol = 0;
        Color_Change.setTypes();
    }

    public void DoInitClick()
    {
        totalComponent = new List<infoComponent>();

        var ele_ob = GameObject.FindWithTag("selected");
        my_image = ele_ob.GetComponent<Image>();
        color = my_image.color;

        var result_ob = GameObject.FindWithTag("result");
        res_image = result_ob.GetComponent<Image>();
        result_col = res_image.color;
        result_col.r = 255;
        result_col.g = 255;
        result_col.b = 255;
        result_col.a = 0;
        res_image.color = result_col;

        GameObject[] hole_ob = GameObject.FindGameObjectsWithTag("2D_hole");
        
        for (int i = 0; i < hole_ob.Length; i++)
        {
            hole_ob[i].GetComponent<Button>().interactable = true;
            hole_ob[i].GetComponent<Button>().GetComponentInChildren<Text>().text = "";
        }

        GameObject[] elec_object = GameObject.FindGameObjectsWithTag("electronic component");
        for (int i = 0; i < elec_object.Length; i++)
        {
            elec_object[i].GetComponent<Button>().interactable = true;
        }

        GameObject[] resistance_ob = GameObject.FindGameObjectsWithTag("resistance");
        for (int i = 0; i < resistance_ob.Length; i++)
        {
            resistance_ob[i].GetComponent<Button>().interactable = false;
        }

        GameObject[] compo_ob = GameObject.FindGameObjectsWithTag("3DComponent");
        for (int i = 0; i < compo_ob.Length; i++)
        {
            Destroy(compo_ob[i]);
        }

        total_resist = 0;
        color.a = 0;
        my_image.color = color;

        error_ob.GetComponent<Text>().text = "";

        count = 0;
        pair_count = 1;
        list = new int[7, 10];
        pair_list = new int[7, 10];
    }

    public void DoReturnClick()
    {

    }

    public void DoResultClick()
    {
        result_flag = true;
        for (int i =0; i < 10; i++)
        {
            int col_count = 0;
            for (int j =0; j < 7; j++)
            {
                if (list[j, i] != 0)
                {
                    col_count++;
                }
            }
            if (col_count == 1 || col_count > 3)
            {
                error_ob.GetComponent<Text>().text = "회로 구성이 부적합합니다.";
                return;
            }
        }

        int plus_location = 0;
        int minus_location = 0;
        bool location = true;

        for (int i = 5; i < 7; i++)
        {
            int row_count = 0;
            for (int j = 0; j < 10; j++)
            {
                if (list[i, j] == 1)
                {
                    row_count++;
                    if (i==5)
                    {
                        plus_location = j;
                    }
                    else if (i == 6)
                    {
                        minus_location = j;
                    }
                }
            }
            if (row_count != 1)
            {
                error_ob.GetComponent<Text>().text = "+,-의 연결이 정확하지 않습니다.";
                location = false;
                return;
            }
        }

        int plus_col = 0;
        int minus_col = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 7; j++)
            {
    
                if (list[j, i] == 2)
                {
                    plus_col = i;
                }
                else if (list[j, i] == -2)
                {
                    minus_col = i;
                }
            }
        }
        if (location)
        {
            if (plus_location > minus_location)
            {
                if (plus_col > minus_col)
                {
                    result_flag = true;
                }
                else
                {
                    error_ob.GetComponent<Text>().text = "LED의 방향이 올바르지 않습니다.";
                    return;
                }
            }
            else if (plus_location < minus_location)
            {
                if (plus_col < minus_col)
                {
                    result_flag = true;
                }
                else
                {
                    error_ob.GetComponent<Text>().text = "LED의 방향이 올바르지 않습니다.";
                    return;
                }
            }
        }
        double ampere = 3.3 / (double) total_resist;
        if(ampere < 0.02)
        {
            error_ob.GetComponent<Text>().text = "LED에 흐르는 전류의 세기가 약합니다.";
            return;
        }
        else if (total_resist == 0 || ampere > 0.05)
        {
            error_ob.GetComponent<Text>().text = "LED에 흐르는 전류의 세기가 강합니다.";
            return;
        }

        if (result_flag == true)
        {
            var ele_ob = GameObject.FindWithTag("result");
            my_image = ele_ob.GetComponent<Image>();
            color = my_image.color;
            color.a = 255;
            my_image.color = color;

            Color_Change.setModel(list, pair_list, pair_count, totalComponent);
        }
        
    }

    public void resistanceClick()
    {
        string btn_name = GetComponent<Button>().name;
        if (btn_name.Equals("resistance_110_button"))
        {
            total_resist += 110;
        }
        else if (btn_name.Equals("resistance_220_button"))
        {
            total_resist += 220;
        }
        else if (btn_name.Equals("resistance_330_button"))
        {
            total_resist += 330;
        }

        GameObject[] resistance_ob = GameObject.FindGameObjectsWithTag("resistance");
        for (int i = 0; i < resistance_ob.Length; i++)
        {
            resistance_ob[i].GetComponent<Button>().interactable = false;
        }
        
    }

    //private Text text; // oculus 환경에서 확인하기 위한 text
    //private Text text2;

    // wire : 1, led : 2, battery : 3, registance : 4, switch : 5
    // condenser : 6, diode : 7, coil : 8, transistor : 9, none : 0
    public void holeClick()
    {
        var ele_ob = GameObject.FindWithTag("selected");
        my_image = ele_ob.GetComponent<Image>();
        color = my_image.color;
        string image_name = my_image.sprite.name; //component sprite의 이름

        if (color.a == 0) return; //component가 선택되지 않았을 때 종료
        if (count == 0)
        {
            saveRow = row;
            saveCol = column;
        }
        else if (count == 1) totalComponent.Add(new infoComponent(saveRow, saveCol, row, column, typeNum[image_name]));


        if (image_name.Equals("stripping-wires"))
        {
            
            if (list[row, column] == 0)
            {

                list[row, column] = 1;
                pair_list[row, column] = pair_count;
                count++;

                GetComponent<Button>().GetComponentInChildren<Text>().text = "" + pair_count;
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = Color.red;
                GetComponent<Button>().colors = b_color;
            }
            
        }
        else if (image_name.Equals("led2"))
        {

            if (count == 0 && list[row, column] == 0)
            {
                list[row, column] = 2;
                pair_list[row, column] = pair_count;
                count++;
                GetComponent<Button>().GetComponentInChildren<Text>().text = "+";
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = Color.blue;
                GetComponent<Button>().colors = b_color;
            }

            if (count == 1 && list[row, column] == 0)
            {
                list[row, column] = -2;
                pair_list[row, column] = pair_count;
                count++;
                GetComponent<Button>().GetComponentInChildren<Text>().text = "-";
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = Color.blue;
                GetComponent<Button>().colors = b_color;
            }
        }
        else if (image_name.Equals("battery"))
        {

            if (list[row, column] == 0)
            {
                list[row, column] = 3;
                pair_list[row, column] = pair_count;
                count++;
                GetComponent<Button>().GetComponentInChildren<Text>().text = "" + pair_count;
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = Color.green;
                GetComponent<Button>().colors = b_color;
            }
        }
        else if (image_name.Equals("resistance"))
        {
            GameObject[] resistance_ob = GameObject.FindGameObjectsWithTag("resistance");
            for (int i = 0; i < resistance_ob.Length; i++)
            {
                resistance_ob[i].GetComponent<Button>().interactable = true;
            }

            if (list[row, column] == 0)
            {
                list[row, column] = 4;
                pair_list[row, column] = pair_count;
                count++;
                GetComponent<Button>().GetComponentInChildren<Text>().text = "" + pair_count;
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = Color.yellow;
                GetComponent<Button>().colors = b_color;
                
            }
        }
        else if (image_name.Equals("switch"))
        {
            if (list[row, column] == 0)
            {
                list[row, column] = 5;
                pair_list[row, column] = pair_count;
                count++;
                GetComponent<Button>().GetComponentInChildren<Text>().text = "" + pair_count;
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = Color.magenta;
                GetComponent<Button>().colors = b_color;
            }
        }
        else if (image_name.Equals("capacitor"))
        {
            if (list[row, column] == 0)
            {
                list[row, column] = 6;
                pair_list[row, column] = pair_count;
                count++;
                GetComponent<Button>().GetComponentInChildren<Text>().text = "" + pair_count;
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = Color.cyan;
                GetComponent<Button>().colors = b_color;
            }
        }
        else if (image_name.Equals("diode"))
        {
            if (list[row, column] == 0)
            {
                list[row, column] = 7;
                pair_list[row, column] = pair_count;
                count++;
                GetComponent<Button>().GetComponentInChildren<Text>().text = "" + pair_count;
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = new Color(255/255f, 127 / 255f, 0 / 255f);
                GetComponent<Button>().colors = b_color;
            }
        }
        else if (image_name.Equals("wire"))
        {
            if (list[row, column] == 0)
            {
                list[row, column] = 8;
                pair_list[row, column] = pair_count;
                count++;
                GetComponent<Button>().GetComponentInChildren<Text>().text = "" + pair_count;
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = new Color(139 / 255f, 0 / 255f, 255 / 255f);
                GetComponent<Button>().colors = b_color;
            }
        }
        else if (image_name.Equals("transistor"))
        {

            if (list[row, column] == 0)
            {
                list[row, column] = 9;
                pair_list[row, column] = pair_count;
                count++;
                GetComponent<Button>().GetComponentInChildren<Text>().text = "" + pair_count;
                GetComponent<Button>().interactable = false;
                var b_color = GetComponent<Button>().colors;
                b_color.disabledColor = new Color(0 / 255f, 128 / 255f, 0 / 255f);
                GetComponent<Button>().colors = b_color;
            }
        }

        if (count == 1)
        {
            GameObject[] elec_object = GameObject.FindGameObjectsWithTag("electronic component");
            for (int i = 0; i< elec_object.Length; i++)
            {
                elec_object[i].GetComponent<Button>().interactable = false;
            }
        }

        if (count >= 2)
        {
            GameObject[] elec_object = GameObject.FindGameObjectsWithTag("electronic component");
            for (int i = 0; i < elec_object.Length; i++)
            {
                elec_object[i].GetComponent<Button>().interactable = true;
            }

            GameObject[] resistance_ob = GameObject.FindGameObjectsWithTag("resistance");
            for (int i = 0; i < resistance_ob.Length; i++)
            {
                resistance_ob[i].GetComponent<Button>().interactable = false;
            }

            count = 0;
            pair_count++;
            color.a = 0;
            my_image.color = color;
        }

    }
}
