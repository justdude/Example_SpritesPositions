using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PositionHandler : MonoBehaviour {


    public GameObject TEMPSIZE;

    public Rect r;

    public int Rows=4;
    public int Columns=4;

    public Vector2 distBetweenXY=Vector2.zero;
    public Vector2 RCScale = Vector2.zero;


    public GameObject tk2dspr;
    List<Vector3> positions=new List<Vector3>();


    void Start() {


        Vector2 objs = tk2dspr.GetComponent<tk2dSprite>().GetUntrimmedBounds().size;
        r = new Rect(-3, 1.3f , TEMPSIZE.GetComponent<tk2dSprite>().GetUntrimmedBounds().size.x, TEMPSIZE.GetComponent<tk2dSprite>().GetUntrimmedBounds().size.y);

        Scale(r, Rows, Columns, objs, distBetweenXY, out RCScale);
        SetSPriteSize(tk2dspr.GetComponent<tk2dSprite>(), RCScale);

         objs = tk2dspr.GetComponent<tk2dSprite>().GetUntrimmedBounds().size;
        
        Vector2 pos=new Vector2(r.x,r.y);
        Debug.LogError(pos);
        for (int i = 0; i < Rows; i++) {
            for (int j = 0; j < Columns; j++) { 
                positions.Add(CreateContainerPos(pos,new Vector2(pos.x+objs.x,pos.y-objs.y)));
                    pos.x += distBetweenXY.x;
                    pos.x += objs.x;
            }
            pos.x = r.xMin;
            pos.y -= distBetweenXY.y;
            pos.y -= objs.y;
        }

  
        foreach (var p in positions)
        {
            Instantiate(tk2dspr, p, new Quaternion());
            print(p);
        }

    }

    Vector2 CreateContainerPos(Vector2 left,Vector2 right) {
        return (right + left) / 2;
    }

    void SetSPriteSize(tk2dSprite spr,Vector2 scaleSize) {
        spr.scale = scaleSize;
    }

    void Scale(Rect rect,float rows,float colls,Vector2 objSize, Vector2 distBxy,out Vector2 scale) 
    {
        scale = Vector2.zero;
        scale.x = rect.width/(colls * objSize.x + (colls - 1) * distBxy.x);
        scale.y = rect.height/(rows * objSize.y + (rows - 1) * distBxy.y);
    }


}
