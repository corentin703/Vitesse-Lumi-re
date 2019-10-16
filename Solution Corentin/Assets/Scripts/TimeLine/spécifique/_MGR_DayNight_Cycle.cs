using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class _MGR_DayNight_Cycle : MonoBehaviour
{
    private static _MGR_DayNight_Cycle p_instance = null;
    public static _MGR_DayNight_Cycle Instance { get { return p_instance; } }

    private Light m_pLight;

    [SerializeField]
    private Camera player_camera;

    public List<Color> lightColors = new List<Color>();

    public void Awake()
    {
        if (p_instance == null)
            p_instance = this;
        else if (p_instance != this)
            Destroy(gameObject);

        m_pLight = this.gameObject.GetComponent<Light>();

        SetColor(lightColors[0]);
        player_camera.backgroundColor = new Color(0, 0, 0, 0);
    }

    public void UpdateCycle()
    {
        Debug.Log("Day/Night cycle update");

        if (lightColors.IndexOf(m_pLight.color) + 1 == lightColors.Count)
            SetColor(lightColors[0]);
        else
            SetColor(lightColors[lightColors.IndexOf(m_pLight.color) + 1]);
    }

    private void SetColor(Color lightColor)
    {
        if (lightColor.r <= 0.4f && lightColor.g <= 0.4f && lightColor.b <= 0.4f)
        {
            player_camera.clearFlags = CameraClearFlags.SolidColor;
            //player_camera.backgroundColor = new Color(0, 0, 0, 0);
            m_pLight.color = lightColor;
        }
        else
        {
            player_camera.clearFlags = CameraClearFlags.Skybox;
            m_pLight.color = lightColor;
        }
    }

}
