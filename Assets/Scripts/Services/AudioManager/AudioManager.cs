using Enums;
using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using AnnulusGames.LucidTools.Audio;
using System.Collections.Generic;
using Game.Code.Common.CoroutineRunner;
using Gameplay.GameParameters.EntitiesParameters;

namespace Services
{
    public class AudioManager : MonoBehaviour
    {
        private CoroutineRunner coroutineRunner;
        private List<SoundGroup<UnitType>> activeAttackSoundsAudioPlayers = new List<SoundGroup<UnitType>>();
        private List<SoundGroup<UnitType>> activeUnitsSpawnSoundsAudioPlayers = new List<SoundGroup<UnitType>>();
        private List<SoundGroup<UnitType>> activeUnitsDeathSoundsAudioPlayers = new List<SoundGroup<UnitType>>();

        private List<SoundGroup<YeeHawActionType>> activeUYeeHawActionTypeSpawnSoundsAudioPlayers =
            new List<SoundGroup<YeeHawActionType>>();

        [SerializeField] private EntitiesSounds entitiesSounds;
        [SerializeField] private AudioClip[] boozerBombExplosionClip;
        [SerializeField] private AudioClip[] airplaneBombExplosionClip;

        public void Initialize(CoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }

        public void AddEntitySoundArtist<T>(T entityType, List<SoundGroup<T>> activeEntitySoundsAudioPlayers,
            AudioClip[] sounds)
        {
            SoundGroup<T> soundGroup = activeEntitySoundsAudioPlayers.FirstOrDefault(activeAudioPlayers =>
                activeAudioPlayers.entityType.Equals(entityType));

            if (!activeEntitySoundsAudioPlayers.Any(activeAudioPlayers =>
                    activeAudioPlayers.entityType.Equals(entityType)))
            {
                soundGroup = new SoundGroup<T>(entityType, null, 1);
                activeEntitySoundsAudioPlayers.Add(soundGroup);
                PlayRandomClip<T>(soundGroup, sounds);
            }
            else if(soundGroup.artistsCount == 0)
            {
                soundGroup.artistsCount++;
                PlayRandomClip<T>(soundGroup, sounds);
            }
        }


        public void AddUnitSpawnSoundArtist(UnitType unitType)
        {
            AudioClip[] spawnSounds = UnitTypeToSpawnAudioCLips(unitType);
            if (spawnSounds != null && spawnSounds.Length > 0)
            {
                AddEntitySoundArtist<UnitType>(unitType, activeUnitsSpawnSoundsAudioPlayers, spawnSounds);
            }
        }

        public void AddUnitDeathSoundArtist(UnitType unitType)
        {
            AudioClip[] spawnSounds = UnitTypeToDeathAudioCLips(unitType);
            if (spawnSounds != null && spawnSounds.Length > 0)
            {
                AddEntitySoundArtist<UnitType>(unitType, activeUnitsDeathSoundsAudioPlayers, spawnSounds);
            }
        }

        public void RemoveUnitDeathSoundArtist(UnitType unitType)
        {
            RemoveEntitySoundArtist<UnitType>(unitType, activeUnitsDeathSoundsAudioPlayers);
        }

        public void PlayAddYeeHawPowerSpawnSound(YeeHawActionType yeeHawActionType)
        {
            AudioClip[] spawnSounds = YeeHawActionTypeToSpawnAudioCLips(yeeHawActionType);
            if (spawnSounds != null && spawnSounds.Length > 0)
                PlayRandomClip<YeeHawActionType>(spawnSounds);
        }

        public void RemoveEntitySoundArtist<T>(T entityType,
            List<SoundGroup<T>> activeEntitySoundsAudioPlayers)
        {
            if (activeEntitySoundsAudioPlayers.Any(activeAudioPlayers =>
                    activeAudioPlayers.entityType.Equals(entityType)))
            {
                SoundGroup<T> existingEntry =
                    activeEntitySoundsAudioPlayers.FirstOrDefault(activeAudioPlayers =>
                        activeAudioPlayers.entityType.Equals(entityType));
                int newArtistCount = existingEntry.artistsCount - 1;
                existingEntry.artistsCount = newArtistCount;
                if (newArtistCount == 0)
                {
                    existingEntry.audioPlayer.Stop();
                    activeEntitySoundsAudioPlayers.Remove(existingEntry);
                }
            }
        }

        public void RemoveUnitSpawnSoundArtist(UnitType unitType)
        {
            RemoveEntitySoundArtist<UnitType>(unitType, activeUnitsSpawnSoundsAudioPlayers);
        }

        public void AddUnitAttackSoundArtist(UnitType unitType, float cooldown)
        {
            AudioClip[] attackSounds = UnitTypeToAttackAudioCLips(unitType);

            if (attackSounds != null && attackSounds.Length > 0)
            {
                if (!activeAttackSoundsAudioPlayers.Any(activeAudioPlayers =>
                        activeAudioPlayers.entityType == unitType))
                {
                    SoundGroup<UnitType> soundGroup = new SoundGroup<UnitType>(unitType, null, 1);
                    activeAttackSoundsAudioPlayers.Add(soundGroup);
                    coroutineRunner.RunCoroutine(PlayRandomClip<UnitType>(soundGroup, attackSounds, cooldown));
                }

                else
                {
                    SoundGroup<UnitType> existingEntry =
                        activeAttackSoundsAudioPlayers.FirstOrDefault(activeAudioPlayers =>
                            activeAudioPlayers.entityType == unitType);
                    int newArtistCount = existingEntry.artistsCount + 1;
                    existingEntry.artistsCount = newArtistCount;
                }
            }
        }

        public void RemoveAttackSoundArtist(UnitType unitType)
        {
            if (activeAttackSoundsAudioPlayers.Any(activeAudioPlayers => activeAudioPlayers.entityType == unitType))
            {
                SoundGroup<UnitType> existingEntry =
                    activeAttackSoundsAudioPlayers.FirstOrDefault(activeAudioPlayers =>
                        activeAudioPlayers.entityType == unitType);

                if (existingEntry != null)
                {
                    int newArtistCount = existingEntry.artistsCount - 1;
                    existingEntry.artistsCount = newArtistCount;
                    if (newArtistCount == 0)
                    {
                        if (existingEntry.audioPlayer != null)
                            existingEntry.audioPlayer.Stop();
                        activeAttackSoundsAudioPlayers.Remove(existingEntry);
                    }
                }
            }
        }

        public AudioClip[] UnitTypeToAttackAudioCLips(UnitType unitType)
        {
            AudioClip[] attackSounds = unitType switch
            {
                UnitType.Digger => entitiesSounds.diggerAttackSounds,
                UnitType.Shooter => entitiesSounds.shooterAttackSounds,
                UnitType.Boozer => entitiesSounds.boozerAttackSounds,
                UnitType.SurvivalistCar => entitiesSounds.survivalistCarAttackSounds,
                UnitType.Cleric => entitiesSounds.clericAttackSounds,
                UnitType.Zombie => entitiesSounds.zombieAttackSounds,
                UnitType.ZombieJumper => entitiesSounds.zombieJumperAttackSounds,
                UnitType.Banshee => entitiesSounds.bansheeAttackSounds,
                UnitType.Giant => entitiesSounds.giantAttackSounds,
                _ => null
            };

            return attackSounds;
        }

        public AudioClip[] UnitTypeToSpawnAudioCLips(UnitType unitType)
        {
            AudioClip[] spawnSounds = unitType switch
            {
                UnitType.Cleric => entitiesSounds.clericSpawnSounds,
                UnitType.Zombie => entitiesSounds.zombieSpawnSounds,
                UnitType.Banshee => entitiesSounds.bansheeSpawnSounds,
                UnitType.Giant => entitiesSounds.giantSpawnSounds,
                _ => null
            };

            return spawnSounds;
        }

        public AudioClip[] YeeHawActionTypeToSpawnAudioCLips(YeeHawActionType yeeHawActionType)
        {
            AudioClip[] attackSounds = yeeHawActionType switch
            {
                YeeHawActionType.CrowdSummon => entitiesSounds.crowdSpawnSounds,
                YeeHawActionType.Bombardment => entitiesSounds.airstrikeSpawnSounds,
                _ => null
            };

            return attackSounds;
        }

        public AudioClip[] UnitTypeToDeathAudioCLips(UnitType unitType)
        {
            AudioClip[] spawnSounds = unitType switch
            {
                UnitType.Digger => entitiesSounds.diggerDeathSounds,
                UnitType.Shooter => entitiesSounds.shooterDeathSounds,
                UnitType.Boozer => entitiesSounds.boozerDeathSounds,
                UnitType.SurvivalistCar => entitiesSounds.survivalistCarDeathSounds,
                UnitType.Cleric => entitiesSounds.clericDeathSounds,
                _ => null
            };

            return spawnSounds;
        }

        public IEnumerator PlayRandomClip<T>(SoundGroup<T> soundGroup, AudioClip[] audioClips, float cooldown)
        {
            if (audioClips != null && audioClips.Length != 0)
            {
                while (soundGroup.artistsCount > 0)
                {
                    yield return new WaitForSeconds(cooldown);
                    AudioClip audioClip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
                    soundGroup.audioPlayer = LucidAudio.PlaySE(audioClip);
                }
            }
        }

        public void PlayRandomClip<T>(SoundGroup<T> soundGroup, AudioClip[] audioClips)
        {
            if (audioClips != null && audioClips.Length != 0)
            {
                AudioClip audioClip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
                soundGroup.audioPlayer = LucidAudio.PlaySE(audioClip).OnComplete(() => { soundGroup.artistsCount--; });
            }
        }

        public void PlayRandomClip<T>(AudioClip[] audioClips)
        {
            if (audioClips != null && audioClips.Length != 0)
            {
                AudioClip audioClip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
                LucidAudio.PlaySE(audioClip);
            }
        }

        public void PlayBombExplosionClip(BombType bombType)
        {
            AudioClip[] audioClips = bombType switch
            {
                BombType.BoozerBomb => boozerBombExplosionClip,
                BombType.AirstrikeBomb => airplaneBombExplosionClip,
                _ => null
            };

            if (audioClips != null && audioClips.Length != 0)
            {
                AudioClip audioClip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
                LucidAudio.PlaySE(audioClip);
            }
        }

        public void PlayYeeHawSound()
        {
            if (entitiesSounds.yeeHawSpawnSounds != null && entitiesSounds.yeeHawSpawnSounds.Length != 0)
            {
                AudioClip audioClip =
                    entitiesSounds.yeeHawSpawnSounds[
                        UnityEngine.Random.Range(0, entitiesSounds.yeeHawSpawnSounds.Length)];
                LucidAudio.PlaySE(audioClip);
            }
        }
    }

    public class SoundGroup<T>
    {
        public T entityType;
        public AudioPlayer audioPlayer;
        public int artistsCount;

        public SoundGroup(T entityType, AudioPlayer audioPlayer, int artistsCount)
        {
            this.entityType = entityType;
            this.audioPlayer = audioPlayer;
            this.artistsCount = artistsCount;
        }
    }
}