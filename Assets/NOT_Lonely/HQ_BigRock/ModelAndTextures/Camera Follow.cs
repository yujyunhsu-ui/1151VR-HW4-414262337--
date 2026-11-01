using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟隨目標")]
    public Transform target;          // 飛機的 Transform

    [Header("視角位置與平滑參數")]
    public Vector3 offset = new Vector3(0f, 3f, -10f); // 攝影機相對於飛機的位置偏移
    public float followSpeed = 10f;    // 位置跟隨平滑速度
    public float rotateSpeed = 10f;    // 旋轉跟隨平滑速度

    void LateUpdate()
    {
        if (target == null) return;

        // 1. 計算攝影機目標位置 (包含飛機的旋轉方向)
        Vector3 desiredPosition = target.TransformPoint(offset);

        // 2. 平滑移動攝影機位置
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // 3. 平滑旋轉攝影機朝向飛機的飛行方向
        Quaternion desiredRotation = Quaternion.LookRotation(target.forward, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);
    }
}
