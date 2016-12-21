using UnityEngine;

public class Timer : MonoBehaviour{
    public delegate void TickAction();
    public event TickAction OnTick;
    public float interval;
    private bool oneTime;

    public static Timer CreateTimer(GameObject parent, float interval, Timer.TickAction tickAction, bool oneTime = false) {
        Timer timer = parent.AddComponent<Timer>();
        timer.interval = interval;
        timer.OnTick += tickAction;
        timer.StartTimer();
        timer.oneTime = oneTime;
        return timer;
    }

    public static Timer CreateInvocation(GameObject parent, float delay, Timer.TickAction action) {
        return CreateTimer(parent, delay, action, true);
    }

    private float lastIntervalTime;
    private bool active;

    public void StartTimer() {
        active = true;
        lastIntervalTime = Time.time;
    }

    public void StopTimer() {
        active = false;
    }

    private void Update() {
        if (active && lastIntervalTime + interval <= Time.time) {
            lastIntervalTime = Time.time;
            if (OnTick != null) {
                OnTick();
                if(oneTime) {
                    Destroy(this);
                }
            }
        }
    }
}
