using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NewEvents
{
    public string name { get; set; }
    public IEnumerable eventRoutine { get; set; }

    public NewEvents(string name, IEnumerable routine)
    {
        this.name = name;
        this.eventRoutine = routine;
    }
}
