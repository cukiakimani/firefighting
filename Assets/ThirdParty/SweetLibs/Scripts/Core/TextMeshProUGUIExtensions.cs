using System.Text;
using TMPro;
using UnityEngine;

namespace SweetLibs
{
    public static class TextMeshProUGUIExtensions
    {
        public static string GetDisplayString(this TextMeshProUGUI textMesh, StringBuilder stringBuilder)
        {
            stringBuilder.Clear();

            TMP_CharacterInfo[] chars = textMesh.textInfo.characterInfo;

            for (int i = 0; i < textMesh.textInfo.characterCount; i++)
            {
                stringBuilder.Append(chars[i].character);
            }

            return stringBuilder.ToString();
        }

        public static char GetCharacter(this TextMeshProUGUI textMesh, int characterIndex)
        {
            TMP_CharacterInfo charInfo = textMesh.textInfo.characterInfo[characterIndex];
            return charInfo.character;
        }

        public static Color GetCharacterColor(this TextMeshProUGUI textMesh, int characterIndex)
        {
            TMP_CharacterInfo charInfo = textMesh.textInfo.characterInfo[characterIndex];

            if (System.Char.IsWhiteSpace(charInfo.character) || charInfo.character == '\0')
            {
                return Color.clear;
            }

            int vertexIndex = charInfo.vertexIndex;
            int meshIndex = charInfo.materialReferenceIndex;
            Color32[] meshVertexColors = textMesh.textInfo.meshInfo[meshIndex].colors32;

            return meshVertexColors[vertexIndex];
        }

        public static void SetCharacterColor(this TextMeshProUGUI textMesh, int characterIndex, Color color)
        {
            TMP_CharacterInfo charInfo = textMesh.textInfo.characterInfo[characterIndex];

            if (System.Char.IsWhiteSpace(charInfo.character) || charInfo.character == '\0')
            {
                return;
            }

            int vertexIndex = charInfo.vertexIndex;
            int meshIndex = charInfo.materialReferenceIndex;
            Color32[] meshVertexColors = textMesh.textInfo.meshInfo[meshIndex].colors32;

            for (int i = 0; i < 4; i++)
            {
                meshVertexColors[vertexIndex + i] = color;
            }

            textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }

        public static void SetCharacterAlpha(this TextMeshProUGUI textMesh, int characterIndex, float alpha)
        {
            TMP_CharacterInfo charInfo = textMesh.textInfo.characterInfo[characterIndex];

            if (System.Char.IsWhiteSpace(charInfo.character) || charInfo.character == '\0')
            {
                return;
            }

            int vertexIndex = charInfo.vertexIndex;
            int meshIndex = charInfo.materialReferenceIndex;
            Color32[] meshVertexColors = textMesh.textInfo.meshInfo[meshIndex].colors32;

            Color32 originalColor = meshVertexColors[vertexIndex];
            originalColor.a = (byte) alpha;

            for (int i = 0; i < 4; i++)
            {
                meshVertexColors[vertexIndex + i] = originalColor;
            }

            textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }

        public static Vector3 GetCharacterPosition(this TextMeshProUGUI textMesh, int characterIndex)
        {
            TMP_CharacterInfo charInfo = textMesh.textInfo.characterInfo[characterIndex];

            return charInfo.topLeft;
        }
    }
}