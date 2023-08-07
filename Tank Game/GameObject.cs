using System;
using System.Runtime.InteropServices;
using MathClasses;

namespace Tank_Game
{
    class GameObject
    {
        public GameObject Parent; // The Game Object this Object is a Child to  // Can be Null
        public List<GameObject> Children; // List of Game Objects that are Attached to this Object
        public List<Component> Components; // List of Game Components that are Attached to this Object
        public Transform Transform; // Public Transform that Handles Position, Rotation, and Scale

        public GameObject()
        {
            EngineAPI.Instance.RegisterGameObject(this); // Registers this Object to the List of Game Objects in the Engine API

            // === List Initialising ===
            Children = new List<GameObject>();
            Components = new List<Component>();
            Transform = new Transform(this);
        }
        public virtual void Draw() { }
        public virtual void LateDraw() { }
        public virtual void Tick(ref float DeltaTime)
        {
            FixedUpdate(ref DeltaTime);
            UpdateTransforms();
            UpdateComponents();
        }

        #region Heirarchy Handling
        public void AddChild(GameObject Object) // Adds the Object to the List of Children 
        {
            Children.Add(Object);
        }
        public void RemoveChild(GameObject Object) // Removes the Object from the List of Children 
        { 
            Children.Remove(Object); 
        }
        public void AddComponent(Component Component) // Adds the Component to the List of Components 
        {
            Components.Add(Component); 
        }
        public void RemoveComponent(Component Component) // Removes the Component from the List of Components 
        {
            Components.Remove(Component);
        }
        public void UpdateComponents() // Updates Each Component in the List of Components 
        {
            foreach (Component CurrentComponent in Components)
            {
                CurrentComponent.Update();
            }
        }


        //T GetComponent<T>() where T : Component
        //{
        //    return Components.Find(comp => comp.GetType() == typeof(T)) as T;
        //}

        public void DestroyAndAllComponents()
        {
            foreach (Component CurrentComponent in Components)
                CurrentComponent.Destroy();
            Components.Clear();
            EngineAPI.Instance.UnregisterGameObject(this);
        }
        public void DestroyAndAllChildren()
        {
            DestroyAndAllComponents();
            Children.Clear();
        }
        #endregion

        #region Transform Handling
        public virtual void FixedUpdate(ref float DeltaTime)
        {
            foreach (Component CurrentComponent in Components)
                CurrentComponent.FixedUpdate(ref DeltaTime);
        }
        public void UpdateTransforms() // Updates the Global Transfrom and for Any Child
        {
            Transform.Update();
        }
        #endregion
    }
}
