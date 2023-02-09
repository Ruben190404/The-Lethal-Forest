using UnityEngine;

public class Hat : MonoBehaviour
{
    private int Wins;
    [SerializeField] private GameObject HatObject;
    
    void Start()
    {
        Renderer rend = HatObject.GetComponent<Renderer>();
        Material mat = rend.material;
        
        Wins = PlayerPrefs.GetInt("Wins");

        if (Wins >= 10)
        {
            HatObject.SetActive(true);
            if (Wins >= 100)
            {
                if (mat.HasProperty("_Metallic"))
                {
                    mat.color = new Color(1f, 0.843f, 0f);
                    mat.SetFloat("_Metallic", 0.8f);
                }
                else
                {
                    mat.shader = Shader.Find("Standard");
                    mat.color = new Color(1f, 0.843f, 0f);
                    mat.SetFloat("_Metallic", 0.8f);
                }
            }
        }
    }
}
