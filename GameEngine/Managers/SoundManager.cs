using System;
using System.Media;

namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    class SoundManager
    {
        private GameEngine _gameEngine;
        private MediaPlayer.MediaPlayer _backgroundSoundPlayer = new MediaPlayer.MediaPlayer();
                
        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void PlayBackgroundSound(string backgroundSoundPath)
        {
            _backgroundSoundPlayer.Stop();
            _backgroundSoundPlayer.Open(backgroundSoundPath);
            _backgroundSoundPlayer.Play();
        }

        public void StopBackgroundSound()
        {
            _backgroundSoundPlayer.Stop();
        }

        public void PlaySound(string soundPath)
        {
            String fullPath = _gameEngine.audioPath + soundPath;
            SoundPlayer soundPlayer = new SoundPlayer(fullPath);
            soundPlayer.Play();
        }
        
    }
}