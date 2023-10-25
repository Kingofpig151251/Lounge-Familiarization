using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontAssetReference : Singleton<FontAssetReference>
{
    public TMP_FontAsset m_englishRegularFont;
    public TMP_FontAsset m_englishBoldFont;
    public TMP_FontAsset m_traditionalChineseRegularFont;
    public TMP_FontAsset m_traditionalChineseBoldFont;
    public TMP_FontAsset m_simplifiedChineseRegularFont;
    public TMP_FontAsset m_simplifiedChineseBoldFont;
}
