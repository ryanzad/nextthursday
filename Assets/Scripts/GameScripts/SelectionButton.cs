using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionButton : MonoBehaviour {

    public SelectionDetection selectionDetection;
    public TextMesh textMesh;
    public Renderer iconRenderer;
    
	public void Setup (Modifiers.Modifier mod, SelectionHandler.ModifierDisplay modDisplay) {
        selectionDetection.mod = mod;
        textMesh.text = modDisplay.text;
        iconRenderer.material.mainTexture = modDisplay.icon;
    }

	
}
