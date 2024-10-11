using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EventManager Instance;
    private Dictionary<int, bool> eventCompleted = new Dictionary<int, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    // Starting an event
    public bool StartEvent(int index)
    {
        if (!eventCompleted.ContainsKey(index))
        {
            eventCompleted[index] = false;
            return true;
        }
        return false; 
    }

    //Check Completion


    // Update is called once per frame
    public bool isCompleted(int index)
    {
        if (eventCompleted.ContainsKey(index))
        {
            return eventCompleted[index];
        }
        return false; 
    }

    public void CompleteEvent(int index)
    {
        if (eventCompleted.ContainsKey(index)) {
            eventCompleted[index] = true;
        }
    }
}
