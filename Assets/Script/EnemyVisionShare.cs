using UnityEngine;

public class EnemyVisionShare : MonoBehaviour
{
    private bool isAlly = false;

    [Header("References")]
    public FieldOfView fieldOfView;  // 적의 시야

    void Start()
    {
        if (fieldOfView == null)
        {
            fieldOfView = GetComponentInChildren<FieldOfView>();
        }

        // 처음에는 FOV 비활성화 (적은 안 보여야 하니까)
        if (fieldOfView != null)
        {
            fieldOfView.enabled = false;
        }
    }

    public void BecomeAlly()
    {
        if (isAlly) return;
        isAlly = true;

        Debug.Log($"[{gameObject.name}] 아군이 되었습니다!");

        // 적의 FOV 활성화 = Darkness 뚫림 = 적 주변 보임
        if (fieldOfView != null)
        {
            fieldOfView.enabled = true;

            // FOV 색상을 초록색으로 (구분용)
            MeshRenderer renderer = fieldOfView.GetComponent<MeshRenderer>();
            if (renderer != null && renderer.material != null)
            {
                renderer.material.color = new Color(0f, 1f, 0f, 0.3f); // 초록색 반투명
            }
        }
    }

    public bool IsAlly()
    {
        return isAlly;
    }
}