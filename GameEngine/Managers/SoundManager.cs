using System;
using System.Media;

namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    class SoundManager
    {
        private GameEngine _gameEngine;
        private SoundPlayer _backgroundSoundPlayer;
                
        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void PlayBackgroundSound(string backgroundSoundPath)
        {
            if (_backgroundSoundPlayer != null)
            {
                _backgroundSoundPlayer.Stop();
                _backgroundSoundPlayer.Dispose();
            }
            _backgroundSoundPlayer = new SoundPlayer(backgroundSoundPath);
            _backgroundSoundPlayer.PlayLooping();
        }

        public void StopBackgroundSound()
        {
            _backgroundSoundPlayer.Stop();
        }


        public void PlaySound(string soundPath)
        {
            String fullPath = _gameEngine.audioPath + soundPath;
            SoundPlayer soundPlayer = new SoundPlayer(fullPath); // TODO sound not play parallely of the background music
            soundPlayer.PlaySync();
        }
        
    }
}