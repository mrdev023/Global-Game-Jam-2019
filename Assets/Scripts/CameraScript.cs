using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Material material;
    public FearLevel fear;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Fear", fear.GetFearLevel());
        // Copy the source Render Texture to the destination,
        // applying the material along the way.
        Graphics.Blit(source, destination, material);
    }

}
