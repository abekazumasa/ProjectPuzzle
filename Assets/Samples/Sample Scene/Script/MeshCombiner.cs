using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// ���b�V������������N���X�ł��B
/// </Summary>
public class MeshCombiner : MonoBehaviour
{
    // �t�B�[���h�p�[�c�̐e�I�u�W�F�N�g��Transform�ł��B
    public Transform fieldParent;

    // �����������b�V���̃}�e���A���ł��B
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
    /// ���b�V�����������܂��B
    /// </Summary>
    void CombineMesh(GameObject parent)
    {
        // �e�I�u�W�F�N�g��MeshFilter�����邩�ǂ����m�F���܂��B
        MeshFilter parentMeshFilter = CheckParentComponent<MeshFilter>(parent);
        // �e�I�u�W�F�N�g��MeshRenderer�����邩�ǂ����m�F���܂��B
        MeshRenderer parentMeshRenderer = CheckParentComponent<MeshRenderer>(parent);
        // �q�I�u�W�F�N�g��MeshFilter�ւ̎Q�Ƃ�z��Ƃ��ĕێ����܂��B
        // �������A�e�I�u�W�F�N�g�̃��b�V����GetComponentsInChildren�Ɋ܂܂��̂ŏ��O���܂��B
        MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
        List<MeshFilter> meshFilterList = new List<MeshFilter>();
        for (int i = 1; i < meshFilters.Length; i++)
        {
            meshFilterList.Add(meshFilters[i]);
        }
        // �������郁�b�V���̔z����쐬���܂��B
        CombineInstance[] combine = new CombineInstance[meshFilterList.Count];
        // �������郁�b�V���̏���CombineInstance�ɒǉ����Ă����܂��B
        for (int i = 0; i < meshFilterList.Count; i++)
        {
            combine[i].mesh = meshFilterList[i].sharedMesh;
            combine[i].transform = meshFilterList[i].transform.localToWorldMatrix;
            meshFilterList[i].gameObject.SetActive(false);
        }

        // �����������b�V�����Z�b�g���܂��B
        parentMeshFilter.mesh = new Mesh();
        parentMeshFilter.mesh.CombineMeshes(combine);

        // �����������b�V���Ƀ}�e���A�����Z�b�g���܂��B
        parentMeshRenderer.material = combinedMat;

        var objCollider= parent.gameObject.AddComponent<MeshCollider>();

        objCollider.convex = true;
        objCollider.isTrigger = true;
        // �e�I�u�W�F�N�g��\�����܂��B
        parent.gameObject.SetActive(true);
    }

    /// <Summary>
    /// �w�肳�ꂽ�R���|�[�l���g�ւ̎Q�Ƃ��擾���܂��B
    /// �R���|�[�l���g���Ȃ��ꍇ�̓A�^�b�`���܂��B
    /// </Summary>
    T CheckParentComponent<T>(GameObject obj) where T : Component
    {
        // �^�p�����[�^�Ŏw�肵���R���|�[�l���g�ւ̎Q�Ƃ��擾���܂��B
        var targetComp = obj.GetComponent<T>();
        if (targetComp == null)
        {
            targetComp = obj.AddComponent<T>();
        }
        return targetComp;
    }
}
