﻿

namespace TrapLight.Events
{
    public class EventService
    {
        public EventController<int> OnMapSelected { get; private set; }

        public EventService()
        {
            OnMapSelected = new EventController<int>();
        }

    }
}