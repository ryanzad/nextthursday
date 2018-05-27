using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnim : MonoBehaviour
{
	
	/*
	
		This is a sprite animation script.
		It has a few methods to help run sprite animations.
		Enjoy!
		
		- Sean
		thosesixfaces.com		
	
	*/
	
	

    public enum LoopModes { SINGLE, SINGLE_END_NULL, REPEAT }

    //to add: Ping pong


    [Header("References")]

    [Tooltip("The renderer to use")]
    public Renderer renderer;


    [Tooltip("The folder in resources to load in the sprites")]
    public string initialFolderPath;

    [Tooltip("The first frame to start in")]
    public int initialFrame;

    [Tooltip("Whether to loop the animation")]
    public LoopModes initialLoopMode;

    [Tooltip("The animation speed")]
    public float initialSpeed = 1;

    [Tooltip("The animation curve")]
    public AnimationCurve intialCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("Setup On Start")]

    [Tooltip("Loads in the folder of sprites")]
    public bool setupOnStart = true;

    [Header("Play On Start")]

    [Tooltip("Loads in the folder and sets the sprite up at start to frame 0")]
    public bool playOnStart = true;


    //internal values
    bool play;
    string folderPath;
    LoopModes loopMode = LoopModes.REPEAT;
    float speed = 30;
    float counter = 0;
    AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

    List<Texture> sprites = new List<Texture>();


    public void Play(string _folderPath, int _frame, LoopModes _loopMode, float _speed, AnimationCurve _curve)
    {
        play = false;
        FolderPath = _folderPath;
        SetFrame(_frame);
        LoopMode = _loopMode;
        Speed = _speed;
        Curve = _curve;
        play = true;
    }

    public void Play(string _folderPath, int _frame, LoopModes _loopMode, float _speed)
    {
        play = false;
        FolderPath = _folderPath;
        SetFrame(_frame);
        LoopMode = _loopMode;
        Speed = _speed;
        play = true;
    }

    public void Play(string _folderPath, int _frame, LoopModes _loopMode)
    {
        play = false;
        FolderPath = _folderPath;
        SetFrame(_frame);
        LoopMode = _loopMode;
        play = true;
    }


    public void Play(string _folderPath, int _frame)
    {
        play = false;
        FolderPath = _folderPath;
        SetFrame(_frame);
        play = true;
    }

    public void Play(string _folderPath)
    {
        play = false;
        FolderPath = _folderPath;
        SetFrame(0);
        play = true;
    }

    public void Play(int _frame)
    {
        play = false;
        SetFrame(_frame);
        play = true;
    }

    public void Play()
    {
        play = false;
        SetFrame(0);
        play = true;
    }

    public void Stop()
    {
        play = false;
    }

    public void Null()
    {
        SetTex(null);
    }



    void Start()
    {
        LoopMode = initialLoopMode;
        Speed = initialSpeed;
        Curve = intialCurve;

        if (!setupOnStart) return;

        FolderPath = initialFolderPath;
        int frame = WrapFrame(initialFrame);
        if (playOnStart)
        {
            Play(frame);
        }
        else
        {
            SetFrame(frame);
        }
    }

    public string FolderPath
    {
        set
        {
            LoadInSprites(value);
            folderPath = value;
        }
        get
        {
            return folderPath;
        }
    }

    public LoopModes LoopMode
    {
        set
        {
            loopMode = value;
        }
        get
        {
            return loopMode;
        }

    }

    public float Speed
    {
        set
        {
            speed = value;
        }
        get
        {
            return speed;
        }
    }

    public AnimationCurve Curve
    {
        set
        {
            curve = value;
        }
        get
        {
            return curve;
        }
    }




    void LoadInSprites(string _path)
    {
        sprites = new List<Texture>(Resources.LoadAll<Texture>(_path));
    }




    private void Update()
    {
        if (AllowAnim)
        {
            counter += Time.deltaTime * Speed;
            int frame = ConvertToFrame(counter);
            SetFrame(frame);

            if (counter <= Frames) return;

            counter = 0;
            if (LoopMode == LoopModes.REPEAT)
            {
            }
            else if (LoopMode == LoopModes.SINGLE)
            {
                Stop();
            }
            else if (LoopMode == LoopModes.SINGLE_END_NULL)
            {
                Stop();
                Null();
            }
        }
    }




    public bool AllowAnim
    {
        get
        {
            return play;
        }
    }

    public int Frames
    {
        get
        {
            return sprites.Count;
        }
    }

    public bool HasFrames
    {
        get
        {
            return Frames != 0;
        }
    }


    void SetFrame(string _folderPath, int _frame)
    {
        FolderPath = _folderPath;
        SetFrame(_frame);
    }

    void SetFrame(int _frame)
    {
        if (HasFrames)
        {
            if (_frame >= sprites.Count && HasFrames)
            {
                int wrappedFrame = WrapFrame(_frame);
                _frame = wrappedFrame;
            }
            SetTex(sprites[_frame]);
        }
        else
        {
            Debug.LogWarning("cannot set frame to " + _frame + " because no sprites have been loaded");
        }
    }

    void SetTex(Texture _tex)
    {
        renderer.material.mainTexture = _tex;
    }



    int ConvertToFrame(float _value)
    {
        float normalised = _value / Frames;
        float curveMapped = Curve.Evaluate(normalised);
        float unnormalised = curveMapped * Frames;
        return Mathf.FloorToInt(unnormalised);
    }

    int WrapFrame(int _frame)
    {
        return _frame % Frames;
    }

    float WrapFrame(float _frame)
    {
        return _frame % Frames;
    }


}
