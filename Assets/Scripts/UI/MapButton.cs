using System;
using System.Collections.Generic;
using TrapLight.Events;
using UnityEngine;
using UnityEngine.UI;

namespace TrapLight.UI
{
    public class MapButton : MonoBehaviour
    {

        [SerializeField] private int MapId;
        private EventService eventService;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);
        public void Init(EventService eventService)
        {
            this.eventService = eventService;
        }
     
        private void OnMapButtonClicked() => eventService.OnMapSelected.InvokeEvent(MapId);
    }
}
