using UnityEngine;


//���̃X�N���v�g
public class MergePice : MonoBehaviour
{
    //��̃s�[�X
    [SerializeField]
    private GameObject pisceA;
    //������̃s�[�X
    [SerializeField]
    private GameObject pisceB;
    //�s�[�X�̏����ʒu
    Vector3 offset;

    //firstframe
    void Start()
    {
        //�����ʒu��ݒ�
        offset = pisceB.transform.position - pisceA.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //����̓{�^������������ړ�����悤�ɂ��Ă��܂�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //���s�ړ�
            var translate = (pisceA.transform.position - pisceB.transform.position) + offset;
            //��]
            var quaternion =pisceA.transform.rotation;
            //�X�P�[��
            var scale = pisceB.transform.localScale;
            //�A�t�B���ϊ��s��̐ݒ�
            var matrixs = Matrix4x4.TRS(translate, quaternion, scale);
            //�ʒu�ϊ�
            pisceB.transform.position = matrixs.MultiplyPoint(pisceB.transform.position);
            //���������ɂ���
            pisceB.transform.rotation = pisceA.transform.rotation;
        }
    }
}
