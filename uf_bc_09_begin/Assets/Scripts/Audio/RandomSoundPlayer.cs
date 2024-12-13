using System.Collections;   
using System.Collections.Generic;   
using UnityEngine;   

namespace UnityFundamentals
{
    // This class handles the playing of random audio clips from a list, with optional pitch, volume, and probability variations.
    // It also supports looped playback with time variations, and allows the user to start/stop the loop or play a sound once.
    //
    // @author J.C. Wichman

    public class RandomSoundPlayer : MonoBehaviour
    {
        // List of audio clips that can be played randomly.
        [Header("Audio Clips")]
        public List<AudioClip> audioClips;

        // Controls how much the pitch and volume can vary randomly from the default values.
        [Header("Pitch and Volume Variations")]
        public float pitchVariation = 0f;
        public float volumeVariation = 0f;

        // Probability that a sound will play when requested. Range is from 0 (never play) to 1 (always play).
        [Header("Playback Probability")]
        [Range(0f, 1f)]
        public float playProbability = 1f;

        // Settings for looping sound playback, including base loop time and optional variation in time between loops.
        [Header("Loop Settings")]
        public float loopTime = 1f;
        public float loopTimeVariation = 0f;
        public bool loopOnStart = false;   

        // Private fields for audio management
        private AudioSource audioSource;   // The AudioSource component that plays the sound
        private Coroutine loopCoroutine;   // Reference to the coroutine running the sound loop

        private float originalPitch;
        private float originalVolume;

        // Called when the script is first initialized. This method sets up the AudioSource component.
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            originalPitch = audioSource.pitch;
            originalVolume = audioSource.volume;
        }

        // Called at the start of the game. If `loopOnStart` is true, the sound loop starts automatically.
        private void Start()
        {
            if (loopOnStart) StartLoop();
        }

        // Starts looping the sound if not already started.
        public void StartLoop()
        {
            if (loopCoroutine == null)
            {
                loopCoroutine = StartCoroutine(PlayLoop());
            }
        }

        // Stops the sound loop if it is running.
        public void StopLoop()
        {
            if (loopCoroutine != null)
            {
                StopCoroutine(loopCoroutine);
                loopCoroutine = null;
            }
        }

        // Plays a sound once. The parameter `pAlways` forces the sound to play regardless of the `playProbability`.
        public void PlayOnce(bool pAlways)
        {
            TryPlaySound(pAlways);
        }

        // Coroutine that manages the looped playback of sounds. It plays a sound, waits for a randomized interval, then repeats.
        private IEnumerator PlayLoop()
        {
            while (true)
            {
                TryPlaySound();

                // Calculate next loop time with random variation applied
                float nextLoopTime = loopTime + Random.Range(-loopTimeVariation, loopTimeVariation);
                yield return new WaitForSeconds(nextLoopTime);
            }
        }

        // Attempts to play a sound based on the `playProbability`, or always plays if `pAlways` is true.
        private void TryPlaySound(bool pAlways = false)
        {
            // Check if there are any audio clips to play
            if (audioClips.Count == 0)
            {
                Debug.LogWarning("No audio clips assigned.");
                return;
            }

            // Determine if the sound should play based on the `playProbability`
            if (pAlways || Random.value <= playProbability)
            {
                // Select a random audio clip from the list
                AudioClip clip = audioClips[Random.Range(0, audioClips.Count)];

                // Set random pitch and volume variations
                audioSource.pitch = originalPitch + Random.Range(-pitchVariation, pitchVariation);
                audioSource.volume = originalVolume + Random.Range(-volumeVariation, volumeVariation);

                // Play the selected audio clip
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
