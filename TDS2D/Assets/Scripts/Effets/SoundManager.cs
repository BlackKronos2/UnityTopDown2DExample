using UnityEngine;


/// <summary>
/// Звуки игры
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[SerializeField]
	[Range(0f, 1f)] 
	private float _soundEffectVolume;
	[SerializeField]
	[Range(0f, 1f)] 
	private float _soundEffectPitchVariance;
	[SerializeField]
	[Range(0f, 1f)]
	private float _musicVolume;

	private AudioSource _musicAudioSource;

	private void Awake()
	{
		Instance = Instance ?? this;
		_musicAudioSource = GetComponent<AudioSource>();
	}

	/// <summary>
	/// Воспроизводит звуковой эффект в указанной позиции
	/// </summary>
	/// <param name="audio">Звуковой эффект для воспроизведения</param>
	/// <param name="position">Позиция, в которой проигрывать звук</param>
	public static void PlaySoundEffect(AudioClip audio, Vector3 position) => PlayClipAtPoint(audio, position, true);

	/// <summary>
	/// Воспроизводит звуковой эффект глобально
	/// </summary>
	/// <param name="audio">Звуковой эффект для воспроизведения</param>
	public static void PlaySoundEffect(AudioClip audio) => PlayClipAtPoint(audio, Vector3.zero, false);

	/// <summary>
	/// Изменяет фоновую музыку
	/// </summary>
	/// <param name="music">Новая музыка для воспроизведения</param>
	public static void ChangeBackGroundMusic(AudioClip music)
	{
		Instance._musicAudioSource.Stop();
		Instance._musicAudioSource.clip = music;
		Instance._musicAudioSource.Play();
	}

	/// <summary>
	/// Создает и воспроизводит временный AudioSource для воспроизведения одного звука
	/// </summary>
	/// <param name="clip">Клип для воспроизведения</param>
	/// <param name="pos">Позиция, в которой проигрывать клип</param>
	/// <param name="spatialize">Следует ли пространственно организовывать звук или нет</param>
	private static void PlayClipAtPoint(AudioClip clip, Vector3 pos, bool spatialize)
	{
		GameObject gameObject = new GameObject("TempAudio");
		gameObject.transform.position = pos;
		AudioSource source = gameObject.AddComponent<AudioSource>(); // добавить источник звука
		source.clip = clip; 
		source.volume = Instance._soundEffectVolume;

		source.Play(); 
		source.pitch = 1f + Random.Range(-Instance._soundEffectPitchVariance, Instance._soundEffectPitchVariance);
		source.spatialize = spatialize;
		Destroy(gameObject, clip.length * 2f); // уничтожить объект после окончания клипа
	}

}

