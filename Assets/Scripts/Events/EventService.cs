using UnityEngine;

namespace TrapLight.Events
{
    public class EventService : MonoBehaviour
    {
        public EventController<int> OnMapSelected { get; private set; }

        private void Awake()
        {
            OnMapSelected = new EventController<int>();
        }

    }
}