using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static holeButtonScript;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Color_Change : MonoBehaviour
{
    public int row;
    public int column;
    public static Dictionary<string, int> typeNum = new Dictionary<string, int>();
    public static float[] lineScale = { 0, 3, 6, 9, 13, 17, 20, 23, 27, 30 };
    public static float[] ResisScale = { 0,8,17,26,35,44,53,62,71,80 };
    private static Text text;
    public static float lineHeight;

    public static void setTypes()
    {
        typeNum["stripping-wires"] = 1;
        typeNum["led2"] = 2;
        typeNum["battery"] = 3;
        typeNum["resistance"] = 4;
        typeNum["switch"] = 5;
        typeNum["capacitor"] = 6;
        typeNum["diode"] = 7;
        typeNum["wire"] = 8;
        typeNum["transistor"] = 9;

        lineHeight = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public struct infoComponent
    {
        public Vector2 st;
        public Vector2 ed;
        public int types;

        public infoComponent(int stRow, int stCol, int edRow, int edCol, int types)
        {
            this.st = new Vector2(stRow, stCol);
            this.ed = new Vector2(edRow, edCol);
            this.types = types;
        }
    }

    public static void setModel(int[,] list, int[,] pair_list, int pair_count, List<infoComponent> totalComponent)
    {
        int pair_first_row = 0;
        int pair_first_col = 0;
        int pair_second_row = 0;
        int pair_second_col = 0;
        int row_3D;
        int col_3D;
        Vector3 first_loc_hole = new Vector3(0,0,0);
        Vector3 second_loc_hole = new Vector3(0, 0, 0);
        float location_x = 0.0f;
        float location_z = 0.0f;



        GameObject[] hole3d_ob = GameObject.FindGameObjectsWithTag("3Dhole");

        GameObject[] compo_ob = GameObject.FindGameObjectsWithTag("3DComponent");
        for (int i = 0; i < compo_ob.Length; i++)
        {
            Destroy(compo_ob[i]);
            lineHeight = 0.0f;
        }

        for (int k =0; k < totalComponent.Count; k++)
        {
            pair_first_row = (int)totalComponent[k].st.x;
            pair_first_col = (int)totalComponent[k].st.y;
            pair_second_row = (int)totalComponent[k].ed.x;
            pair_second_col = (int)totalComponent[k].ed.y;
            int pair_types = totalComponent[k].types;

            if (pair_types == 1 || pair_types == 2 || pair_types == 4)
            {
                for (int h = 0; h < hole3d_ob.Length; h++)
                {
                    row_3D = hole3d_ob[h].GetComponent<Color_Change>().row;
                    col_3D = hole3d_ob[h].GetComponent<Color_Change>().column;

                    if (row_3D == pair_first_row && col_3D == pair_first_col)
                    {
                        first_loc_hole = hole3d_ob[h].transform.position;
                    }
                    else if (row_3D == pair_second_row && col_3D == pair_second_col)
                    {
                        second_loc_hole = hole3d_ob[h].transform.position;
                    }

                    location_x = (first_loc_hole.x + second_loc_hole.x) / 2.0f;
                    location_z = (first_loc_hole.z + second_loc_hole.z) / 2.0f;
                    
                }

                if (pair_types == 4)
                {
                    GameObject Resist_Component = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Placement/Resistor_Small_Placed"), new Vector3(location_x, first_loc_hole.y, location_z), Quaternion.identity);
                    if(pair_first_row != pair_second_row)
                    {
                        int scale_num = Math.Abs(pair_first_row - pair_second_row);
                        Resist_Component.transform.localScale += new Vector3(0, 0, ResisScale[scale_num]);
                    }
                    else if (pair_first_col != pair_second_col)
                    {
                        Resist_Component.transform.Rotate(Resist_Component.transform.rotation.x, 90.0f, Resist_Component.transform.rotation.z, Space.World);
                        int scale_num = Math.Abs(pair_first_col - pair_second_col);
                        Resist_Component.transform.localScale += new Vector3(0, 0, ResisScale[scale_num]);
                    }
                    continue;
                }

                GameObject Component = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Placement/Wire_Placed"), new Vector3(location_x, first_loc_hole.y, location_z), Quaternion.identity);
                if (pair_first_col != pair_second_col)
                {
                    Component.transform.Rotate(Component.transform.rotation.x, 90.0f, Component.transform.rotation.z, Space.World);
                    int scale_num = Math.Abs(pair_first_col - pair_second_col);
                    Component.transform.localScale += new Vector3(0, lineHeight, lineScale[scale_num] - (scale_num == 5 ? 1 : 0));
                    
                }
                else if(pair_first_row != pair_second_row)
                {
                    int scale_num = Math.Abs(pair_first_row - pair_second_row);
                    Component.transform.localScale += new Vector3(0, lineHeight, lineScale[scale_num]);
                    
                }

                if (pair_types == 2)
                {
                    GameObject LED_Component = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Placement/LED"), new Vector3(location_x, first_loc_hole.y, location_z), Quaternion.identity);
                    LED_Component.transform.localScale += new Vector3(0, lineHeight, 0);
                }

                lineHeight += 0.5f;
            }
            
        }
    }

}
