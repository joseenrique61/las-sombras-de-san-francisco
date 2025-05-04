using UnityEngine;

namespace Interactions
{
    public class InteractionAnimation : MonoBehaviour
    {
        private Animator objectAnimator;

        void Awake()
        {
            objectAnimator = GetComponent<Animator>();
            if (objectAnimator == null)
            {
                Debug.LogError("No se encontró el componente Animator en " + gameObject.name);
            }
        }

        public void PlayOnceAnimation()
        {
            if (objectAnimator != null)
            {
                objectAnimator.SetTrigger("play");
            }
        }

        public void ResetValues()
        {
            if (objectAnimator != null && objectAnimator.isActiveAndEnabled)
            {
                objectAnimator.ResetTrigger("play");
            }
        }
    }
}