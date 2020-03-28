using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PreviewData")]
public class SongPreviewObjectCreate : ScriptableObject {
    public SongPreview Song;

}
[CreateAssetMenu(menuName = "ScriptableObject/DiffData")]
public class SongDifficultyCreate : ScriptableObject
{
    public StageData Song;

}