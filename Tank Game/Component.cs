namespace Tank_Game
{
    internal class Component
    {
        public GameObject AttachedObject; // Object that the component is attached to
        public virtual void Destroy () { }
        public virtual void Update() { }
        public virtual void FixedUpdate(ref float DeltaTime) { }
    }
}
