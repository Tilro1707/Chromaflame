using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [Header("Random sound variations")]
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip[] deathSounds;

    [Header("Single sounds")]
    [SerializeField] private AudioClip colourSwitchSound;
    [SerializeField] private AudioClip victorySound;

    private AudioSource audioSource;
    private int lastJumpIndex = -1;
    private int lastDeathIndex = -1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        PlayRandomSound(jumpSounds, ref lastJumpIndex);
    }

    public void PlayDeath()
    {
        PlayRandomSound(deathSounds, ref lastDeathIndex);
    }

    public void PlayColourSwitch()
    {
        PlaySound(colourSwitchSound);
    }

    public void PlayVictory()
    {
        PlaySound(victorySound);
    }

    private void PlayRandomSound(AudioClip[] sounds, ref int lastIndex)
    {
        if (sounds == null || sounds.Length == 0)
        {
            return;
        }

        int randomIndex;

        if (sounds.Length == 1)
        {
            randomIndex = 0;
        }
        else
        {
            do
            {
                randomIndex = Random.Range(0, sounds.Length);
            }
            while (randomIndex == lastIndex);
        }

        lastIndex = randomIndex;
        PlaySound(sounds[randomIndex]);
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound == null)
        {
            return;
        }

        audioSource.PlayOneShot(sound);
    }
}
