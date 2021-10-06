using UnityEngine;


//問題のスクリプト
public class MergePice : MonoBehaviour
{
    //一つのピース
    [SerializeField]
    private GameObject pisceA;
    //もう一つのピース
    [SerializeField]
    private GameObject pisceB;
    //ピースの初期位置
    Vector3 offset;

    //firstframe
    void Start()
    {
        //初期位置を設定
        offset = pisceB.transform.position - pisceA.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //今回はボタンを押したら移動するようにしています
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //平行移動
            var translate = (pisceA.transform.position - pisceB.transform.position) + offset;
            //回転
            var quaternion =pisceA.transform.rotation;
            //スケール
            var scale = pisceB.transform.localScale;
            //アフィン変換行列の設定
            var matrixs = Matrix4x4.TRS(translate, quaternion, scale);
            //位置変換
            pisceB.transform.position = matrixs.MultiplyPoint(pisceB.transform.position);
            //同じ向きにする
            pisceB.transform.rotation = pisceA.transform.rotation;
        }
    }
}
