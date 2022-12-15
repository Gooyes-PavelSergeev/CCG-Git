using UnityEngine;

public class Playfield : Singleton<Playfield>
{
    private PlayfieldView _view;

    public void Init()
    {
        _view = new PlayfieldView(transform);
    }
}
