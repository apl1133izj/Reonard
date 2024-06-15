using UnityEngine;

public class ChageMap : MonoBehaviour
{
    public enum mapType { Home, Fram }
    public mapType map;
    public GameObject[] chageMap; //0:���� 1:��
    public GameObject[] camera;
    private void Start()
    {
        map = mapType.Home;
    }
    public void FramChange()
    {
        map = mapType.Fram;
        camera[1].SetActive(true);
        camera[0].SetActive(false);
        chageMap[0].SetActive(true);
        if (map == mapType.Home)
        {
            GameObject.Find("UIBackGameObject").SetActive(false);
        }
        if (map == mapType.Fram)
        {
            GameObject.Find("UIBackGameObject").SetActive(false);
        }
    }

    public void HomeChange()
    {
        map = mapType.Home;
        camera[0].SetActive(true);
        camera[1].SetActive(false);
        chageMap[0].SetActive(false);
        GameObject.Find("instCanvas").transform.GetChild(0).gameObject.SetActive(true);
    }
}
