using UnityEngine;
using System.Collections;

public class MathUtil
{
    private MathUtil() { }
    private static MathUtil mInstance;
    public static MathUtil GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new MathUtil();
        }
        return mInstance;
    }

    //对一个向量绕x轴，再绕y轴旋转,世界坐标轴
    public Vector3 Rotate(Vector3 point, float angleX, float angleY)
    {
		float radian = angleX * Mathf.Deg2Rad;
		float cos = Mathf.Cos(radian);
		float sin = Mathf.Sin(radian);
        Vector3 temp = new Vector3();
		temp.x = point.x;
		temp.y = point.y * cos - point.z * sin;
		temp.z = point.y * sin + point.z * cos;

        Vector3 result = new Vector3();
		radian = angleY * Mathf.Deg2Rad;
		cos = Mathf.Cos(radian);
		sin = Mathf.Sin(radian);
		result.x = temp.x * cos + temp.z * sin;
		result.y = temp.y;
		result.z = -sin * temp.x + temp.z * cos;

        return result;
    }

    /// <summary>
    /// 屏幕坐标-->ui坐标
    /// </summary>
    /// <param name="screenPos"></param>
    /// <returns></returns>
    public static Vector3 ScreenPos_to_NGUIPos(Vector3 screenPos)
    {
        Vector3 uiPos = UICamera.currentCamera.ScreenToWorldPoint(screenPos);
        uiPos = UICamera.currentCamera.transform.InverseTransformPoint(uiPos);
        return uiPos;
    }

    /// <summary>
    /// 屏幕坐标-->ngui坐标
    /// </summary>
    /// <param name="screenPos"></param>
    /// <returns></returns>
    Vector3 ScreenPos_to_NGUIPos(Vector2 screenPos)
    {
        return ScreenPos_to_NGUIPos(new Vector3(screenPos.x, screenPos.y, 0f));
    }
}
