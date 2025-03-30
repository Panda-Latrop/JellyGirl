using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Utility
{
	public static string ResourcesPath = "Assets/Resources/";
    public static string resourcesAudioMixerPath = "Audio/AudioMixerMaster";
    #region RaycastHit
    public static bool HasCollision(this RaycastHit raycastHit)
	{
		return raycastHit.normal.sqrMagnitude > 0.5f;
	}
	public static bool HasCollision(this RaycastHit2D hit2D)
	{
		return hit2D.normal.sqrMagnitude > 0.5f;
	}
	#endregion

	#region Vector
	public static Vector3 ExtractDotVector(Vector3 vector, Vector3 direction)
	{
		float amount = Vector3.Dot(vector, direction);
		return direction * amount;
	}
	public static Vector3 RemoveDotVector(Vector3 vector, Vector3 direction)
	{
		vector -= ExtractDotVector(vector, direction);
		return vector;
	}
	public static void Extract(this Vector3 vector, out float magnitude, out Vector3 normal)
	{
        magnitude = Vector3.Magnitude(vector);
        if (magnitude > 1E-05f)
            normal = vector / magnitude;
        else
            normal = Vector3.zero;
    }
    public static float Max(this Vector3 vector) { return Mathf.Max(vector.x, vector.y, vector.z);}

 
    
    #endregion

    #region Math
    public static Quaternion ClampRotation(Quaternion q, Vector3 minBounds, Vector3 maxBounds)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, minBounds.x, maxBounds.x);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);
        angleY = Mathf.Clamp(angleY, minBounds.y, maxBounds.y);
        q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

        float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);
        angleZ = Mathf.Clamp(angleZ, minBounds.z, maxBounds.z);
        q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

        return q.normalized;
    }
    public static Quaternion ClampRotation2D(Quaternion q, float minBounds, float maxBounds)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);
        angleZ = Mathf.Clamp(angleZ, minBounds, maxBounds);
        q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

        return q.normalized;
    }
    #endregion

    #region Scene
    public static bool FindComponentInRootObjectsByTag<T>(Scene scene, string tag, out T component) where T : Component
	{
		var roots = new List<GameObject>(scene.rootCount);
		scene.GetRootGameObjects(roots);	
		for (int i = 0; i < roots.Count; i++)
		{
            if (roots[i].CompareTag(tag))
			{
				component = roots[i].GetComponent<T>();
				return true;
			}
		}
		component = null;
		return false;
	}
    #endregion

    #region List
    public static bool TryFindByType<T,J>(this List<T> list, out J result) where J : class
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] is J)
            {
                result = list[i] as J;
                return true;
            }
        }
        result = default;
        return false;
    }
    #endregion

    #region String
    public static string AddSpacesToSentence(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }
    #endregion


}
