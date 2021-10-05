using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// メッシュを結合するクラスです。
/// </Summary>
public class MeshCombiner : MonoBehaviour
{
    // フィールドパーツの親オブジェクトのTransformです。
    public Transform fieldParent;

    // 結合したメッシュのマテリアルです。
    public Material combinedMat;


    public void CreateGameObject(GameObject obj1,GameObject obj2 )
    {
        var parentObject = new GameObject();

        obj1.transform.parent = parentObject.transform;
        obj2.transform.parent = parentObject.transform;

        parentObject.tag = "Interactable";
        var objRigid = parentObject.AddComponent<Rigidbody>();
        objRigid.useGravity = false;
        CombineMesh(parentObject);
    }

    /// <Summary>
    /// メッシュを結合します。
    /// </Summary>
    void CombineMesh(GameObject parent)
    {
        // 親オブジェクトにMeshFilterがあるかどうか確認します。
        MeshFilter parentMeshFilter = CheckParentComponent<MeshFilter>(parent);
        // 親オブジェクトにMeshRendererがあるかどうか確認します。
        MeshRenderer parentMeshRenderer = CheckParentComponent<MeshRenderer>(parent);
        // 子オブジェクトのMeshFilterへの参照を配列として保持します。
        // ただし、親オブジェクトのメッシュもGetComponentsInChildrenに含まれるので除外します。
        MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
        List<MeshFilter> meshFilterList = new List<MeshFilter>();
        for (int i = 1; i < meshFilters.Length; i++)
        {
            meshFilterList.Add(meshFilters[i]);
        }
        // 結合するメッシュの配列を作成します。
        CombineInstance[] combine = new CombineInstance[meshFilterList.Count];
        // 結合するメッシュの情報をCombineInstanceに追加していきます。
        for (int i = 0; i < meshFilterList.Count; i++)
        {
            combine[i].mesh = meshFilterList[i].sharedMesh;
            combine[i].transform = meshFilterList[i].transform.localToWorldMatrix;
            meshFilterList[i].gameObject.SetActive(false);
        }

        // 結合したメッシュをセットします。
        parentMeshFilter.mesh = new Mesh();
        parentMeshFilter.mesh.CombineMeshes(combine);

        // 結合したメッシュにマテリアルをセットします。
        parentMeshRenderer.material = combinedMat;

        var objCollider= parent.gameObject.AddComponent<MeshCollider>();

        objCollider.convex = true;
        objCollider.isTrigger = true;
        // 親オブジェクトを表示します。
        parent.gameObject.SetActive(true);
    }

    /// <Summary>
    /// 指定されたコンポーネントへの参照を取得します。
    /// コンポーネントがない場合はアタッチします。
    /// </Summary>
    T CheckParentComponent<T>(GameObject obj) where T : Component
    {
        // 型パラメータで指定したコンポーネントへの参照を取得します。
        var targetComp = obj.GetComponent<T>();
        if (targetComp == null)
        {
            targetComp = obj.AddComponent<T>();
        }
        return targetComp;
    }
}
