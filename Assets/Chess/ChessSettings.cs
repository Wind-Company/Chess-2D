using UnityEngine;

namespace Chess
{
    public class ChessSettings
    {
        private static int? skinPack;
        public static int SkinPack
        {
            get
            {
                if(Equals(skinPack, null)) skinPack = PlayerPrefs.GetInt("ChessSkinPack", 0);
                return skinPack.Value;
            }
            set
            {
                skinPack = value;
                PlayerPrefs.SetInt("ChessSkinPack", value);
            }
        }
    }
}