using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// 画像マーカーから原点を定める
/// </summary>
public class OriginDecideFromImageMaker : MonoBehaviour
{
    /// <summary>
    /// ARTrackedImageManager
    /// </summary>
    [SerializeField] private ARTrackedImageManager _imageManager;

    /// <summary>
    /// ARSessionOrigin
    /// </summary>
    [SerializeField] private ARSessionOrigin _sessionOrigin;

    /// <summary>
    /// ワールドの原点として振る舞うオブジェクト
    /// </summary>
    private GameObject _worldOrigin;

    /// <summary>
    /// コルーチン
    /// </summary>
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _worldOrigin = new GameObject("Origin");
        _imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        _imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    /// <summary>
    /// 原点を定める
    /// 今回は画像マーカーの位置が原点となる
    /// </summary>
    /// <param name="trackedImage">認識した画像マーカー</param>
    /// <param name="trackInterval">認識のインターバル</param>
    /// <returns></returns>
    private IEnumerator OriginDecide(ARTrackedImage trackedImage,float trackInterval)
    {
        yield return new WaitForSeconds(trackInterval);
        var trackedImageTransform = trackedImage.transform;
        _worldOrigin.transform.SetPositionAndRotation(Vector3.zero,Quaternion.identity);
        _sessionOrigin.MakeContentAppearAt(_worldOrigin.transform, trackedImageTransform.position,trackedImageTransform.localRotation);
        _coroutine = null;
    }

    /// <summary>
    /// ワールド座標を任意の点から見たローカル座標に変換
    /// </summary>
    /// <param name="world">ワールド座標</param>
    /// <returns></returns>
    public Vector3 WorldToOriginLocal(Vector3 world)
    {
        return _worldOrigin.transform.InverseTransformDirection(world);
    }

    /// <summary>
    /// TrackedImagesChanged時の処理
    /// </summary>
    /// <param name="eventArgs">検出イベントに関する引数</param>
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            StartCoroutine(OriginDecide(trackedImage,0));
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if(_coroutine == null)  _coroutine = StartCoroutine(OriginDecide(trackedImage, 5));
        }
    }
}
