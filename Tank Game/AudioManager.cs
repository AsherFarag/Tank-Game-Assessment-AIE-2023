using Raylib_cs;

namespace Tank_Game
{
    class AudioManager
    {
        
        public Sound TankMovementSounds = new Sound();
        public string TankMovementAddresses = @"../../Sounds/TankMovementSounds/MoveYes.wav";

        public Sound TankFiringSounds = new Sound();
        public string TankFiringAddresses = @"../../Sounds/TankFiringSounds/ShootYes.wav";

        public Sound TurretRotatingSounds = new Sound();
        public string TurretRotatingAddresses = @"../../Sounds/TurretRotateSounds/RotateYes.wav";

        public List<Sound> Sounds;
        public List<string> SoundAddressList;
        public AudioManager()
        {
            Raylib.InitAudioDevice();

            #region Sounds List Handler
            Sounds = new List<Sound>();
            SoundAddressList = new List<string>();

            SoundAddressList.Add(TankMovementAddresses);
            SoundAddressList.Add(TankFiringAddresses);
            SoundAddressList.Add(TurretRotatingAddresses);

            Sounds.Add(TankMovementSounds);
            Sounds.Add(TankFiringSounds);
            Sounds.Add(TurretRotatingSounds);

            Load();
            #endregion
        }

        #region SoundHandler
        private void Load ()
        {
            TankMovementSounds = Raylib.LoadSound(TankMovementAddresses);
            TankFiringSounds = Raylib.LoadSound(TankFiringAddresses);
            TurretRotatingSounds = Raylib.LoadSound(TurretRotatingAddresses);
            //foreach (string SoundAddress in SoundAddressList)
            //{
            //    for (int i = 0; i < SoundAddressList.Count; i++)
            //        Sounds[i] = Raylib.LoadSound(SoundAddress);
            //}
        }
        
        public void PlaySound(bool Stop, int SoundToPlay)
        {
            if (Stop)
            {
                switch (SoundToPlay)
                {
                    case 1:
                    //TankMovementSounds[m_Index].stream;
                    Raylib.StopSound(TankMovementSounds);
                        break;
                    case 2:
                        Raylib.StopSound(TankFiringSounds);
                        break;
                    case 3:
                        Raylib.StopSound(TurretRotatingSounds);
                        break;
                }
            }
            else
            {
                switch (SoundToPlay)
                {
                    case 1:
                        //TankMovementSounds[m_Index].stream;
                        Raylib.PlaySound(TankMovementSounds);
                        break;
                    case 2:
                        Raylib.PlaySound(TankFiringSounds);
                        break;
                    case 3:
                        Raylib.PlaySound(TurretRotatingSounds);
                        break;
                }
            }
        }

        //public void PlayStartSound(Sound[] SoundArrayToPlay, int m_Index)
        //{
        //    Raylib.PlaySound(TankFiringSounds[m_Index]);
        //}
        //public void PlayMidSound(Sound[] SoundArrayToPlay, int m_Index)
        //{
        //    Raylib.PlaySound(TankFiringSounds[m_Index]);
        //}
        //public void PlayStartSound(Sound[] SoundArrayToPlay, int m_Index)
        //{
        //    Raylib.PlaySound(TankFiringSounds[m_Index]);
        //}
        #endregion

    }
}
