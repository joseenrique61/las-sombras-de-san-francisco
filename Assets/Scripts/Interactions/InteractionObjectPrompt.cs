using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interactions
{
    public class InteractionObjectPrompt : MonoBehaviour
    {
        public List<GameObject> InteractionPrompts;
        public void Awake()
        {
            if (InteractionPrompts == null)
            {
                Debug.LogError("Hace falta agregar uno o más prompts.");
            }
        }

        public void ShowPrompt()
        {
            InteractionPrompts.First().SetActive(true);
        }

        public void HidePrompt()
        {
            InteractionPrompts.First().SetActive(false);
        }

        public void ShowPrompt(string name)
        {
            foreach (GameObject prompt in InteractionPrompts)
            {
                if (prompt.name == name)
                {
                    Debug.Log($"Activando prompt {prompt.name}");
                    prompt.SetActive(true);
                    break;
                }
            }
        }

        public void HidePrompt(string name)
        {
            foreach (GameObject prompt in InteractionPrompts)
            {
                if (prompt.name == name)
                {
                    prompt.SetActive(false);
                    break;
                }
            }
        }
    }
}