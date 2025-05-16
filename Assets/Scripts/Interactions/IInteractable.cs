using UnityEngine;
using Player;

namespace Interactions
{
    public interface IInteractable
    {
        void Interact(GameObject player);
        void ShowPrompt();
        void HidePrompt();
        void OnEnterRange(GameObject player);
        void OnExitRange(GameObject player);
    }
}