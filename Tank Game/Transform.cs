using MathClasses;

namespace Tank_Game
{
    internal class Transform : Component
    {
        #region Variables
        #region Public Propeties
        // === Global ===
        public Matrix3 GlobalMatrix // Gets the Global Matrix
        {
            get
            {
                UpdateTransform(false); // Will Update this Transform if it is Dirty
                return m_GlobalMatrix;  // Returns the Private Global Matrix
            }
        }
        public Vector3 GlobalPosition // Gets the Global Translation
        {
            get
            {
                UpdateTransform(false); // Will Update this Transform if it is Dirty
                return new Vector3(GlobalMatrix.m20, GlobalMatrix.m21, 0); // Returns the Translation as a Vector 3
            }
        }
        public float GlobalRotation
        {
            get
            {
                UpdateTransform(false);                 // Will Update this Transform if it is Dirty
                Matrix3 TempGlobal = GlobalMatrix;      // Creates a Copy of the Global Matrix
                TempGlobal.m20 = 0; TempGlobal.m21 = 0; // Sets the Translation to 0 for Both X and Y

                float XMagnitude = new Vector3(TempGlobal.m00, TempGlobal.m01, TempGlobal.m02).Magnitude(); // Calculates the Scale of the First Row in Temp Global 
                TempGlobal.m00 /= XMagnitude; TempGlobal.m01 /= XMagnitude; // Seperates the Rotation of m00 and m01 from the TempGlobal by Dividing with the Scale

                return (float)Math.Atan2(TempGlobal.m00, TempGlobal.m01); // Returns the Rotation around the Z-Axis 
            }
        }
        public Vector3 GlobalScale
        {
            get
            {
                UpdateTransform(false);                 // Will Update this Transform if it is Dirty
                Matrix3 TempGlobal = GlobalMatrix;      // Creates a Copy of the Global Matrix
                TempGlobal.m20 = 0; TempGlobal.m21 = 0; // Sets the Translation to 0 for Both X and Y

                float XMagnitude = new Vector3(TempGlobal.m00, TempGlobal.m01, TempGlobal.m02).Magnitude(); // Calculates the Scale of the First Row in Temp Global 
                float YMagnitude = new Vector3(TempGlobal.m10, TempGlobal.m11, TempGlobal.m02).Magnitude(); // Calculates the Scale of the Second Row in Temp Global 

                return new Vector3(XMagnitude, YMagnitude, 1); // Returns the Scale as a Vector3
            }
        }

        // === Local ===
        public Matrix3 LocalMatrix
        {
            get  // Returns the Private Local Matrix
            {
                UpdateTransform(false); // Will Update this Transform if it is Dirty
                return m_LocalMatrix;
            }
        }
        public Vector3 LocalPosition
        {
            get // Returns the Private Local Position
            { 
                return m_Position;
            }

            set // Sets the Private Local Position to Value and then Marks this Transform as Dirty
            { 
                m_Position = value;
                Dirty = true;
            } 
        }
        public float LocalRotation {
            get // Returns the Private Local Rotation
            { 
                return m_Rotation;
            }
            set // Sets the Private Local Rotation to Value and then Marks this Transform as Dirty
            {
                m_Rotation = value;
                Dirty = true;
            }
        }
        public Vector3 LocalScale
        {
            get // Returns the Private Local Scale
            {
                return m_Scale;
            }
            set // Sets the Private Local Scale to Value and then Marks this Transform as Dirty
            { 
                m_Scale = value;
                Dirty = true;
            }
        }
        #endregion

        #region Private Members
        private Matrix3 m_GlobalMatrix = new Matrix3();
        private Matrix3 m_LocalMatrix = new Matrix3();
        private Vector3 m_Position = new Vector3();
        private float m_Rotation = 0;
        private Vector3 m_Scale = new Vector3(1, 1, 1);

        private bool Dirty; // Is Dirty if a Component or Parent has been changed and Transform needs to be Updated
        #endregion
        #endregion

        public Transform(GameObject Object)
        {
            // === Attached Object Handling ===
            AttachedObject = Object; // Sets the Object this Component is Attached to
            AttachedObject.AddComponent(this); // Adds this Component to the Attached Objects Component List
        }

        public void UpdateTransform(bool Force)
        {
            if (Dirty || Force)
            {
                Matrix3 Translation = Matrix3.CreateTranslation(m_Position.x, m_Position.y);
                Matrix3 Rotation = Matrix3.CreateRotationZ(m_Rotation);
                Matrix3 Scale = Matrix3.CreateScale(m_Scale.x, m_Scale.y);

                m_LocalMatrix = Translation * Rotation * Scale;
                m_GlobalMatrix = (AttachedObject.Parent == null ? m_LocalMatrix : AttachedObject.Parent.Transform.m_GlobalMatrix * m_LocalMatrix);
                Dirty = false;

                foreach (GameObject Child in AttachedObject.Children)
                    Child.Transform.UpdateTransform(true);
            }
        }

        #region Overrides
        public override void Update()
        {
            UpdateTransform(false);
        }
        #endregion

    }
}
