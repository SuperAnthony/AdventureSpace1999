using UnityEditor;
using UnityEngine;
using WhatPumpkin;
using WhatPumpkin.Actions.Sequences;
using WhatPumpkin.Sgrid.Triggers;
using WhatPumpkin.Sound;
using MessageType = UnityEditor.MessageType;


/// <summary>
/// Editor window for creating the game's different custom sound types.
/// </summary>
public class CustomSoundEditor : EditorWindow
{

    /// <summary>
    /// The sound types available to create.
    /// </summary>
    private static readonly string[] SoundTypeSelections = {"Ambient", "Music", "SFX", "Random SFX"};
    
    /// <summary>
    /// Used for setting ambient audio source
    /// </summary>
    private const int Ambient = 0;

    /// <summary>
    /// Used for setting music audio source
    /// </summary>
    private const int Music = 1;

    /// <summary>
    /// Used for setting SFX audio source
    /// </summary>
    private const int SFX = 2;

    /// <summary>
    /// Index used in displaying corresponding sound editting options.
    /// </summary>
    private int _soundTypeOptionsIndex;

    /// <summary>
    /// Represents the keyname that the sound type will be assigned to.
    /// </summary>
    protected string Key;

    /// <summary>
    /// Represents the audio clip the sound type will be assigned to.
    /// </summary>
    protected AudioClip AudioClip;

    /// <summary>
    /// A reference to the sound manager.
    /// </summary>
    protected SoundManager SoundManager;

    /// <summary>
    /// Used for displaying scroll bar if content too large for the editor window.
    /// </summary>
    protected Vector2 ScrollArea;

    /// <summary>
    /// Referece to the custom audio type's Sgrid Audio Source, which hold the actual audio source.
    /// </summary>
    private SgridAudioSource _soundTypeAudioSource;

    /// <summary>
    /// Reference to the audio lister to use as the parent position of the created sound types.
    /// </summary>
    private AudioListener _audioListener;

    /// <summary>
    /// Used only for random SFX creations.
    /// </summary>
    [SerializeField] private AudioClip[] _audioClips;
    
    /// <summary>
    /// Toggle used to display warning if missing key name for the custom sound type.
    /// </summary>
    private bool _toggleKeyNameWarning = false;

    /// <summary>
    /// Toggle used to display warning if missing audio clip for the custom sound type.
    /// </summary>
    private bool _toggleAudioClipWarning = false;


    protected GameData GameData;


    [MenuItem("HiveSwap/Create/Custom Sounds")]
    private static void Init()
    {
        CustomSoundEditor customSoundEditor = (CustomSoundEditor) GetWindow(typeof (CustomSoundEditor));
        customSoundEditor.Show();
    }

    void OnEnable()
    {
        /*SoundManager.Initialize();
        SoundManager = SoundManager.Instance;*/
        /*SoundManager = FindObjectOfType<SoundManager>();*/
        GameData = FindObjectOfType<GameData>();
        if (GameData == null) GameData = new GameObject("GameData").AddComponent<GameData>();
        _audioListener = FindObjectOfType<AudioListener>();
        //CheckForMusicAudioSource();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Sound Editor", EditorStyles.boldLabel);
        /*if (SoundManager == null)
        {
            EditorGUILayout.HelpBox("GameData not present.\nPlease attach GameData to a GameObject and restart the this editor.", MessageType.Warning);
            return;
        }*/
        if (GameData == null)
        {
            EditorGUILayout.HelpBox("GameData not present.\nPlease attach GameData to a GameObject and restart the this editor.", MessageType.Warning);
            return;
        }
        if (_audioListener == null)
        {
            EditorGUILayout.HelpBox("Audio Listner not present.\n Please attach Audio Listner to the Main Camera.", MessageType.Warning);
            return;
        }
        //Begin main area.
        //Begin sound type drop down.
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        EditorGUILayout.PrefixLabel("Sound Type");
        int prevIndex = _soundTypeOptionsIndex;
        _soundTypeOptionsIndex = EditorGUILayout.Popup(_soundTypeOptionsIndex, SoundTypeSelections);
        EditorGUILayout.EndHorizontal();
        //End sound type drop down.
        
        //Begin property area.
        ScrollArea = EditorGUILayout.BeginScrollView(ScrollArea);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (prevIndex != _soundTypeOptionsIndex) Reset();
        DisplayTools(_soundTypeOptionsIndex);
        EditorGUILayout.EndScrollView();
        //End property area.
        //End main area.
    }

    /// <summary>
    /// Displays the corresponding sound tools based on the parameter passed through from the drop down.
    /// </summary>
    /// <param name="soundTypeIndex">Index used to display corresponding sound tools.</param>
    private void DisplayTools(int soundTypeIndex)
    {
        if(soundTypeIndex == 0) AmbientOptions();
        if(soundTypeIndex == 1) MusicOptions();
        if(soundTypeIndex == 2) SFXGOOptions();
        if(soundTypeIndex == 3) RandomSFXOptions();
    }

    /// <summary>
    /// Displays the options for creating Ambient sounds.
    /// </summary>
    private void AmbientOptions()
    {
        EditorGUILayout.LabelField("Ambient Sound", EditorStyles.boldLabel);
        ShowRequiredFields();
        if (GUILayout.Button("Create"))
        {
            if (!string.IsNullOrEmpty(Key) && AudioClip != null)
            {
                _soundTypeAudioSource = new GameObject(Key).AddComponent<SgridAudioSource>();
                var gO = new GameObject();
                _soundTypeAudioSource.transform.SetParent(_audioListener.transform);
                gO.transform.position = _audioListener.transform.position - new Vector3(0, 0, 10);
                _soundTypeAudioSource.gameObject.GetComponent<AudioSource>().clip = AudioClip;
                _soundTypeAudioSource.gameObject.GetComponent<AudioSource>().playOnAwake = false;
                _soundTypeAudioSource.SetAudioSourceType(Ambient);
                var view = SceneView.lastActiveSceneView;
                view.AlignViewToObject(gO.transform);
                Selection.activeGameObject = _soundTypeAudioSource.gameObject;
                view.Repaint();
                GameData.AmbientSounds.Add(new AmbientSound(_soundTypeAudioSource, AudioClip, Key));
                //SoundManager.AddAmbientSound(new AmbientSound(_soundTypeAudioSource, AudioClip, Key));
                DestroyImmediate(gO, true);
                Reset();
            }
            else
            {
                _toggleKeyNameWarning = true;
                _toggleAudioClipWarning = true;
            }
        }
        ShowMissingKeyNameWarning(0);
        ShowMissingAudioClipMessage(0);
    }

    /// <summary>
    /// Displays the options to create Music sounds.
    /// </summary>
    private void MusicOptions()
    {
        EditorGUILayout.LabelField("Music", EditorStyles.boldLabel);
        ShowRequiredFields();
        if (GUILayout.Button("Create"))
        {
            if (!string.IsNullOrEmpty(Key) && AudioClip != null)
            {
                GameData.Musics.Add(new Music(AudioClip, Key));
                //SoundManager.AddMusic(new Music(AudioClip, Key));
                Reset();
            }
            else
            {
                _toggleKeyNameWarning = true;
                _toggleAudioClipWarning = true;
            }
        }
        ShowMissingKeyNameWarning(1);
        ShowMissingAudioClipMessage(1);
    }

    /// <summary>
    /// Displays the options to create RandomSFX.
    /// </summary>
    private void RandomSFXOptions()
    {
        EditorGUILayout.LabelField("Random SFX", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        Key = EditorGUILayout.TextField("Key Name", Key);
        if (string.IsNullOrEmpty(Key)) GUILayout.Box(EditorGUIUtility.FindTexture("console.erroricon.sml"));
        EditorGUILayout.EndHorizontal();
        ScriptableObject target = this;
        SerializedObject sO = new SerializedObject(target);
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        SerializedProperty randomSFXProperty = sO.FindProperty("_audioClips");
        EditorGUILayout.PropertyField(randomSFXProperty, true);
        EditorGUILayout.EndHorizontal();
        sO.ApplyModifiedProperties();
        if (GUILayout.Button("Create"))
        {
            if (!string.IsNullOrEmpty(Key) && _audioClips != null && _audioClips.Length > 0)
            {
                foreach (var audioClip in _audioClips)
                {
                    if (audioClip == null)
                    {
                        _toggleAudioClipWarning = true;
                        break;
                    }
                    _toggleAudioClipWarning = false;
                }
                if (!_toggleAudioClipWarning)
                {
                    var randomSFXGO = new GameObject(Key + " (RandomSFX)").AddComponent<RandomSFXGO>();
                    var tAS = new GameObject(Key + " (Trigger)").AddComponent<TriggerActionSequence>();
                    randomSFXGO.GetComponent<SgridAudioSource>().SetAudioSourceType(SFX);
                    randomSFXGO.SetKey(Key);
                    randomSFXGO.AddAudioClips(_audioClips);
                    if (tAS != null) tAS.SetProperties(randomSFXGO.Key, randomSFXGO.Key);
                    if (tAS.ActionSequence == null)
                    {
                        tAS.ActionSequence = new ActionSequence();
                    }
                    tAS.AddAction("Play", randomSFXGO.Key);
                    GameData.RandomSfxs.Add(randomSFXGO);
                    //SoundManager.AddRandomSFXGO(randomSFXGO);
                    Reset();
                }
            }
            else
            {
                _toggleKeyNameWarning = true;
                _toggleAudioClipWarning = true;
            }
        }
        ShowMissingKeyNameWarning(3);
        ShowMissingAudioClipMessage(3);
    }

    /// <summary>
    /// Displays the option to create Positional sounds.
    /// </summary>
    private void SFXGOOptions()
    {
        EditorGUILayout.LabelField("SFX", EditorStyles.boldLabel);
        ShowRequiredFields();
        if (GUILayout.Button("Create"))
        {
            if (!string.IsNullOrEmpty(Key) && AudioClip != null)
            {
                var sfxgo = new GameObject(Key + " (SFX)").AddComponent<SFXGO>();
                var tAS = new GameObject(Key + " (Trigger)").AddComponent<TriggerActionSequence>();
                sfxgo.gameObject.GetComponent<AudioSource>().clip = AudioClip;
                sfxgo.gameObject.GetComponent<SgridAudioSource>().SetAudioSourceType(SFX);
                sfxgo.SetKey(Key);
                sfxgo.SetAudioClip(AudioClip);
                if (tAS != null) tAS.SetProperties(sfxgo.Key, sfxgo.Key);
                if (tAS.ActionSequence == null)
                {
                    tAS.ActionSequence = new ActionSequence();
                }
                tAS.AddAction("Play", sfxgo.Key);
                var view = SceneView.lastActiveSceneView;
                view.MoveToView(sfxgo.transform);
                view.MoveToView(tAS.transform);
                GameData.Sfxs.Add(sfxgo);
                //SoundManager.AddSFXGO(sfxgo);
                Reset();
            }
            else
            {
                _toggleKeyNameWarning = true;
                _toggleAudioClipWarning = true;
            }
        }
        ShowMissingKeyNameWarning(2);
        ShowMissingAudioClipMessage(2);
    }

    /// <summary>
    /// Displays the key field and audio clip field every sound type requires.
    /// </summary>
    private void ShowRequiredFields()
    {
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        Key = EditorGUILayout.TextField("Key Name", Key);
        if (string.IsNullOrEmpty(Key)) GUILayout.Box(EditorGUIUtility.FindTexture("console.erroricon.sml"));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal(GUIStyle.none);
        AudioClip = EditorGUILayout.ObjectField("Audio Clip", AudioClip, typeof(AudioClip), false) as AudioClip;
        if (AudioClip == null) GUILayout.Box(EditorGUIUtility.FindTexture("console.erroricon.sml"));
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Checks whether the SoundManager holds SgridAudioSources for Music sound type, and if it doesn't, it creates them.
    /// //TODO: This can probably be moved into soundmanager's MusicAudioSource get property.
    /// </summary>
    private void CheckForMusicAudioSource()
    {
        for (int i = 0; i < 2; ++i)
        {
            if (SoundManager.MusicAudioSources[i] == null)
            {
                var temp = i + 1;
                var gO = GameObject.Find("Music Aduio Source " + (temp));
                if (gO == null)
                {
                    SgridAudioSource musicAudioSource = new GameObject("Music Audio Source " + (temp)).AddComponent<SgridAudioSource>();
                    musicAudioSource.SetAudioSourceType(Music);
                    musicAudioSource.transform.SetParent(_audioListener.transform);
                    SoundManager.MusicAudioSources[i] = musicAudioSource;
                }
                else
                {
                    gO.transform.SetParent(_audioListener.transform);
                    SgridAudioSource musicAudioSource = gO.GetComponent<SgridAudioSource>();
                    if (musicAudioSource == null)
                    {
                        musicAudioSource = gO.AddComponent<SgridAudioSource>();
                    }
                    musicAudioSource.SetAudioSourceType(Music);
                    SoundManager.MusicAudioSources[i] = musicAudioSource;
                }
            }
        }
    }

    /// <summary>
    /// Warning shown if attempting to create a sound type without specifying a key name.
    /// </summary>
    private void ShowMissingKeyNameWarning(int soundType)
    {
        if (_toggleKeyNameWarning)
        {
            if (string.IsNullOrEmpty(Key))
            {
                EditorGUILayout.HelpBox("Please give the " + SoundTypeSelections[soundType] + " sound a name.", UnityEditor.MessageType.Error);
            }
        }
    }

    /// <summary>
    /// Warning shown if attempting to create a sound type without specifying an audio clip.
    /// </summary>
    private void ShowMissingAudioClipMessage(int soundType)
    {
        if (_toggleAudioClipWarning)
        {
            if (AudioClip == null)
            {
                EditorGUILayout.HelpBox("Please provide an audio clip for the " + SoundTypeSelections[soundType] + " sound.", UnityEditor.MessageType.Error);
            }
        }
    }
    

    /// <summary>
    /// Resets fields to default.
    /// </summary>
    private void Reset()
    {
        Key = "";
        AudioClip = null;
        _audioClips = null;
        _toggleKeyNameWarning = false;
        _toggleAudioClipWarning = false;
    }

    private void OnDestroy()
    {
        Reset();
    }
}