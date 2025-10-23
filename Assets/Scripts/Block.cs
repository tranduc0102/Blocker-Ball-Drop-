using System;
using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Direction _direction;
    [SerializeField] private Rotation _rotation;
    [SerializeField] private MeshRenderer _meshRenderer;

    private Material _matInstance;
    public Direction Direction => _direction;
    public Rigidbody Rigidbody => _rigidbody;
#if UNITY_EDITOR
    private void OnValidate()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        ApplyRotation();
    }
#endif

    private void Awake()
    {
        _matInstance = _meshRenderer.material;
    }

    public void Init(ColorBlock colorBlock, Direction direction)
    {
        _direction = direction;
        // _matInstance.color = ConvertColor(colorBlock);
    }

    public void Selected()
    {
        _rigidbody.isKinematic = false;

        transform.DOKill();

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveZ(0.025f, 0.25f).SetEase(Ease.OutBack))
           .Join(DOVirtual.Float(0f, 1.5f, 0.3f, value =>
           {
               _matInstance.SetFloat("_OutlineWidth", value);
           }).SetEase(Ease.OutSine));
    }

    public void Deselected()
    {
        _rigidbody.isKinematic = true;

        transform.DOKill();

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveZ(0f, 0.25f).SetEase(Ease.InOutSine))
           .Join(DOVirtual.Float(_matInstance.GetFloat("_OutlineWidth"), 0f, 0.25f, value =>
           {
               _matInstance.SetFloat("_OutlineWidth", value);
           }));
    }
    private void ApplyRotation()
    {
        float angle = 0f;
        switch (_rotation)
        {
            case Rotation.Angle45: angle = 45f; break;
            case Rotation.Angle135: angle = 135f; break;
            case Rotation.Angle225: angle = 225f; break;
            case Rotation.Angle315: angle = 315f; break;
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

}


public enum Direction
{
  FullDirection,
  Vertical,
  Horizontal,
}

public enum ColorBlock
{
  Red,
  DarkBlue,
  Yellow,
  Green,
  Pink,
  Purple,
  Orange,
  LightBlue
}
public enum Rotation
{
    Angle45,
    Angle135,
    Angle225,
    Angle315
}